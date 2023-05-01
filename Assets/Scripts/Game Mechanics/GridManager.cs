using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [Header ("Grid Charateristics")]
    public GameObject block;
    public Transform spawnPoint;
    public int gridSize;
    public int blockIndex;

    [Header ("Generated Grid Info")]
    public List<Blocks> blocks;

    private void Awake()
    {
        Instance = this;
        blocks = new List<Blocks>(gridSize * gridSize);
        blockIndex = 0;
    }
    private void Start()
    {
        //GenerateGrid();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            GenerateGrid();
    }

    public void GenerateGrid()
    {
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                GameObject gridBloc = Instantiate(block, spawnPoint.position, Quaternion.identity, spawnPoint.parent);
                spawnPoint.position += new Vector3(3, 0, 0);
                //Debug.LogError(blocks[blockIndex].block);
                Blocks curr_block = new Blocks { block = gridBloc };
                blocks.Add(curr_block);
                blockIndex++;

                //gridBloc.GetComponent<CubeSlot>().row_p = i;
                //gridBloc.GetComponent<CubeSlot>().column_p = j;
            }
            spawnPoint.position += new Vector3(-3 * gridSize, 0, -3);
        }
    }

    public void CheckMatches()
    {
        for (int i = 0; i < gridSize * gridSize; i++)
        {
            if(blocks[i].foodComp == blocks[i+1].foodComp)
            {
                Debug.LogError("match horizontal");
            }
            if(blocks[i].foodComp == blocks[i + gridSize].foodComp)
            {
                Debug.LogError("match vertical");
            }
        }
    }    
}

[System.Serializable]
public class Blocks
{
    public bool status;
    public GameObject foodComp;
    public GameObject block;
}
