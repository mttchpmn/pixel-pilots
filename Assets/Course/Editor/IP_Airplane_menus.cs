using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IP_Airplane_menus : Editor
{
    [MenuItem("Airplane Tools/Create new airplane")]
    public static void CreateNewAirplane()
    {
        var curSelected = Selection.activeGameObject;
        if (curSelected != null)
        {
            var curController = curSelected.AddComponent<IP_Airplane_controller>();
            var curCog = new GameObject("COG");
            curCog.transform.SetParent(curSelected.transform);

            curController.centerOfGravitiyPosition = curCog.transform;
        }
    }
}
