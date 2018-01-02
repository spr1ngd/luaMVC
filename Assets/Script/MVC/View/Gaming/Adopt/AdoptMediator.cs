
using System.Collections.Generic;
using LuaMVC;
using PureMVC.Patterns;

namespace Game
{
    public class AdoptMediator : Mediator
    {
        public new const string NAME = "AdoptMediator";
        public AdoptView AdoptView
        {
            get { return (AdoptView)View; }
        }
        public AdoptMediator(IBaseView view) : base(NAME,view)
        {

        } 

        public void Last()
        {

        }
        public void Next()
        {

        } 
		public void Sure(string petName,int petID)	
        {
			UnityEngine.Debug.Log (string.Format("----------> Select the {0} name is {1}",petID,petName));
			SendNotification (NotificationType.TURNAROUND_OPEN);
			AdoptView.Close ();
			// 把数据注册到proxy
			Pet myPet = new Pet(petID,petName);
			PlayerProxy player = (PlayerProxy)Facade.RetrieveProxy ("PlayerProxy");
			player.AddPet (myPet);
			// 注册完成加载宠物场景
			SendNotification(NotificationType.V2V_GAME_PETHOUSE_OPEN);
        } 
        public void RandomAnimation()
        {

        } 
        public void PlayAnimation()
        {

        }

        public override void OnRegister()
        {
            base.OnRegister();
        }
        public override void OnRemove()
        {
            base.OnRemove();
        }
        public override void HandleNotification(INotification notification)
        {
            base.HandleNotification(notification);
        }
        public override IList<string> ListNotificationInterests()
        {
            return base.ListNotificationInterests();
        }
    }
}