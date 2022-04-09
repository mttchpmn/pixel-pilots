using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(IP_BaseAirplane_Input))]
public class IP_BaseAirplaneInput_Editor : Editor
{
    private IP_BaseAirplane_Input _targetInput;
    // Start is called before the first frame update

    private void OnEnable()
    {
        _targetInput = (IP_BaseAirplane_Input)target;
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
        EditorGUILayout.TextArea(debugInfo, GUILayout.Height(100));
        GUILayout.Space(20);
        
        Repaint();
    }
}
