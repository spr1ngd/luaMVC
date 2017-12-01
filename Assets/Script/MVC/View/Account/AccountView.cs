
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    public class AccountView : MonoBehaviour
    {
        public UnityAction ActAgain = null;
        public UnityAction ActEnd = null;

        private GameObject viewPanel = null;
        private Text scoreText = null;
        private Text highScoreText = null;

        private void Awake()
        {
            this.viewPanel = this.transform.Find("Panel").gameObject;
            this.scoreText = viewPanel.transform.Find("ScoreText").GetComponent<Text>();
            this.highScoreText = viewPanel.transform.Find("HighScoreText").GetComponent<Text>();
            viewPanel.transform.Find("EndButton").GetComponent<Button>().onClick.AddListener(OnEndClick);
            viewPanel.transform.Find("AgainButton").GetComponent<Button>().onClick.AddListener(OnAgainClick);
        }

        public void SetScore( int score,int highScore )
        {
            this.scoreText.text = score.ToString();
            this.highScoreText.text = "HighScore:" + highScore;
        }

        public void OnAgainClick()
        {
            ActAgain();
            Close();
        }

        public void OnEndClick()
        {
            ActEnd();
        }

        public void Open(int score, int highScore)
        {
            SetScore(score, highScore);
            this.viewPanel.SetActive(true);
        }

        public void Close()
        {
            this.viewPanel.SetActive(false);
        }
    }
}