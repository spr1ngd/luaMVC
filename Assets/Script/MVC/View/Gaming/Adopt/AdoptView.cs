
namespace Game
{
	using LuaMVC;
	using UnityEngine;
	using UnityEngine.UI;
	using System.Collections.Generic;

    public class AdoptView : BaseView
    {
        private AdoptMediator AdoptMediator
        {
            get { return (AdoptMediator)Mediator; }
        }

		private InputField m_inputName = null;
		private List<GameObject> m_pets = new List<GameObject>();
		private List<int> m_petList = new List<int>(){1,2}; 

        public override void Initialize()
        {
            this.ViewName = E_ViewType.Adopt;
            base.Initialize();
			m_selector = GameObject.Find ("Ground/PetSelector").GetComponent<PetSelector> ();
			m_inputName = m_transform.Find ("Panel/InputName").GetComponent<InputField> ();
            m_transform.Find("Panel/LastButton").GetComponent<Button>().onClick.AddListener(() => { AdoptMediator.Last(); });
			m_transform.Find("Panel/NextButton").GetComponent<Button>().onClick.AddListener(() => { AdoptMediator.Next();});
			m_transform.Find("Panel/SureButton").GetComponent<Button>().onClick.AddListener(() => 
				{
					if( string.IsNullOrEmpty(m_inputName.text) )
					{
						Debug.LogWarning("请为宠物设置名称");
						return;
					}
					AdoptMediator.Sure(m_inputName.text,m_selectorID);
				});
			m_transform.Find("Panel/AnimationList/RandomButton").GetComponent<Button>().onClick.AddListener(() => {AdoptMediator.RandomAnimation(); });
			Transform stateParent = m_transform.Find("Panel/AnimationList");
            // todo 生成方法 并给按钮添加点击事件

			//for (int i = 0; i < m_petList.Count; i++)   
			//{
			//    Transform dog = null;
			//    AssetLoader.LoadAssetInstantiate<Object>("Dog" + m_petList[i], obj =>
			//    {
			//        dog = obj.transform;
			//        dog.name = "Dog" + m_petList[i];
			//        dog.gameObject.AddComponent<DogBehaviour>().properties = new Dog(m_petList[i], dog.name);
			//        dog.position = new Vector3(0.18f * i, 0, 0);
			//        m_pets.Add(dog.gameObject);
			//        m_selector.SetTarget(m_pets[0].transform);
			//        m_selectorID = m_pets[0].GetComponent<DogBehaviour>().properties.id;
			//        InputBus.Instance.RegisterRaycastHit("AdoptViewRay", RayAction);
            //    }); 
			//}
        }

		private PetSelector m_selector = null;
		private int m_selectorID ; 

		private void RayAction(RaycastHit rayInfo)
		{
			if (rayInfo.transform.name.Contains ("Dog")) 
			{
				if (Input.GetMouseButtonDown (0)) 
				{
					m_selector.SetTarget (rayInfo.transform);
					m_selectorID = rayInfo.transform.GetComponent<DogBehaviour> ().properties.id;
				}
			}
		}

        public override void Open()
        {
            base.Open();
        }
        public override void Close()
        {
			InputBus.Instance.UnregisterRaycastHit ("AdoptViewRay" );
            base.Close();
        }
        public override void OpenAnimation()
        {
            base.OpenAnimation();
        }
        public override void CloseAnimation()
        {
			GameObject.Find ("Ground").SetActive (false);
            base.CloseAnimation();
			Dispose ();
        }

		public void Dispose()
		{
			for (int i = 0; i < m_pets.Count; i++) 
				GameObject.Destroy (m_pets[i]);
			m_pets = null;
		}
    }
}