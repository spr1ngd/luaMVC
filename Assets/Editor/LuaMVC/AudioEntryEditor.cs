
using System;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using Game;
using UnityEngine;

//Unity3D游戏引擎一共支持4个音乐格式的文件
//.AIFF 适用于较短的音乐文件可用作游戏打斗音效
//.WAV  适用于较短的音乐文件可用作游戏打斗音效
//.MP3 适用于较长的音乐文件可用作游戏背景音乐
//.OGG 适用于较长的音乐文件可用作游戏背景音乐

namespace LuaMVC.Editor
{
    public class AudioEntryEditor
    {
        private static Dictionary<string, string> audioClips = new Dictionary<string, string>();
        private static string rootPath = Application.streamingAssetsPath + "/";
        private static List<string> extension = new List<string>() { ".mp3", ".aiff", ".wav", ".ogg" };

        [MenuItem("LuaMVC/UpdateAudioClipFile")]
        public static void UpdateAudioClipFile()
        {
            audioClips = new Dictionary<string, string>();
            DirectoryInfo dirInfo = new DirectoryInfo(rootPath + "AudioClips");
            if (!dirInfo.Exists)
                throw new Exception(rootPath + "AudioClips" + "is not exists , please check your directory!");
            Write2Dic(dirInfo);
            JsonFS.Instance.Object2File("AudioClips", audioClips);
        }

        private static void Write2Dic(DirectoryInfo dir)
        {
            var files = dir.GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                var file = files[i];
                if (extension.Contains(file.Extension))
                {
                    string dirPath = file.FullName;
                    string fileName = file.Name.Split('.')[0];
                    dirPath = dirPath.Replace('\\', '/');
                    dirPath = dirPath.Replace(rootPath, "");
                    audioClips.Add(fileName, dirPath);
                }
            }
            var childDirs = dir.GetDirectories();
            for (int i = 0; i < childDirs.Length; i++)
                Write2Dic(childDirs[i]);
        }
    }
}