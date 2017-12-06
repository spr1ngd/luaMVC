
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LuaMVC
{
    public class Setting : BaseView
    {
        public override void Initialize()
        {
            this.ViewName = E_ViewType.Register;
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 100;
        }
        public override void Open()
        {
            m_showSettingWindow = true;
        }
        public override void Close()
        {
            m_showSettingWindow = false;
        }

        void Start()
        {
            m_LastUpdateShowTime = Time.realtimeSinceStartup;
        } 
        void Update()
        {
            m_FrameUpdate++;
            if (Time.realtimeSinceStartup - m_LastUpdateShowTime >= m_UpdateShowDeltaTime)
            {
                m_FPS = m_FrameUpdate / (Time.realtimeSinceStartup - m_LastUpdateShowTime);
                m_FrameUpdate = 0;
                m_LastUpdateShowTime = Time.realtimeSinceStartup;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
                m_showSettingWindow = true;
        }
        private void OnGUI()
        {
            if (m_showSettingWindow)
            {
                GUILayout.Window(2, new Rect(
                    new Vector2(Screen.width / 2.0f - 250, Screen.height / 2.0f - 290), new Vector2(500, 580)), DrawSettingWindow, "Setting");
            }
        } 
        private bool m_showSettingWindow = false;
        private float m_LastUpdateShowTime = 0f;
        private float m_UpdateShowDeltaTime = 0.01f;
        private int m_FrameUpdate = 0;
        private float m_FPS = 0;
        private int vSyncCount = 1;
        private int antiAliasing = 1;
        private int[] antiAliasingSetting = new[] { 0, 2, 4, 8 };
        private int textureQuality = 0;
        private int screenFull = 0;
        private int screenSize = 3;
        private List<ScreenResolution> screenSizes = new List<ScreenResolution>()
        {
            new ScreenResolution() { width = 800,height =  600},
            new ScreenResolution() { width = 1280,height =  720},
            new ScreenResolution() { width = 1366,height =  768},
            new ScreenResolution() { width = 1920,height =  1080},
        };
        // shadow
        private int shadowType = 1;
        private int shadowQuality = 2;
        private int shadowDistanceIndex = 1;
        private int[] shadowDistances = new int[] { 50, 100, 150 };

        private string NetState()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
                return "无网络";
            if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
                return "WIFI";
            if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
                return "移动网络";
            return "网络异常";
        }

#if UNITY_ANDROUD
        private int GetBatteryLevel()
        {
            try
            {
                string CapacityString = System.IO.File.ReadAllText("/sys/class/power_supply/battery/capacity");
                return int.Parse(CapacityString);
            }
            catch (Exception e)
            {
                Debug.Log("Failed to read battery power; " + e.Message);
            }
            return -1;
        }
#endif

#if UNITY_IPHONE
        private float GetBatteryLevel()
        {
            [[UIDevice currentDevice] setBatteryMonitoringEnabled:YES];
　　　　　　return [[UIDevice currentDevice] batteryLevel];
        }
#endif

        private void DrawSettingWindow( int windowID )
        {
            GUILayout.Label("系统时间：" + DateTime.Now.ToShortTimeString());

#if UNITY_ANDROUD
            GUILayout.Label("剩余电量：" + GetBatteryLevel());
#elif UNITY_IPHONE
            GUILayout.Label("剩余电量：" + GetBatteryLevel());
#elif UNITY_EDITOR || UNITY_STANDALONE_WIN
            GUILayout.Label("剩余电量：" + "***移动端可测");
#endif
            GUILayout.Label("网络状态："+ NetState());
            GUILayout.Label( "当前帧数：" + m_FPS.ToString("F1"));
            GUILayout.Label("窗口化/全屏");
            screenFull = GUILayout.SelectionGrid(screenFull, new string[] { "全屏", "窗口化" }, 2);
            GUILayout.Label("屏幕分辨率");
            screenSize = GUILayout.SelectionGrid(screenSize, new string[] { "800*600", "1280*720", "1366*768", "1920*1080" }, 4);
            GUILayout.Label("帧数设置");
            vSyncCount = GUILayout.SelectionGrid(vSyncCount, new string[] {"不封顶", "60", "30"},3);
            GUILayout.Label("抗锯齿");
            antiAliasing = GUILayout.SelectionGrid(antiAliasing, new string[] { "关闭", "2倍", "4倍", "8倍" }, 4);
            GUILayout.Label("贴图纹理质量");
            textureQuality = GUILayout.SelectionGrid(textureQuality, new string[] {"极致", "良", "中", "不忍直视"}, 4);
            GUILayout.Label("实时阴影");
            shadowType = GUILayout.SelectionGrid(shadowType,new string[]{"关闭","硬阴影","软硬兼具"},3);
            GUILayout.Label("阴影质量");
            shadowQuality = GUILayout.SelectionGrid(shadowQuality, new string[] {"不忍直视","中","高","极致" }, 4);
            GUILayout.Label("阴影距离");
            shadowDistanceIndex = GUILayout.SelectionGrid(shadowDistanceIndex, new string[] { "低", "中", "高" }, 3);
            if (GUILayout.Button("确定修改"))
            {
                QualitySettings.vSyncCount = vSyncCount;
                QualitySettings.antiAliasing = antiAliasingSetting[antiAliasing];
                QualitySettings.masterTextureLimit = textureQuality;
                if (screenFull.Equals(0))
                    Screen.fullScreen = true;
                else
                    Screen.fullScreen = false;
                ScreenResolution size = screenSizes[screenSize];
                Screen.SetResolution(size.width,size.height, Screen.fullScreen);
                QualitySettings.shadows = (ShadowQuality)shadowType;
                QualitySettings.shadowResolution = (ShadowResolution)shadowQuality;
                QualitySettings.shadowDistance = shadowDistances[shadowDistanceIndex];
                QualitySettings qs = new QualitySettings();
                ;
                Debug.Log(SimpleJson.SimpleJson.SerializeObject(qs));
            }
            if (GUILayout.Button("取消"))
            {
                m_showSettingWindow = false;
            }
        }

        public struct ScreenResolution 
        {
            public int width;
            public int height;
        }
    }
}