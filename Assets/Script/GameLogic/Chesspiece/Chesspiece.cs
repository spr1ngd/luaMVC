
using UnityEngine;

namespace Game
{
	public class Chesspiece : MonoBehaviour
	{
        private bool playAnimation = false;
		private float timer = 0;
		private Vector2 randomForce;
		private Rigidbody2D rb = null;

	    public Vector2 OriginPos;  

        //核心数据
	    public ChesspieceData CpData = null;

	    private void Awake()
	    {
	        OriginPos = this.transform.localPosition;
        }

	    private void FixedUpdate()
		{
			timer += Time.deltaTime;
			if (playAnimation) 
			{
				rb.AddForce (randomForce* 3000);
				if (timer > 0.3f) 
				{
					playAnimation = false;
					timer = 0.0f;
				}
			}
		}

		public void Eliminate()
		{
            this.GetComponent<CircleCollider2D>().enabled = false;
			rb = this.GetComponent<Rigidbody2D>();
		    if (null == rb)
		        rb = this.gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 300;
		    this.CpData.active = false;
            float randomX = Random.Range (-7,7);
			float randomY = Random.Range (8,12);
			randomForce = new Vector2 (randomX,randomY);
			playAnimation = true;
		}

	    public void AutoEliminate()
	    {
	        this.CpData.active = false;
            this.gameObject.SetActive(false);
        }
	}
}