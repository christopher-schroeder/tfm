using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TFPlanet))]
public class TFPlanetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TFPlanet planet = (TFPlanet)target;
        if (GUILayout.Button("Create Grid"))
        {
            planet.CreateGrid();
        }
    }
}