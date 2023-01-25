using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [System.Serializable]
    public class Blocks
    {
        public bool status;
        public GameObject foodComp;
        public GameObject block;
    }


    public static GridManager Instance;

    [Header ("Grid Charateristics")]
    public GameObject block;
    public Transform spawnPoint;
    public int gridSize;
    private int blockIndex = 0;

    [Header ("Generated Grid Info")]
    public Blocks[] blocks;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        blocks = new Blocks[gridSize * gridSize];
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
                blocks[blockIndex].block = gridBloc;
                blockIndex++;

                //gridBloc.GetComponent<CubeSlot>().row_p = i;
                //gridBloc.GetComponent<CubeSlot>().column_p = j;
            }
            spawnPoint.position += new Vector3(-3 * gridSize, 0, -3);
        }
    }
}
