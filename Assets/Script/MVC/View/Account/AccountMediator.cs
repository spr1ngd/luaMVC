
using System;
using System.Collections.Generic;
using PureMVC.Patterns;
using UnityEngine;

namespace Game
{
    public class AccountMediator : Mediator
    {
        public new const string NAME = "AccountMediator";
        public AccountView AccountView
        {
            get { return (AccountView) ViewComponent; }
        }

        public AccountMediator(AccountView view) : base(NAME, view)
        {
            AccountView.ActAgain += () => { SendNotification(NotificationType.V2V_GAME_START); };
            AccountView.ActEnd += () => { Debug.Log("退出游戏"); };
        }

        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                case NotificationType.V2V_GAME_ACCOUNT:
                    ScoreEvent score = notification.Body as ScoreEvent;
                    if( null == score)
                        throw new Exception("Get player record score fialed.please check your net.");
                    AccountView.Open(score.currentScore, score.highScore);
                    break;
            }
        }

        public override IList<string> ListNotificationInterests()
        {
            IList<string> notifications = new List<string>();
            notifications.Add(NotificationType.V2V_GAME_ACCOUNT);
            return notifications;
        }
    }
}