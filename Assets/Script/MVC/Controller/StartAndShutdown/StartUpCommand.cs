
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
            Facade.RegisterMediator(new LoginMediator(mainUI.LoginView));
            Facade.RegisterMediator(new HUDMediator(mainUI.HUDView));
			Facade.RegisterMediator(new ChessboardMediator(mainUI.ChessboardView));
            Facade.RegisterMediator(new MessageMediator(mainUI.MessageView));
		    Facade.RegisterMediator(new ReadyMediator(mainUI.ReadyView));
            Facade.RegisterMediator(new AccountMediator(mainUI.AccountView));
		    RegisterSetting();
		    RegisterAudioEntry();
            // proxy
            var playerProxy = new PlayerProxy();
		    Facade.RegisterProxy(playerProxy);
            var clientAddressProxy = new ServerAddressProxy();
		    Facade.RegisterProxy(clientAddressProxy);

            // command
		    Facade.RegisterCommand(NotificationType.COMMAND_ACCOUNT, new GameAccountCommand());

            // server
            Facade.RegisterHandler(new LoginHandler(playerProxy));
		    Facade.RegisterHandler(new ClientStartHandler(clientAddressProxy));
            Facade.RegisterHandler(new ClientQuitHandler());

            // notice
            SendNotification(NotificationType.SERVICE_CONNECTSERVER);// 启动游戏连接服务器
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