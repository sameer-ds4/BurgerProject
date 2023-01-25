using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelContainerData", menuName = "ScriptableObjects/LevelContainerData", order = 1)]
public class LevelContainerData : ScriptableObject
{
    public List<string> levelContainers;
}
