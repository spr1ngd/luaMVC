
namespace Game
{
	using LuaMVC;
	using UnityEngine;
	using System.Collections;
	using DG.Tweening;

	public class PetHouseView : BaseView
	{
		public PetHouseMediator PetHouseMediator
		{
			get{ return (PetHouseMediator)Mediator;}
		}

		private DogBehaviour m_dog = null;
		private float m_dogMoveSpeed = 0.2f;

		public override void Initialize ()
		{
			this.ViewName = E_ViewType.PetHouse;
			base.Initialize ();
			m_panel = m_transform.Find ("Content"); 
		}

		private void RayAction( RaycastHit hitInfo )
		{
			//todo dog move to this point position.
			if (Input.GetMouseButtonDown (0)) 
			{
				float distance = Vector3.Distance(hitInfo.point,m_dog.transform.position);
				float time = distance / m_dogMoveSpeed;
				m_dog.transform.DOMove(hitInfo.point,time);
			}
		}

		public override void Open ()
		{
			// 根据当前自己的pet
			base.Open();
			if (null == m_dog) 
			{
				// instantiate a dog by the playerproxy pets.data
				GameObject dog = PetHouseMediator.SpwanDog();
				m_dog = dog.AddComponent<DogBehaviour>();
				dog.transform.position = m_transform.Find ("Content/SpwanPos").position;
			}
			StartCoroutine (initOver());
			InputBus.Instance.RegisterRaycastHit ("PetInteractoinRay",RayAction);
		}
		private IEnumerator initOver()
		{
			yield return new WaitForSeconds (0.8f);
			PetHouseMediator.PetHouseSceneInitOver ();
		}

		public override void OpenAnimation ()
		{
			m_panel.gameObject.SetActive (true);
		}

		public override void Close ()
		{
			InputBus.Instance.UnregisterRaycastHit ("PetInteractoinRay");
			base.Close();
		}
		public override void CloseAnimation ()
		{
			m_panel.gameObject.SetActive(false);
		}
	}
}