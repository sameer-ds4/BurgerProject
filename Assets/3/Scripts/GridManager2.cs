using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager2 : MonoBehaviour
{
    public GameObject groundPlate;
    public int sizeX, sizeY;

    public static ColorPick[,] gridFormed;

    public Transform spawnPoint;
    
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

    void Start()
    {
        gridFormed = new ColorPick[sizeX,sizeY];
        spawnPoint.position = Vector3.zero;

        GenerateGrid();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) DestroyMatch();
    }

    private void GenerateGrid()
    {
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                GameObject plateSpawned = Instantiate(groundPlate, spawnPoint.position, Quaternion.identity, transform);
                plateSpawned.name = "" + i + j;
                // gridFormed[i, j] = plateSpawned;
                spawnPoint.position += new Vector3(0, 0, 1);
                // Debug.LogError(gridFormed[i,j].name);
            }

            spawnPoint.position -= new Vector3(0, 0, sizeY);
            spawnPoint.position += new Vector3(1, 0, 0);
        }
    }


    public List<ColorPick> match_H;
    public List<ColorPick> match_V;
    
    public Vector2Int currentSpot;

    private void MatchCheck_S(int x, int y)
    {
        match_H.Add(gridFormed[x, y]);
        match_V.Add(gridFormed[x, y]);
    }

    private void MCH(int x, int y)
    {
        for (int i = 0; i < sizeX; i++)
        {
            if(gridFormed[i, y] == null)
            {
                match_H.Clear();
                continue;
            }

            if(gridFormed[i,y]._color == gridFormed[x, y]._color)
            {
                if(match_H.Contains(gridFormed[i, y])) continue;

                match_H.Add(gridFormed[i, y]);
            }
            else if(match_H.Count >= 3)
            {
                DestroyMatch();
            }
            else
            {
                match_H.Clear();
            }
        }
    }

    private void MCV(int x, int y)
    {
        for (int i = 0; i < sizeY; i++)
        {
            if(gridFormed[x, i] == null) break;

            if(gridFormed[x, i]._color == gridFormed[x,y]._color)
            {
                if(match_H.Contains(gridFormed[x, i])) continue;

                match_V.Add(gridFormed[x, i]);
            }
            else if(match_V.Count >= 3)
            {
                DestroyMatch();
            }
            else
                match_V.Clear();
        }
    }

    private void MatchCheck_H(int x, int y)
    {
        //Check for Vertical
        for (int i = x; i < sizeX; i++)
        {
            if(gridFormed[i, y] == null) break;

            if(gridFormed[i, y]._color == gridFormed[x, y]._color)
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

            if(gridFormed[i, y]._color == gridFormed[x, y]._color)
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
        for (int i = y; i < sizeY; i++)
        {
            if(gridFormed[x, i] == null) break;

            if(gridFormed[x,i]._color == gridFormed[x, y]._color)
            {
                // if(i == y) continue;

                if(match_V.Contains(gridFormed[x, i])) continue;

                match_V.Add(gridFormed[x, i]);
            }
            else
            {
                break;
            }
        }

        for (int i = y; i >= 0; i--)
        {
            if(gridFormed[x, i] == null) break;

            if(gridFormed[x, i]._color == gridFormed[x, y]._color)
            {
                // if(i == y) continue;

                if(match_V.Contains(gridFormed[x, i])) continue;

                match_V.Add(gridFormed[x, i]);
            }
            else
            {
                break;
            }
        }

        // DestroyMatch();
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