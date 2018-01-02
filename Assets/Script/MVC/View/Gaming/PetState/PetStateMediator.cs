
namespace Game
{
	using PureMVC.Patterns;
	using LuaMVC;
	using System.Collections.Generic;

	public class PetStateMediator : Mediator
	{
		public new const string NAME = "PetStateView";
		public PetStateView PetStateView
		{
			get{ return (PetStateView)View;}
		}

		public PetStateMediator(IBaseView view):base(NAME,view){}

		public override void OnRegister ()
		{
			base.OnRegister ();
			PetStateView.Close ();
		}

		public override void HandleNotification (INotification notification)
		{
			switch (notification.Name) 
			{
			case NotificationType.V2V_GAME_PETHOUSE_OPEN:
				PetStateView.Open (); 
				break;
			}
		}
		public override System.Collections.Generic.IList<string> ListNotificationInterests ()
		{
			return new List<string> (){NotificationType.V2V_GAME_PETHOUSE_OPEN};
		}
	}
}