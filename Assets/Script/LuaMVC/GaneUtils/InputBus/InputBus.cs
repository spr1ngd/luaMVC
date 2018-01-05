 
namespace LuaMVC
{
	using UnityEngine;
	using UnityEngine.EventSystems;
	using System;
	using System.Collections.Generic;

    public class InputBus : MonoBehaviour
    {
		private static InputBus m_instance;
		public static InputBus Instance
		{
			get
			{
				if (null == m_instance) 
				{
					GameObject RayMaster = new GameObject ("RayMaster");
					m_instance = RayMaster.AddComponent <InputBus>();
				}
				return m_instance;
			}
		}
		private Ray m_ray ;
		private RaycastHit m_rayInfo; 
		private IDictionary<string,Action<RaycastHit>> m_rayRequests = new Dictionary<string, Action<RaycastHit>>();

		private void Update()
		{
			if (EventSystem.current.IsPointerOverGameObject())
				return;
			m_ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (m_ray, out m_rayInfo))
			{ 
				foreach (var ray in m_rayRequests.Values)
					ray (m_rayInfo);
			}
		}

		public void RegisterRaycastHit( string requestName, Action<RaycastHit> callback )
		{
			if (m_rayRequests.ContainsKey (requestName))
				return;
			m_rayRequests.Add(requestName,callback);
		}

		public void UnregisterRaycastHit( string requestName )
		{
			if (!m_rayRequests.ContainsKey (requestName))
				return;
			m_rayRequests.Remove(requestName);
		}
    }
}