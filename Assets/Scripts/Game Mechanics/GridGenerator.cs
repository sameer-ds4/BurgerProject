using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public static GridGenerator Instance;

    public int gridSize;

    public GameObject block;
    public Transform spawnPoint;

    private void Awake()
    {
        Instance = this;
    }


}
