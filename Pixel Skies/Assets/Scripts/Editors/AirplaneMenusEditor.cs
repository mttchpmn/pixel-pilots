using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AirplaneMenusEditor : Editor
{
    [MenuItem("Airplane Tools/Create new airplane")]
    public static void CreateNewAirplane()
    {
        var curSelected = Selection.activeGameObject;
        if (curSelected != null)
        {
            var curController = curSelected.AddComponent<AirplaneController>();
            var curCog = new GameObject("COG");
            curCog.transform.SetParent(curSelected.transform);

            curController.centerOfGravityPosition = curCog.transform;
        }
    }
}
