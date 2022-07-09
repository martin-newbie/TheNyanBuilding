using UnityEngine;
using GameDuo;
using System;
using UnityEditor;

[Serializable]
public abstract class TextDataBase : StaticDataBase
{
    public string TextDataPath;
    public virtual void SaveForTextData()
    {

    }
    public virtual void LoadForTextData()
    {

    }

    public virtual void Load()
    {
        
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(TextDataBase), true)]
public class TextDataBaseEditor : StaticDataEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        TextDataBase myScript = (TextDataBase)target;
        GUILayout.Space(20f);
        GUILayout.Label("Text Data");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("SaveText"))
        {
            myScript.SaveForTextData();
        }
        if (GUILayout.Button("LoadText"))
        {
            myScript.LoadForTextData();
        }
        GUILayout.EndHorizontal();
    }
}
#endif