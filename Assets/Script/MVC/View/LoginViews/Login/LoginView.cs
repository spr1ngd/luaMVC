
using System.Collections;
using cn.sharesdk.unity3d;
using LuaMVC; 
using UnityEngine;
using UnityEngine.UI; 

namespace Game
{
    public class LoginView : BaseView
    {
        private InputField username = null;
        private InputField password = null;
        private Button loginButton = null;
        private Button touristButton = null;
        private Button registerButton = null;
        private LoginMediator LoginMediator
        {
            get { return (LoginMediator) Mediator; }
        }

        private ShareSDK shareSDK = null;

        public override void Initialize()
        {
            this.ViewName = E_ViewType.Login;
            base.Initialize();
            shareSDK = this.m_transform.GetComponent<ShareSDK>();
            shareSDK.authHandler = OnAuthResultHandler;

            username = this.m_transform.Find("Panel/Username").GetComponent<InputField>();
            password = this.m_transform.Find("Panel/Password").GetComponent<InputField>();
            loginButton = this.m_transform.Find("Panel/LoginButton").GetComponent<Button>();
            touristButton = this.m_transform.Find("Panel/TouristButton").GetComponent<Button>();
            registerButton = this.m_transform.Find("Panel/RegisterButton").GetComponent<Button>();
            loginButton.onClick.AddListener(() => { LoginMediator.Login(new Player(username.text, password.text)); });
            registerButton.onClick.AddListener(() => { LoginMediator.Register(); });
            touristButton.onClick.AddListener(() => { LoginMediator.TouristLogin(); });
            this.m_transform.Find("Panel/SMSButton").GetComponent<Button>().onClick.AddListener(() =>
            {
                shareSDK.Authorize(PlatformType.SMS);
            });
        }

        private void OnAuthResultHandler( int requestID,ResponseState state,PlatformType type,Hashtable result )
        {
            switch (state)
            {
                case ResponseState.Success:
                    Debug.Log("Get the register data:" + MiniJSON.jsonEncode(result));
                    break;
                case ResponseState.Fail:
                    Debug.LogWarning( "Get mobile register failed." );
                    break;
                case ResponseState.Cancel:
                    Debug.LogWarning("The mobile register cancel.please input the admin/password login.");
                    break;
            }
        }

        public override void Open()
        {
            this.gameObject.SetActive(true);
            base.Open(); 
        }
        public override void Close()
        {
            this.gameObject.SetActive(false);
            base.Close(); 
        }
        public override void OpenAnimation()
        {
            base.OpenAnimation();
        }
        public override void CloseAnimation()
        {
            base.CloseAnimation();
        }
    }
}