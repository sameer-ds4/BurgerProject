using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [Header ("Grid Charateristics")]
    // public GameObject block;
    public CubeInfo cube;
    public Transform spawnPoint;
    public int gridSize;

    [Header ("Generated Grid Info")]
    // public List<Blocks> blocks;
    public List<CubeInfo> cubes;

    private void Awake()
    {
        Instance = this;
        // blocks = new List<Blocks>(gridSize * gridSize);
        
        cubes = new List<CubeInfo>(gridSize * gridSize);
    }
    private void Start()
    {
        GenerateGrid();
    }


    public void GenerateGrid()
    {
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                // GameObject gridBloc = Instantiate(block, spawnPoint.position, Quaternion.identity, spawnPoint.parent);
                CubeInfo currentcube = Instantiate(cube, spawnPoint.position, Quaternion.identity, spawnPoint.parent);
                spawnPoint.position += new Vector3(3, 0, 0);

                cubes.Add(currentcube);
                //Debug.LogError(blocks[blockIndex].block);
                // Blocks curr_block = new Blocks { block = gridBloc };
                // blocks.Add(curr_block);
                // blockIndex++;

                //gridBloc.GetComponent<CubeSlot>().row_p = i;
                //gridBloc.GetComponent<CubeSlot>().column_p = j;
            }
            spawnPoint.position += new Vector3(-3 * gridSize, 0, -3);
        }
    }

    private void CheckMatches(int index)
    {
        if(cubes[index].cube.burgerpart == cubes[index + 1].cube.burgerpart)
        {
            
        }

    }

    // public void CheckMatches()
    // {
    //     for (int i = 0; i < gridSize * gridSize; i++)
    //     {
    //         // if(blocks[i].foodComp == blocks[i+1].foodComp)
    //         // {
    //         //     Debug.LogError("match horizontal");
    //         // }
    //         // if(blocks[i].foodComp == blocks[i + gridSize].foodComp)
    //         // {
    //         //     Debug.LogError("match vertical");
    //         // }
    //     }
    // }    
}

[System.Serializable]
public class Blocks
{
    public bool status;
    public GameObject foodComp;
    public GameObject block;
}
