
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Game
{
    public class LoginView : MonoBehaviour
    {
        public UnityAction<Player> ActLogin = null;
        public UnityAction ActRegister = null;
        public UnityAction ActTouristLogin = null;

        private InputField username = null;
        private InputField password = null;
        private Button loginButton = null;
        private Button touristButton = null;
        private Button registerButton = null; 

        private void Awake()
        {
            username = this.transform.Find("Username").GetComponent<InputField>();
            password = this.transform.Find("Password").GetComponent<InputField>();
            loginButton = this.transform.Find("LoginButton").GetComponent<Button>();
            touristButton = this.transform.Find("TouristButton").GetComponent<Button>();
            registerButton = this.transform.Find("RegisterButton").GetComponent<Button>();
            loginButton.onClick.AddListener(Login);
            registerButton.onClick.AddListener(ActRegister);
            touristButton.onClick.AddListener(ActTouristLogin);
        }

        private void Login()
        {
            ActLogin(new Player(username.text, password.text));
        }

        public void Close()
        {
            this.gameObject.SetActive(false);
        }
    }
}