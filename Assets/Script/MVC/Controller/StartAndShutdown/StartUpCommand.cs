
using System;
using LuaMVC;
using PureMVC.Patterns;
using UnityEngine;

namespace Game
{
	public class StartUpCommand : SimpleCommand
	{
        public override void Execute (INotification notification)
		{
			base.Execute (notification);
		    ProgramEntry mainUI = notification.Body as ProgramEntry;
            if( !mainUI )
                throw new Exception("mainUI is null ,please check it before running!");
            Facade.RegisterMediator(new LoadingMediator(mainUI.LoadingView));
            Facade.RegisterMediator(new LoginMediator(mainUI.LoginView));
            Facade.RegisterMediator(new AdoptMediator(mainUI.AdoptView));
            Facade.RegisterMediator(new PetHouseMediator(mainUI.PetHouseView));
            Facade.RegisterMediator(new TurnaroundMediator(mainUI.TurnaroundView));
            Facade.RegisterMediator(new PetStateMediator(mainUI.PetStateView));
            Facade.RegisterMediator(new PetInteractionMediator(mainUI.PetInteraction)); 
            Facade.RegisterMediator(new LoadingMediator(mainUI.LoadingView)); 

            // todo 系统控制器目前还是手动构造注册。等待改进 有框架统一自动构造是比较合适的
            RegisterSetting();
		    RegisterAudioEntry();
            // proxy
            var playerProxy = new PlayerProxy();
		    Facade.RegisterProxy(playerProxy);
            var clientAddressProxy = new ServerAddressProxy();
		    Facade.RegisterProxy(clientAddressProxy);

            // command
		    // Facade.RegisterCommand(NotificationType.COMMAND_ACCOUNT, new GameAccountCommand());

            // server
		    Facade.RegisterHandler(new ClientStartHandler(clientAddressProxy));
		    Facade.RegisterHandler(new LoginHandler(playerProxy));
            Facade.RegisterHandler(new ClientQuitHandler());

            // notice
		    // 启动游戏连接服务器
            SendNotification(NotificationType.SERVICE_CONNECTSERVER);
        }

	    private void RegisterSetting()
	    {
	        GameObject settingGO = new GameObject("Setting");
	        var settingView = settingGO.AddComponent<Setting>();
	        Facade.RegisterMediator(new SettingMediator(settingView));
        }

	    private void RegisterAudioEntry()
	    {
	        GameObject audioEntryGO = new GameObject("AudioEntry");
	        var audioEntryView = audioEntryGO.AddComponent<AudioEntry>();
	        Facade.RegisterMediator(new AudioEntryMediator(audioEntryView));
        }
	}
}