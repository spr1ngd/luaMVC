
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace Game
{
	public class ChessboardView :MonoBehaviour ,IPointerClickHandler
	{
        public UnityAction<string> ActPlayAudio = null;
		public UnityAction<string> ActUpdateScore = null;
	    public UnityAction ActAddTime = null;
	    public UnityAction ActReduceTime = null;
	    public UnityAction<int> ActAccount = null;

		private Chessboard Chessboard = null;
		private ChesspieceData[,] ChesspieceDatas = null;
	
		private GameObject chesspiecePrefab = null;
	    [HideInInspector]
	    public int TotalScore = 0;

        private Ray2D ray;  
	    private RaycastHit2D raycastHit;

        private void Start()  
		{
			this.Chessboard = this.GetComponent<Chessboard>();
			this.chesspiecePrefab = Resources.Load<GameObject> ("Prefabs/UI/Chesspiece");
		    ChesspieceDatas = Chessboard.CBData.BuilderChesspieces();
			DrawChesspieces ();
		}

		public void OnPointerClick(PointerEventData eventData)
		{
            // 0. 如果点的是豆豆，error audio
		    if (eventData.pointerCurrentRaycast.gameObject.name.Contains("Chesspiece"))
		    {
		        ActPlayAudio("Error");
                //todo 增加时间和减少时间可以不用命令通知，感觉很费劲
		        ActReduceTime();
		        return;
            }

            // 1. 取得十字点豆豆
		    var crossCps = GetChesspieces(eventData.position);
		 
            // 2. 判定消除
            int score = CrossEliminate(crossCps);
            if (score.Equals(0))
            {
                ActPlayAudio("Error");
                //todo 增加时间和减少时间可以不用命令通知，感觉很费劲
                ActReduceTime();
                return;
            }
            // 3. 算分
            ActPlayAudio("Click");
		    //todo 增加时间和减少时间可以不用命令通知，感觉很费劲
            ActAddTime();
            TotalScore += score;
            ActUpdateScore(TotalScore.ToString());
            if (false == AutoChecking())
            {
                Debug.Log("Game Over");
                RefreshChessboard();
            }
        }

        // todo 应该由Chessboard来控制生成
		public void DrawChesspieces()
		{
            for (int x = 0; x < Chessboard.xCount; x++)
			{
                for (int y = 0; y < Chessboard.yCount; y++) 
				{
					ChesspieceData chesspieceData = ChesspieceDatas[x,y];					
					GameObject go = GameObject.Instantiate (chesspiecePrefab,this.transform) as GameObject;
					go.name = "Chesspiece[" + x + "," + y +"]";
					go.transform.localPosition = Chessboard.GetPosition (x, y);
					go.GetComponent<Image>().color = chesspieceData.Color;
				    go.AddComponent<Chesspiece>().CpData = chesspieceData;
                    var cp = go.GetComponent<Chesspiece>().CpData;
					cp.active = chesspieceData.active;
					cp.ChesspieceType = chesspieceData.ChesspieceType;
				    cp.origin = go.transform.position;
                    cp.index.x = x;
				    cp.index.y = y;
				    ChesspieceDatas[x, y] = cp;
                    if(!chesspieceData.active)
                        go.GetComponent<Chesspiece>().AutoEliminate();
                }
			}
		}

		private void EliminateChesspieces( List<Chesspiece> chesspieces )
		{
			for (int i = 0; i < chesspieces.Count; i++)
				chesspieces [i].Eliminate ();
		}

		private bool AutoChecking()
		{
            for (int x = 0; x < Chessboard.xCount; x++)
            {
                for (int y = 0; y < Chessboard.yCount; y++)
                {
                    var cp = ChesspieceDatas[x, y];
                    if (cp.active)
                        continue;
                    if (canEliminate(GetChesspieces(cp.origin)))
                        return true;
                }
            }
            return false;
		}

	    public int CurrentRemainChesspieces()
	    {
	        int count = 0;
	        for (int x = 0; x < Chessboard.xCount; x++)
	        {
	            for (int y = 0; y < Chessboard.yCount; y++)
	            {
	                var cp = ChesspieceDatas[x, y];
	                if (cp.active)
	                    count++;
	            }
	        }
	        return count;
	    }

	    public void RefreshChessboard()
	    {
	        ChesspieceDatas = null;
	        var cps = this.GetComponentsInChildren<Chesspiece>();
	        for( int i = 0 ; i < cps.Length;i++ )
                GameObject.Destroy(cps[i].gameObject);
            ChesspieceDatas = Chessboard.CBData.BuilderChesspieces();
	        DrawChesspieces();
        }

	    public void ResetChessboard()
	    {
            Debug.Log("重新开始");
	    }

	    // 获取交叉的棋子
	    private List<Chesspiece> GetChesspieces( Vector2 position )
	    {
            var crossCps = new List<Chesspiece>();
	        var upHit = Physics2D.Raycast(position, Vector2.up);
	        var downHit = Physics2D.Raycast(position, Vector2.down);
	        var leftHit = Physics2D.Raycast(position, Vector2.left);
	        var rightHit = Physics2D.Raycast(position, Vector2.right);
	        if (upHit.transform != null)
	            crossCps.Add(upHit.transform.GetComponent<Chesspiece>());
	        if (downHit.transform != null)
	            crossCps.Add(downHit.transform.GetComponent<Chesspiece>());
	        if (leftHit.transform != null)
	            crossCps.Add(leftHit.transform.GetComponent<Chesspiece>());
	        if (rightHit.transform != null)
	            crossCps.Add(rightHit.transform.GetComponent<Chesspiece>());
	        return crossCps;
	    }

        // 消除
	    private int CrossEliminate(List<Chesspiece> chesspieces)
	    {
	        int score = 0;
	        if (null == chesspieces || chesspieces.Count == 0)
	            return score;
	        eliminate(chesspieces, ref score);
	        return score;
	    }
	    private void eliminate(List<Chesspiece> chesspieces, ref int score)
	    {
	        for (int i = 0; i < chesspieces.Count - 1; i++)
	        {
	            var curCp = chesspieces[i];
	            var eliminatingCps = new List<Chesspiece>() { curCp };
	            for (int j = i + 1; j < chesspieces.Count; j++)
	            {
	                var nextCp = chesspieces[j];
	                if (curCp.CpData.ChesspieceType == nextCp.CpData.ChesspieceType)
	                    eliminatingCps.Add(nextCp);
	            }
	            if (eliminatingCps.Count > 1)
	            {
	                score += eliminatingCps.Count;
	                EliminateChesspieces(eliminatingCps);
	            }
	        }
	    }

	    private bool canEliminate(List<Chesspiece> chesspieces)
	    {
	        for (int i = 0; i < chesspieces.Count - 1; i++)
	        {
	            var curCp = chesspieces[i];
	            for (int j = i + 1; j < chesspieces.Count; j++)
	            {
	                var nextCp = chesspieces[j];
	                if (curCp.CpData.ChesspieceType == nextCp.CpData.ChesspieceType)
	                    return true;
                }
            }
	        return false;
	    }

        // 重新开始
	    public void Restart()
	    {
	        TotalScore = 0;
            RefreshChessboard();
	    }
	}	
}
