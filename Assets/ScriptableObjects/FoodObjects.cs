using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FoodObjects", menuName = "ScriptableObjects/FoodObjects", order = 2)]

public class FoodObjects : ScriptableObject
{
    public DebgTest de;
}


[System.Serializable]
public class DebgTest : UnityEditor.Editor
{
    public int food;
    public float value;
    public bool eatable;

    public DebgTest()
    {
        food = 3;
        value = 6;
        eatable = true;
    }

    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Create New Object"))
        {
            new DebgTest();
        }
        base.OnInspectorGUI();
    }
}