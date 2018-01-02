
namespace Game
{
	using LuaMVC;
	using UnityEngine;
	using UnityEngine.UI;

	public class PetStateView : BaseView
	{
		public PetStateMediator PetStateMediator
		{
			get{ return (PetStateMediator)Mediator;}
		}

		private Image m_headImage = null;
		private PetStateItem m_hungerState= null;
		private PetStateItem m_happyState = null;
		private PetStateItem m_energyState = null;

		public override void Initialize ()
		{
			this.ViewName = E_ViewType.PetState;
			base.Initialize ();
			m_headImage = m_panel.Find ("HeadImage").GetComponent<Image> ();
			Transform content = m_panel.Find ("StatesList/Content");
			m_hungerState = content.Find ("Hunger").gameObject.AddComponent<PetStateItem> ();
			m_happyState = content.Find ("Happy").gameObject.AddComponent<PetStateItem> ();
			m_energyState = content.Find ("Energy").gameObject.AddComponent<PetStateItem> ();
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

	internal class PetStateItem : MonoBehaviour
	{
		private Slider m_progress = null;
		private Image m_fill = null;

		private void OnInitialize()
		{
			m_progress = transform.Find ("Progress").GetComponent<Slider>();
			m_fill = transform.Find ("Fill Area/Fill").GetComponent<Image> ();
		}

		public void SetProgressValue( float progress )
		{
			m_progress.value = progress;
			m_fill.color = Color.Lerp (Color.green,Color.red,progress);
		}
	}
}