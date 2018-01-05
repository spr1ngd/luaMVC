
namespace LuaMVC
{
	public partial class NotificationType
	{
		// OPERATE 鼠标操作
		public const string CLICK = "MouseClick";

		// TOHUD 视图信息传递
		public const string UPDATESCORE = "UpdateScore";
		public const string UPDATETIMESLIDER = "UpdateTimeSlider";
        
        // TIME 时间 todo 消除
	    //public const string TIME_TIMEIN = "TimeIn";
	    //public const string TIME_TIMEUP = "TimeUp";
	    //public const string TIME_ADDUPPERLIMIT = "AddUpperLimit";
	    //public const string TIME_ADDTIME = "AddTime";
	    //public const string TIME_REDUCETIME = "ReduceTime";
	    //public const string TIME_REMAIN = "RemainTime";
	    //public const string TIME_TIMEWARNING = "TimeWarning";

        // MODEL TO VIEW
	    public const string M2V_PLAYERINFO_UPDATE = "UpdatePlayerInfo";
	    public const string M2V_PLAYERINFO_UPDATEFAIL = "UpdatePlayerInfoFailed";
	    public const string M2V_UPDATE_PROGRESS = "UpdateProgressValue";

        // VIEW TO VIEW
	    public const string V2V_GAME_START = "GameStart";
	    public const string V2V_GAME_ACCOUNT = "GameAccount";
	    public const string V2V_GAME_REGISTER = "Register";
		public const string V2V_GAME_PETHOUSE_OPEN = "OpenPethouse";

        // todo COMMAND 
        public const string COMMAND_ACCOUNT = "Account";

		// todo OPEN TURNAROUND
		public const string TURNAROUND_OPEN = "OpenTurnaround"; 
		public const string TURNAROUND_CLOSE = "CloseTurnaround";
	}
}