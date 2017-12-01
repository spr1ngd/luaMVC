
using System;
using PureMVC.Patterns;
using System.Collections.Generic;

namespace Game
{
	public class HUDMediator : Mediator
	{
		public new const string NAME = "HUDMediator";
		public HUDView HUDView
		{
			get{ return (HUDView)ViewComponent;}
		}

		public HUDMediator(HUDView view):base(NAME,view){}

		public override void HandleNotification (INotification notification)
		{
			switch(notification.Name)
			{
				case NotificationType.UPDATESCORE:
					HUDView.UpdateScore (notification.Body as string);
					break;
				case NotificationType.UPDATETIMESLIDER:
				
					break;
                case NotificationType.TIME_REMAIN:
                    //var time = notification.Body as TimeEvent;
                    //if ( null == time )
                    //    throw new Exception("数据丢失");
                    //HUDView.UpdateTimeSlider(time.RemainTime / time.UpperLimitTime,time.RemainTime);
                    break;
                case NotificationType.M2V_PLAYERINFO_UPDATE:
                    Role role = notification.Body as Role;
                    if( null == role )
                        throw new Exception("抓取角色信息失败");
                    //HUDView.UpdatePlayerInfo(role.name,role.level.ToString(),role.maxScore.ToString());
                    Loom.InvokeAsync((para1,para2,para3) => { HUDView.UpdatePlayerInfo(para1.ToString(),para2.ToString(),para3.ToString()); }, role.name,role.level,role.maxscore);
                    break;
                case NotificationType.V2V_GAME_START:
                    HUDView.Restart();
                    break;
			}
		}

		public override IList<string> ListNotificationInterests ()
		{
			IList<string> notifications = new List<string> ();
			notifications.Add (NotificationType.UPDATESCORE);
			notifications.Add (NotificationType.UPDATETIMESLIDER);
            notifications.Add (NotificationType.TIME_REMAIN);
            notifications.Add (NotificationType.M2V_PLAYERINFO_UPDATE);
            notifications.Add (NotificationType.V2V_GAME_START);
			return notifications;
		}
	}
}