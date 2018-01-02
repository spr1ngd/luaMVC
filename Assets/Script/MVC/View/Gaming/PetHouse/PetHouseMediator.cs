
namespace Game
{
	using PureMVC.Patterns;
	using LuaMVC;
	using System.Collections.Generic;
	using UnityEngine;

	public class PetHouseMediator : Mediator
	{
		public new const string NAME = "PethouseMediator";
		public PetHouseView PetHouseView
		{
			get{return (PetHouseView)View; }
		}
		public PetHouseMediator(IBaseView view):base(NAME,view){}

		public override void OnRegister ()
		{
			base.OnRegister ();
			PetHouseView.Close ();
		}

		public void PetHouseSceneInitOver()
		{ 
			SendNotification (NotificationType.TURNAROUND_CLOSE);
		}
		public GameObject SpwanDog()
		{
			PlayerProxy playerProxy = (PlayerProxy)Facade.RetrieveProxy ("PlayerProxy");
			Pet pet = playerProxy.GetPet ();
		    GameObject dog = null;
		    AssetLoader.LoadAssetInstantiate<Object>("Dog" + pet.id, obj => { dog = (GameObject)obj; });
            return dog;
		}

		public override void HandleNotification (INotification notification)
		{
			switch (notification.Name) 
			{
			case NotificationType.V2V_GAME_PETHOUSE_OPEN:
				PetHouseView.Open (); 
				break;
			}
		}
		public override System.Collections.Generic.IList<string> ListNotificationInterests ()
		{
			return new List<string> (){NotificationType.V2V_GAME_PETHOUSE_OPEN};
		}
	}
}