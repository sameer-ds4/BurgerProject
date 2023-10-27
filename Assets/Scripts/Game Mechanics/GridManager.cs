using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header ("Grid Charateristics")]
    public GameObject cube;
    public Vector2Int gridSize;
    [SerializeField]
    private Transform spawnPoint;

    public static BurgerObject[,] gridFormed;


  private void OnEnable() 
    {
        PlayerInput.CheckMatch += MatchCheck_S;
        PlayerInput.CheckMatch += MatchCheck_H;
        PlayerInput.CheckMatch += MatchCheck_V;
        PlayerInput.MatchRemoval += DestroyMatch;
    }

    private void OnDisable() 
    {
        PlayerInput.CheckMatch -= MatchCheck_S;
        PlayerInput.CheckMatch -= MatchCheck_H;
        PlayerInput.CheckMatch -= MatchCheck_V;
        PlayerInput.MatchRemoval -= DestroyMatch;
    }

    private void Start()
    {
        gridFormed = new BurgerObject[gridSize.x, gridSize.y];
        spawnPoint.position = Vector3.zero;

        GenerateGrid();
    }


    public void GenerateGrid()
    {
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                GameObject currentcube = Instantiate(cube, spawnPoint.position, Quaternion.identity, spawnPoint.parent);
                spawnPoint.position += new Vector3(3, 0, 0);
                currentcube.name = "" + i + j;
                // cubes.Add(currentcube);
            }
            spawnPoint.position += new Vector3(-3 * gridSize.y, 0, -3);
        }
    }

    [HideInInspector]
    public List<BurgerObject> match_H;
    [HideInInspector]
    public List<BurgerObject> match_V;
    
    private void MatchCheck_S(int x, int y)
    {
        match_H.Add(gridFormed[x, y]);
        match_V.Add(gridFormed[x, y]);
    }

    private void MatchCheck_H(int x, int y)
    {
        //Check for Horizontal
        for (int i = x; i < gridSize.x; i++)
        {
            if(gridFormed[i, y] == null) break;

            if(gridFormed[i, y].burgerPart == gridFormed[x, y].burgerPart)
            {
                // if(i == x) continue;

                if(match_H.Contains(gridFormed[i, y])) continue;

                match_H.Add(gridFormed[i, y]);
            }
            else
                break;
        }

           for (int i = x; i >= 0; i--)
        {
            if(gridFormed[i, y] == null) break;

            if(gridFormed[i, y].burgerPart == gridFormed[x, y].burgerPart)
            {
                // if(i == x) continue;

                if(match_H.Contains(gridFormed[i, y])) continue;

                match_H.Add(gridFormed[i, y]);
            }
            else
            {
                break;
            }
        }
    }

    private void MatchCheck_V(int x, int y)
    {
        //Check for Vertical
        for (int i = y; i < gridSize.y; i++)
        {
            if (gridFormed[x, i] == null) break;

            if (gridFormed[x, i].burgerPart == gridFormed[x, y].burgerPart)
            {
                // if(i == y) continue;

                if (match_V.Contains(gridFormed[x, i])) continue;

                match_V.Add(gridFormed[x, i]);
            }
            else
            {
                break;
            }
        }

        for (int i = y; i >= 0; i--)
        {
            if (gridFormed[x, i] == null) break;

            if (gridFormed[x, i].burgerPart == gridFormed[x, y].burgerPart)
            {
                // if(i == y) continue;

                if (match_V.Contains(gridFormed[x, i])) continue;

                match_V.Add(gridFormed[x, i]);
            }
            else
            {
                break;
            }
        }
    }

    private void DestroyMatch()
    {
        if(match_H.Count >= 3)
        {
            foreach (var item in match_H)
            {
                Destroy(item.gameObject);
            }
            match_H.Clear();
        }
        else
            match_H.Clear();

        if(match_V.Count >= 3)
        {
            foreach (var item in match_V)
            {
                Destroy(item.gameObject);
            }
            match_V.Clear();
        }
        else
            match_V.Clear();
    }
}
