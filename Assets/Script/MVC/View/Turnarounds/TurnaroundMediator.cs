
namespace Game
{
	using LuaMVC;
	using PureMVC.Patterns;
	using System.Collections.Generic;

	public class TurnaroundMediator : Mediator
	{
		public new const string NAME = "TurnaroundMediator";
		public TurnaroundView Turnaround
		{
			get{ return (TurnaroundView)View;}
		}
		public TurnaroundMediator(IBaseView view) : base(NAME,view)
		{
			
		}

		public override void OnRegister ()
		{
			base.OnRegister ();
			Turnaround.Close ();
		}

		public override void HandleNotification (INotification notification)
		{
			switch( notification.Name )
			{
			case NotificationType.TURNAROUND_OPEN:
				Turnaround.Open ();
				break;
			case NotificationType.TURNAROUND_CLOSE:
				Turnaround.Close ();
				break;
			}
		}

		public override System.Collections.Generic.IList<string> ListNotificationInterests ()
		{
			return new List<string> (){ NotificationType.TURNAROUND_OPEN,NotificationType.TURNAROUND_CLOSE };
		}
	}
}