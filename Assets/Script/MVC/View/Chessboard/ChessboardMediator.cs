
using PureMVC.Patterns;
using System.Collections.Generic;

namespace Game
{
	public class ChessboardMediator : Mediator
	{
		public new const string NAME = "ChessboardMediator";
		public ChessboardView ChessboardView
		{
			get{ return (ChessboardView)ViewComponent;}
		}

		public ChessboardMediator( ChessboardView view ) : base(NAME,view)
		{
			ChessboardView.ActUpdateScore += ( score ) => { SendNotification(NotificationType.UPDATESCORE,score);};
		    ChessboardView.ActPlayAudio += (audioName) => { SendNotification(NotificationType.AUDIO_PLAY, audioName); };
		    ChessboardView.ActAddTime += () => { SendNotification(NotificationType.TIME_ADDTIME);};
		    ChessboardView.ActReduceTime += () => { SendNotification(NotificationType.TIME_REDUCETIME);};
        }

		public override void HandleNotification (INotification notification)
		{
		    switch (notification.Name)
		    {
                case NotificationType.V2V_GAME_START:
                    ChessboardView.Restart();
                    break;
                case NotificationType.TIME_TIMEUP:
                    SendNotification(NotificationType.COMMAND_ACCOUNT,ChessboardView.TotalScore.ToString());
                    break;
		    }
		}

        //todo 只应该监听两个接口 开始和结束
		public override IList<string> ListNotificationInterests ()
		{
			IList<string> notifications = new List<string> ();
            notifications.Add(NotificationType.V2V_GAME_START);
            notifications.Add(NotificationType.TIME_TIMEUP);
			return notifications;
		}
	}
}