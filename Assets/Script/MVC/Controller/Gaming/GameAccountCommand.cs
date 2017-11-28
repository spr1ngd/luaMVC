
using System;
using PureMVC.Patterns;

namespace Game
{
    public class GameAccountCommand :SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            PlayerProxy player = Facade.RetrieveProxy("PlayerProxy") as PlayerProxy;
            if( null == player )
                throw new Exception("Get role data failed,please reconnect server!");
            int highScore = int.Parse(notification.Body as string);
            player.UpdateRoleHighScore(highScore);
            SendNotification(NotificationType.V2V_GAME_ACCOUNT, new ScoreEvent(highScore, player.HighScoreRecord));
        }
    }
}