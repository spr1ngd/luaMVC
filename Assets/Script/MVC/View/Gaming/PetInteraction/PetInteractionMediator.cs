
namespace Game
{
	using LuaMVC;
	using PureMVC.Patterns;
	using System.Collections.Generic;

	public class PetInteractionMediator : Mediator
	{
		public new const string NAME = "PerInteractionMediator";
		public PetInteraction PetInteraction
		{
			get{ return (PetInteraction)View;}
		}
		public PetInteractionMediator(IBaseView view):base(NAME,view){}

		public override void OnRegister()
		{
			base.OnRegister ();
			PetInteraction.Close ();
		}

		public override void HandleNotification (INotification notification)
		{
			switch (notification.Name) 
			{
			case NotificationType.V2V_GAME_PETHOUSE_OPEN:
				PetInteraction.Open (); 
				break;
			}
		}
		public override System.Collections.Generic.IList<string> ListNotificationInterests ()
		{
			return new List<string> (){NotificationType.V2V_GAME_PETHOUSE_OPEN};
		}
	}
}