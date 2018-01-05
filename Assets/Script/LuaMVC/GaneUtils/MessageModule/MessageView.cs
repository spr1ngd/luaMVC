
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class MessageView : MonoBehaviour
    {
        private Text messageText = null;
        private GameObject messageView = null;
        private bool opening = false;
        private float time = 0.0f;

        private void Awake()
        {
            this.messageView = this.transform.Find("MessageView").gameObject;
            this.messageText = this.transform.Find("MessageView/Text").GetComponent<Text>();
        }

        private void Update()
        {
            if (opening)
            {
                time += Time.deltaTime;
                if (time > 3.0f)
                {
                    Hide();
                    time = 0.0f;
                    opening = false;
                }
            }
        }

        public void ShowMessage( string content )
        {
            opening = true;
            Show();
            messageText.text = content;
        }

        private void Show()
        {
            messageView.SetActive(true);
        }

        private void Hide()
        {
            messageView.SetActive(false);
        }
    }
}