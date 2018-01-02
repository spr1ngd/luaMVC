
namespace Game
{
	using DG.Tweening;
	using UnityEngine;
	using LuaMVC;

	internal class PetSelector : BaseView
	{
		private Transform m_selector = null;

		private void Awake()
		{
			m_selector = transform.Find ("Selector");
			m_selector.DOLocalMoveY (0.05f,1).SetLoops(-1,LoopType.Yoyo);
		} 

		public void SetTarget( Transform parent )
		{
			if (null == parent) 
			{
				CloseAnimation ();
				return;
			}
			transform.SetParent (parent);
			transform.localPosition = new Vector3 (0,0.2f,0);
			OpenAnimation ();
		}

		public override void OpenAnimation ()
		{
			m_selector.gameObject.SetActive (true);
		}

		public override void CloseAnimation ()
		{
			m_selector.gameObject.SetActive (false);
		}
	}
}