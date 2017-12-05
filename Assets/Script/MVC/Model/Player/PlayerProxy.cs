
using System.Collections.Generic;
using PureMVC.Patterns;
using UnityEngine;

namespace Game
{
    public class PlayerProxy : Proxy
    {
        public new const string NAME = "PlayerProxy";
        private Player player;
        public PlayerProxy() : base(NAME)
        {
            player = new Player();
        }
        public PlayerProxy(Player player) : base(NAME, player)
        {
            this.player = player;
        }

        #region Login && Role
        public void RequestLogin( object data )
        {
            SendNotification(NotificationType.SERVICE_LOGIN, data);
        }
        public void LoginError()
        {
            SendNotification(NotificationType.SERVICE_LOGIN_FAILED);
        }

        public void UpdateRole( List<Role> roles )
        {
            if (null == roles)
            {
                // todo 创建新角色
                Debug.Log("roles is null");
            }
            else
            {
                player.roles = new List<Role>(roles);
                SendNotification(NotificationType.SERVICE_LOGIN_SUCCESS);
                SendNotification(NotificationType.M2V_PLAYERINFO_UPDATE, player.roles[0]);
            }
        }
        public void AddRole( Role role )
        {
            player.roles.Add(role);
        }
        public void RemoveRole( Role role )
        {
            player.roles.Remove(role);
        }

        #endregion

        #region Update Role Information

        public int HighScoreRecord
        {
            get { return player.roles[0].maxscore; }
        }

        public void UpdateRoleHighScore( int highScore )
        {
            if (highScore > player.roles[0].maxscore)
                player.roles[0].maxscore = highScore;
        }

        public void ModifyRoleName(Role role, string newName)
        {
            //todo 修改用户名称，并提交服务器
        }

        #endregion

        public Role Submit()
        {
            if (player.roles == null || player.roles.Count.Equals(0))
                return null;
            return player.roles[0];
        }
    }
}