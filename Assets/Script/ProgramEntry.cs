
using System.Collections;
using LuaMVC;
using UnityEngine;

namespace Game
{
	public class ProgramEntry : MonoBehaviour
	{
        [HideInInspector] public LoginView LoginView = null;
        [HideInInspector] public MessageView MessageView = null;
	    [HideInInspector] public LoadingView LoadingView = null;
		[HideInInspector] public AdoptView AdoptView = null;
		[HideInInspector] public PetHouseView PetHouseView = null;
		[HideInInspector] public TurnaroundView TurnaroundView = null; 
		[HideInInspector] public PetStateView PetStateView = null;
		[HideInInspector] public PetInteraction PetInteraction = null;
         
	    private ApplicationFacade facade = null;
	    private LuaApplicationFacade luaFacade = null;

	    private void Start()
	    {
	        StartCoroutine(Init());   
        }

	    private IEnumerator Init()
	    { 
            yield return AssetLoader.OnInitialize();
            yield return StartUp();
            facade = new ApplicationFacade();
            facade.StartUp(this); // application facade 需要手动调用初始化
            luaFacade = new LuaApplicationFacade(); // lua facade 会在构造时进行初始化 
        }

        private IEnumerator StartUp()
        {  
            Loom.Instance.Init();
            Transform canvas = GameObject.Find("Canvas/UICamera").transform; 
			PetHouseView = GameObject.Find("PetHouse").GetComponent<PetHouseView>();

            AssetLoader.LoadAssetInstantiate<Object>("Views.unity3d","Adopt", panel =>
            { 
                panel.transform.SetParent(canvas);
                panel.transform.localScale = Vector3.one;
                AdoptView = panel.AddComponent<AdoptView>(); 
            });
            AssetLoader.LoadAssetInstantiate<Object>("Views.unity3d", "Login", panel => 
            { 
                panel.transform.SetParent(canvas);
                panel.transform.localScale = Vector3.one;
                LoginView = panel.AddComponent<LoginView>(); 
            });
            AssetLoader.LoadAssetInstantiate<Object>("Views.unity3d", "Loading", panel =>
            { 
                panel.transform.SetParent(canvas);
                panel.transform.localScale = Vector3.one;
                LoadingView = panel.AddComponent<LoadingView>();
            });
            AssetLoader.LoadAssetInstantiate<Object>("Views.unity3d", "Message", panel =>
             {
                 panel.transform.SetParent(canvas);
                 panel.transform.localScale = Vector3.one;
                 MessageView = panel.AddComponent<MessageView>();
             });
            AssetLoader.LoadAssetInstantiate<Object>("Views.unity3d", "PetStateView", panel =>
            {
                panel.transform.SetParent(canvas);
                panel.transform.localScale = Vector3.one;
                PetStateView = panel.AddComponent<PetStateView>();
            });
            AssetLoader.LoadAssetInstantiate<Object>("Views.unity3d", "PetInteraction", panel =>
            {
                panel.transform.SetParent(canvas);
                panel.transform.localScale = Vector3.one;
                PetInteraction = panel.AddComponent<PetInteraction>();
            });
            AssetLoader.LoadAssetInstantiate<Object>("Views.unity3d", "Turnaround", panel =>
            {
                panel.transform.SetParent(canvas);
                panel.transform.localScale = Vector3.one;
                TurnaroundView = panel.AddComponent<TurnaroundView>();
            });

            yield return null;
        }

	    private void OnApplicationQuit()
	    {
	        luaFacade.ShutDown();
            facade.ShutDown(); 
        }
    }
}