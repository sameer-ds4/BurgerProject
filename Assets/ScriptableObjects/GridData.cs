using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Grid Data", menuName = "ScriptableObjects/Grid Data", order = 3)]
public class GridData : ScriptableObject
{
    public GameObject cube;
    public Vector2Int gridSize;
}