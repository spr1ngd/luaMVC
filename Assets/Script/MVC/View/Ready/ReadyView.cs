
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    public class ReadyView : MonoBehaviour
    {
        public UnityAction ActStartGame = null;
        private GameObject ReadyPanel = null;

        private void Awake()
        {
            ReadyPanel = this.transform.Find("Panel").gameObject;
            this.ReadyPanel.transform.Find("StartButton").GetComponent<Button>().onClick.AddListener(CloseView);
        }

        public void OpenView()
        {
            this.ReadyPanel.SetActive(true);
        }

        public void CloseView()
        {
            this.ReadyPanel.SetActive(false);
            ActStartGame();
        }
    }
}