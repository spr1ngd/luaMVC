
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
	public class HUDView : MonoBehaviour
	{
        // time
		private Text scoreText = null;
		private Slider timerSlider = null;
	    private Text remainTimeText = null;
	    private Image fill = null;

        // player information
	    private Text nameText = null;
	    private Text levelText = null;
        private Text maxScoreText = null;

		private void Awake()
		{
			scoreText = this.transform.Find ("Score").GetComponent<Text> ();	
			timerSlider = this.transform.Find ("Timer").GetComponent<Slider> ();
		    remainTimeText = timerSlider.transform.Find("RemainTimeText").GetComponent<Text>();
		    fill = timerSlider.transform.Find("Fill Area/Fill").GetComponent<Image>();
		    nameText = this.transform.parent.Find("PlayerInfo/NameText").GetComponent<Text>();
		    levelText = this.transform.parent.Find("PlayerInfo/LevelText").GetComponent<Text>();
		    maxScoreText = this.transform.parent.Find("PlayerInfo/HighScroeText").GetComponent<Text>();
		}

        // time
	    public void UpdateScore( string score )
		{
			this.scoreText.text = score;
		}
		public void UpdateTimeSlider( float timeScale ,float remainTime )
		{
			timerSlider.value = timeScale;
		    remainTimeText.text = remainTime.ToString("F1");
		    fill.color = GetColor(timeScale);
		}
	    private Color GetColor( float value )
	    {
            if(value > 0.5f)
	            return Color.Lerp(Color.green, new Color(0, 0.75f, 1, 1), (value - 0.5f) * 2);
	        return Color.Lerp(Color.red, Color.green, value * 2);
        }

        // player information
	    public void UpdatePlayerInfo( string playerName, string level, string maxScore)
        {
            nameText.text = playerName;
            levelText.text = "Lv."+ level;
            maxScoreText.text = "High Score:"+maxScore;
        }

	    public void Restart()
	    {
	        scoreText.text = "0";
	    }
	}
}