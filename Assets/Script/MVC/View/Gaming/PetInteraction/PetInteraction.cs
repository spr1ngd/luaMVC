
namespace Game
{
	using LuaMVC;
	using UnityEngine;
	using UnityEngine.UI;

	public class PetInteraction : BaseView
	{
		public PetInteractionMediator PetInteractionMediator
		{
			get{ return (PetInteractionMediator)Mediator;}
		}
		
		public override void Initialize ()
		{
			this.ViewName = E_ViewType.PetInteraction;
			base.Initialize ();
			m_panel.Find ("FarmButton").GetComponent<Button> ().onClick.AddListener (()=>{Debug.Log("农场资源暂未开发");});
			m_panel.Find ("ARButton").GetComponent<Button> ().onClick.AddListener (()=>{Debug.Log("AR功能暂未开发");});
			m_panel.Find ("FeedButton").GetComponent<Button> ().onClick.AddListener (()=>{Debug.Log("喂食功能暂未开发");});
			m_panel.Find ("PlayButton").GetComponent<Button> ().onClick.AddListener (()=>{Debug.Log("玩耍暂未开发");});
			m_panel.Find ("TrainButton").GetComponent<Button> ().onClick.AddListener (()=>{Debug.Log("训练暂未开发");});
			m_panel.Find ("VoiceButton").GetComponent<Button> ().onClick.AddListener (()=>{Debug.Log("语音识别暂未开发");});
		}

		public override void Open ()
		{
			base.Open ();
		}
		public override void OpenAnimation ()
		{
			base.OpenAnimation ();
		}

		public override void Close ()
		{
			base.Close ();
		}
		public override void CloseAnimation ()
		{
			base.CloseAnimation ();
		}
	}
}