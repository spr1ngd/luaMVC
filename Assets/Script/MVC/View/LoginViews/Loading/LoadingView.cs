
using LuaMVC; 
using UnityEngine.UI;

namespace Game
{
    public class LoadingView : BaseView
    {
        private Slider m_progressSlider = null;
        private Text m_progressText = null;
		private Timer m_timer = null;

        public override void Initialize()
        {
            this.ViewName = E_ViewType.Loading;
            base.Initialize();
            m_progressSlider = transform.Find("Panel/Progress").GetComponent<Slider>();
			m_progressText = transform.Find("Panel/Progress/Text").GetComponent<Text>(); 
			m_timer = new Timer ("LoadingTest");
			m_timer.OnTimingAction += (time) =>
			{
				if(time > 0.8f)
					this.Close();
			};
			m_timer.StartTime ();
        }

        public void SetProgress( float progress )
        {
            m_progressSlider.value = progress;
            m_progressText.text = (progress * 100).ToString("F1") + "%";
        }
    }
}