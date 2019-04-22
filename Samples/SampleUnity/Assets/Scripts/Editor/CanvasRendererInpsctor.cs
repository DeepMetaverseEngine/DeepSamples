using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(CanvasRenderer), true)]
public class CanvasRendererInpsctor : Editor
{
    public string current = "0";
    public override void OnInspectorGUI()
    {
        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Depth:");
        current = GUILayout.TextField(current);
        if (GUILayout.Button("SetDepth"))
        {
            int newCurrent = 0;
            if (int.TryParse(current, out newCurrent))
            {
                Selection.activeTransform.SetSiblingIndex(newCurrent);
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("BringToBack"))
        {
            Selection.activeTransform.SetAsFirstSibling();
            current = string.Format("{0}", Selection.activeTransform.GetSiblingIndex());
        }

        if (GUILayout.Button("BringToFront"))
        {
            Selection.activeTransform.SetAsLastSibling();
            current = string.Format("{0}", Selection.activeTransform.GetSiblingIndex());
        }
        GUILayout.EndHorizontal();
    }

}
