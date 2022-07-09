
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace GameDuo {
    [Serializable]
    public abstract class StaticDataBase : ScriptableObject
    {
        internal static class Key
        {
            internal const string name = "name";
            internal const string json = "json";
            internal const string version = "version";
            internal const string admin_password = "admin_password";
        }


        public const string title = "GAMEDUO STATIC DATA";
        public const string localPath = "localPath";
        [NonSerialized]
        public Vector2 scroll;
        [NonSerialized]
        public string csv;
        public int currentVersion;
        [NonSerialized]
        protected string prevLoadedJsonData;
        private const string LocalDirStr = "Assets/Editor Default Resources/GameDuo/StaticData/{0}";
        private const string LocalPathStr = "Assets/Editor Default Resources/GameDuo/StaticData/{0}/{1}.json";

        private string DirPath
        {
            get
            {
                return string.Format(LocalDirStr, base.name);
            }
        }
        private string LocalPath
        {
            get
            {
                return string.Format(LocalPathStr, base.name, currentVersion);
            }
        }
        public string Json
        {
            get
            {
                return JsonUtility.ToJson(this);
            }
        }
        public void ValidCheck()
        {

        }

        static public int[] StringToIntArray(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return null;
            string[] div = str.Split(',');
            int[] arr = new int[div.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = int.Parse((div[i]));
            }
            return arr;
        }
        static public float[] StringToFloatArray(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return null;
            string[] div = str.Split(',');
            float[] arr = new float[div.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = float.Parse((div[i]));
            }
            return arr;
        }
        static public double[] StringToDoubleArray(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return null;
            string[] div = str.Split(',');
            double[] arr = new double[div.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = double.Parse(div[i], System.Globalization.CultureInfo.InvariantCulture);
            }
            return arr;
        }

#if UNITY_EDITOR
        internal void LocalLoadForEditor()
        {
            string json= EditorGUIUtility.Load(LocalPath).ToString();
            JsonUtility.FromJsonOverwrite(json, this);
            EditorUtility.DisplayDialog("notice", "static data 로드 성공", "확인");
        }
        internal void LocalSaveForEditor()
        {
            if (!Directory.Exists(DirPath))
            {
                Directory.CreateDirectory(DirPath);

            }

            using (FileStream fs = new FileStream(LocalPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(Json);
                }
            }
            EditorUtility.DisplayDialog(title, "static data 저장 성공", "확인");
        }
#endif
        public virtual void ReadForCSV()
        {

        }
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(StaticDataBase), true)]
    public class StaticDataEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            StaticDataBase myScript = (StaticDataBase)target;


            GUILayout.Space(20f);
            GUILayout.Label("CSV reader");
            myScript.scroll = EditorGUILayout.BeginScrollView(myScript.scroll);
            myScript.csv = GUILayout.TextArea(myScript.csv, GUILayout.ExpandHeight(true));
            EditorGUILayout.EndScrollView();
            if (GUILayout.Button("Read"))
            {
                myScript.ReadForCSV();
            }

            GUILayout.Space(20f);
            GUILayout.Label("Local");
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Load"))
            {
                myScript.LocalLoadForEditor();
            }
            if (GUILayout.Button("Save"))
            {
                myScript.LocalSaveForEditor();
            }
            GUILayout.EndHorizontal();
        }
       
    }
#endif



}
