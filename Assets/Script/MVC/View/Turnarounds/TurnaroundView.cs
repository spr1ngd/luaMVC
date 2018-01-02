
namespace Game
{
	using UnityEngine;
	using UnityEngine.UI;
	using LuaMVC;
	using DG.Tweening;
	using System.Collections;

	public class TurnaroundView : BaseView
	{
		private Text m_systemTime = null;
		private Text m_remainEnergy = null;
		private Text m_netState = null;
		private Text m_tip = null;
		private Transform m_ground = null; 

		public override void Initialize ()
		{ 
			this.ViewName = E_ViewType.TurnaroundA;
			base.Initialize ();
			m_ground = m_panel.Find ("Ground");
			m_tip = m_panel.Find ("Tip").GetComponent<Text>();
			m_systemTime = m_panel.Find ("SystemTime").GetComponent<Text>();
			m_remainEnergy = m_panel.Find ("RemainEnergy").GetComponent<Text> ();
			m_netState = m_panel.Find ("NetState").GetComponent<Text> ();
		}

		private void Update()
		{
			if (Input.GetKeyDown (KeyCode.F2)) {
				Close ();
			}
		}

		public override void Open ()
		{
			m_tip.text = "小提示会从文本档中读出来，方便更新之后新增提示内容";
			m_systemTime.text = System.DateTime.Now.ToShortTimeString ();
			m_remainEnergy.text = "需要从设置项中读取";
			m_netState.text = "WIFI 从Application读取，或者从设置中读取";
			OpenAnimation ();
		}
		public override void OpenAnimation ()
		{ 
			m_panel.gameObject.SetActive (true);  
			m_ground.DOScale (Vector3.one,0.3f);
		}

		public override void Close ()
		{
			CloseAnimation ();
		}
		public override void CloseAnimation ()
		{ 
			m_ground.DOScale (new Vector3 (0, 1, 1), 0.3f);
			StartCoroutine (close());
		}
		private IEnumerator close()
		{
			yield return new WaitForSeconds (0.3f);
			m_panel.gameObject.SetActive (false);
		}
	}
}