using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header ("Grid Charateristics")]
    private Vector2Int gridSize;
    [SerializeField] private Transform spawnPoint;
    public static BurgerObject[,] gridFormed;
    private int gridCount;


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
        gridSize = GameManager.Instance.gridData.gridSize;
        gridFormed = new BurgerObject[gridSize.x, gridSize.y];      //Reinitializing grid on every start/reload
        spawnPoint.position = Vector3.zero;

        GenerateGrid();
        CameraSet();
    }

    public void GenerateGrid()
    {
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                GameObject currentcube = Instantiate(GameManager.Instance.gridData.cube, spawnPoint.position, Quaternion.identity, spawnPoint.parent);
                spawnPoint.position += new Vector3(3, 0, 0);
                currentcube.name = "" + i + j;
            }
            spawnPoint.position += new Vector3(-3 * gridSize.y, 0, -3);
        }

    }

    private void CameraSet()
    {
        Vector2 middlePoint = new Vector2((3 * ((float)gridSize.y - 1))/2, (-3 * ((float)gridSize.x - 1))/2);
        CameraManager.Instance.SetCameraFocus(middlePoint, gridSize.y - 3);
    }

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\\


    [HideInInspector] public List<BurgerObject> match_H;
    [HideInInspector] public List<BurgerObject> match_V;
    
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
                if (match_V.Contains(gridFormed[x, i])) continue;

                match_V.Add(gridFormed[x, i]);
            }
            else
            {
                break;
            }
        }
    }

    bool orderMatched;  //Remove after getting vector2 solution
    [SerializeField] Vector2 iconPos;
    private void DestroyMatch()
    {
        orderMatched = false;

        if(match_H.Count >= 3)
        {
            // OrderManager.Instance.CheckMatch(match_H[0].burgerPart);
            orderMatched = OrderManager.Instance.CheckMatch(match_H[0].burgerPart);
            // iconPos = OrderManager.Instance.CheckMatch(match_H[0].burgerPart);
            StartCoroutine(AnimateMatchMade(match_H));
            // StartCoroutine(AnimMatches(match_H, iconPos));
            gridCount = gridCount - match_H.Count;
        }
        else
            match_H.Clear();

        if(match_V.Count >= 3)
        {
            // OrderManager.Instance.CheckMatch(match_V[0].burgerPart);
            orderMatched = OrderManager.Instance.CheckMatch(match_V[0].burgerPart);
            // iconPos = OrderManager.Instance.CheckMatch(match_V[0].burgerPart);
            StartCoroutine(AnimateMatchMade(match_V));
            // StartCoroutine(AnimMatches(match_V, iconPos));
            gridCount = gridCount - match_V.Count;
        }
        else
            match_V.Clear();

        gridCount++;

        if (gridCount == gridSize.x * gridSize.y)
        {
            UIManager.Instance.gameOver.SetActive(true);
        }
    }

    //Take vector2 return type instead of bool and use it to animate the match. If no order item, send a extra or blank vector2 address.
    //Make the match tween to one and move to the ordered object

    IEnumerator AnimateMatchMade(List<BurgerObject> objects)        // Try to get the reference of matched item from UI card to animate when order item completed
    {
        GameManager.startPlay = false;
        yield return new WaitForSeconds(0.3f);
        foreach (var item in objects)
        {
            Tweening.BubbleOut_gameobject(item.gameObject, 0.5f, Vector3.one * 1.8f, Vector3.one * 0.3f);
        }
        yield return new WaitForSeconds(0.5f);
        foreach (var item in objects)
        {
            Destroy(item.gameObject);
        }
        objects.Clear();
        GameManager.startPlay = true;
    }

    IEnumerator AnimMatches(List<BurgerObject> tweenObjects, Vector2 tweenPos)
    {
        GameManager.startPlay = false;
        yield return new WaitForSeconds(0.3f);
        
        foreach (var item in tweenObjects)
        {
            //Tween together)
        }

        yield return new WaitForSeconds(.8f);

        foreach (var item in tweenObjects)
        {
            
            //Tween to tweenPos
        }
        
        //Punch icon//

        yield return new WaitForSeconds(1.3f);

        foreach (var item in tweenObjects)
        {
            Destroy(item.gameObject);
        }
        tweenObjects.Clear();
        GameManager.startPlay = true;
    }

}