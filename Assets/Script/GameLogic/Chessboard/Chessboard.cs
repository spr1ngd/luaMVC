
// chessboard 
// springdong
// draw the battleground

using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    //todo Chessboard应该只负责面板的绘制，和对外的接口
	public class Chessboard : MaskableGraphic
	{
	    private ChessboardData m_cbData = null;
	    public ChessboardData CBData
	    {
	        get
	        {
	            if( null == m_cbData )
                    m_cbData = new ChessboardData(this);
	            return m_cbData;
	        }
	    }

	    public int xCount = 11;
		public int yCount = 8;
        [Range(0.1f,0.9f)]
	    public float chesspiecePercent = 0.8f;
        public Color color0 = Color.white;
		public Color color1 = Color.gray;

		private Color m_curColor = Color.white;
		private Color CurColor
		{
			get
			{
				if (m_curColor.Equals (color1)) 
					m_curColor = color0;
				else if (m_curColor.Equals (color0)) 
					m_curColor = color1;
				return m_curColor;
			}
			set{ m_curColor = value;}
		}

		private Vector3 origin ;
		private float perLenght;

		protected override void OnPopulateMesh (VertexHelper vh)
		{
           //todo  Debug.Log("刷新显示");
            CurColor = color0;
			vh.Clear();
			Vector2 size = this.GetPixelAdjustedRect().size;
         
			float xPerCell = size.x / yCount;
			float yPerCell = size.y / xCount;
			perLenght = xPerCell;
			if (yPerCell < xPerCell)
				perLenght = yPerCell;

			// calc lefttop start point
			origin = new Vector2(-yCount/2.0f * perLenght,xCount/2.0f * perLenght);
            for (int y = 0; y < xCount; y++) 
			{
				var index = CurColor;
				for (int x = 0; x < yCount; x++) 
				{
					var leftTop = origin + new Vector3 (x * perLenght,-y * perLenght,0);
					var rightTop = origin + new Vector3 ((x +1)*perLenght,-y * perLenght,0);
					var rightBottom = origin + new Vector3 ((x+1)*perLenght, -(y+1) * perLenght,0);
					var leftBottom = origin + new Vector3 (x * perLenght,-(y+1) * perLenght,0);
					Color col = CurColor;
					vh.AddUIVertexQuad (new UIVertex[]
					{
						GetUIVertex(leftTop,col),
						GetUIVertex(rightTop,col),
						GetUIVertex(rightBottom,col),
						GetUIVertex(leftBottom,col)
					});
				}
			}
		}

        //todo 这个方法需要优化掉
		public Vector3 GetPosition( int xIndex ,int yIndex )
		{
            UpdateGeometry();
            return origin + new Vector3 (yIndex * perLenght + 0.5f * perLenght,-xIndex * perLenght - 0.5f * perLenght,0);
		}

	    private UIVertex GetUIVertex(Vector2 point, Color vertexColor)
	    {
	        return new UIVertex { position = point, color = vertexColor };
	    }

        //todo 重置棋盘
	    public void Refresh()
	    {

	    }
	}
}