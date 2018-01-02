
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

        public void LoginSuccess(string serverData)
        { 
            SendNotification(NotificationType.SERVICE_LOGIN_SUCCESS);
        }
        public void LoginError()
        {
            SendNotification(NotificationType.SERVICE_LOGIN_FAILED);
        }
        public void RegisterError()
        {
            // 将失败的消息通知出去
            //SendNotification();
        }
        public void RegisterSuccess()
        {
            // 将注册成功的消息通知到view层
            //SendNotification("");
        }
        public void ModifyPasswordSuccess()
        {
            // 通知修改成功 重新登陆
        }
        public void ModifyPasswordFailed()
        {
            // 通知修改失败 返回失败代码
        }

        public void UpdatePet( List<Pet> pets )
        {
			if (null == pets)
            { 
                Debug.Log("no pets.");
            }
            else
            {
				
            }
        } 

		public void AddPet( Pet pet )
		{
			player.pets.Add (pet);
		}
		public void RemovePet()
		{
			// 弃养pet,不可随便
		}
		public Pet GetPet( int id = 0 )
		{
			return player.pets [id];
		}
    }
}