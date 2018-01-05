
using UnityEngine; 
using System.Collections;
using LuaMVC;

namespace LuaMVC
{
	public class Program : MonoBehaviour
	{
		protected ApplicationFacade facade = null;
		protected LuaApplicationFacade luaFacade = null;

		protected virtual void Awake(){}

		protected virtual void Start()
		{
			StartCoroutine (Init());
		} 

		protected virtual void Update(){}

		protected virtual IEnumerator Init()
		{
			yield return AssetLoader.OnInitialize();
			yield return StartUp();
		}

		protected virtual IEnumerator StartUp()
		{  
			Loom.Instance.Init();
			yield return null;
		}

		protected virtual void OnApplicationQuit()
		{
			luaFacade.ShutDown();
			facade.ShutDown(); 
		}
	}
} 