using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DebugTool : EditorWindow
{
    [MenuItem("Window/Debug Tools")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        DebugTool window = (DebugTool)EditorWindow.GetWindow(typeof(DebugTool));
        window.Show();
    }

    void OnGUI()
    {
        if (GUILayout.Button("Reset Save"))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}