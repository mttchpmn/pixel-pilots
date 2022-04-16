using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AirplaneInputBase))]
public class AirplaneInputBaseEditor : Editor
{
    private AirplaneInputBase _targetInput;
    // Start is called before the first frame update

    private void OnEnable()
    {
        _targetInput = (AirplaneInputBase)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var debugInfo = "";
        debugInfo += $"Pitch: \t\t{_targetInput.Pitch}\n";
        debugInfo += $"Roll: \t\t{_targetInput.Roll}\n";
        debugInfo += $"Yaw: \t\t{_targetInput.Yaw}\n";
        debugInfo += $"Brake: \t\t{_targetInput.Brake}\n";
        debugInfo += $"Throttle: \t{_targetInput.Throttle}\n";
        debugInfo += $"Flaps: \t\t{_targetInput.Flaps}\n";

        GUILayout.Space(10);
        GUILayout.Label("Input Values");
        EditorGUILayout.TextArea(debugInfo, GUILayout.Height(100));
        GUILayout.Space(10);
        GUILayout.Label("Computed Values");
        EditorGUILayout.TextField($"Throttle Setting: {_targetInput.ThrottleSetting}");
        GUILayout.Space(20);
        
        Repaint();
    }
}
