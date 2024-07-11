using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header ("Grid Charateristics")]
    [SerializeField] private Transform spawnPoint;

    public GridData gridData;

    [HideInInspector] public Vector2Int gridSize;
    public static BurgerObject[,] gridFormed;
    private int gridCount;
    private int gridDifficulty;


    public delegate void ScoreUpdate(int amt);
    public static event ScoreUpdate IncrementScore;


  private void OnEnable() 
    {
        PlayerInput.CheckMatch += BombCheck;
        PlayerInput.CheckMatch += MatchCheck_S;
        PlayerInput.CheckMatch += MatchCheck_H;
        PlayerInput.CheckMatch += MatchCheck_V;
        PlayerInput.MatchRemoval += DestroyMatch;
    }

    private void OnDisable() 
    {
        PlayerInput.CheckMatch -= BombCheck;
        PlayerInput.CheckMatch -= MatchCheck_S;
        PlayerInput.CheckMatch -= MatchCheck_H;
        PlayerInput.CheckMatch -= MatchCheck_V;
        PlayerInput.MatchRemoval -= DestroyMatch;
    }

    private void Start()
    {
        if(!SaveDataHandler.Instance.saveData.tutorial)
            gridSize = SetDifficulty();
    
        gridFormed = new BurgerObject[gridSize.x, gridSize.y];      //Reinitializing grid on every start/reload
        spawnPoint.position = Vector3.zero;

        GenerateGrid();
        CameraSet();
    }

    private Vector2Int SetDifficulty()
    {
        gridDifficulty = GameManager.difficultyIndex;

        int option = gridData.levelDifficulty[gridDifficulty].difficulty.Length;
        return gridData.levelDifficulty[gridDifficulty].difficulty[Random.Range(0, option)].gridSize;
    }

    public void GenerateGrid()
    {
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                GameObject currentcube = Instantiate(gridData.cube, spawnPoint.position, Quaternion.identity, spawnPoint.parent);
                spawnPoint.position += new Vector3(3, 0, 0);
                currentcube.name = "" + i + j;

                if(SaveDataHandler.Instance.saveData.tutorial && i == 0)
                {
                    currentcube.tag = "TutPlate";
                    TutorialManager.Instance.tutPlates.Add(currentcube);
                }
            }
            spawnPoint.position += new Vector3(-3 * gridSize.y, 0, -3);
        }

    }

    private void CameraSet()
    {
        // Vector2 middlePoint = new Vector2((3 * ((float)gridSize.y - 1))/2, (-3 * ((float)gridSize.x - 1))/2);    //Mobile Setting
        Vector2 middlePoint = new Vector2((3 * ((float)gridSize.y - 1))/2, (-3 * ((float)gridSize.x))/2);       //PC Setting
        CameraManager.Instance.SetCameraFocus(middlePoint, gridSize.y - 3);
    }

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\\

     public List<BurgerObject> match_H;
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


    public List<BurgerObject> bombedObjects;
    private bool splItemUsed;

    private void BombCheck(int x, int y)
    {
        if(gridFormed[x, y].burgerPart != BurgerPart.bomb) return;

        splItemUsed = true;

        if(x + 1 < gridSize.x && gridFormed[x + 1, y] != null)
            bombedObjects.Add(gridFormed[x + 1, y]);
        if(x - 1 >= 0 && gridFormed[x - 1, y] != null)
            bombedObjects.Add(gridFormed[x - 1, y]);

        if(y + 1 < gridSize.y && gridFormed[x, y + 1] != null)
            bombedObjects.Add(gridFormed[x, y + 1]);
        if(y - 1 >= 0 && gridFormed[x, y - 1] != null)
            bombedObjects.Add(gridFormed[x, y - 1]);

        StartCoroutine(Blast(x, y));
    }

    private IEnumerator Blast(int x, int y)
    {
        yield return new WaitForSeconds(.2f);

        gridCount -= bombedObjects.Count;
        foreach (var item in bombedObjects)
        {
            Destroy(item.gameObject);
            GameManager.Instance._ParticleSystemData.PlayFXs(item.transform.position, Vector3.zero, 0, Vector3.one);
        }

        GameManager.Instance._ParticleSystemData.PlayFXs(gridFormed[x,y].transform.position, Vector3.zero, 0, Vector3.one);
        Destroy(gridFormed[x, y].gameObject);
        gridCount--;

        bombedObjects.Clear();
        splItemUsed = false;
    }


    GameObject iconPos;
    bool mat_H, mat_V;  // Using for double match. Extra adding of grid count
    private void DestroyMatch()
    {
        mat_H = mat_V = false;

        if(match_H.Count >= 3)
        {
            // OrderManager.Instance.CheckMatch(match_H[0].burgerPart);
            // orderMatched = OrderManager.Instance.CheckMatch(match_H[0].burgerPart);
            iconPos = OrderManager.Instance.CheckMatch(match_H[0].burgerPart);

            if(iconPos == null)
                StartCoroutine(AnimateMatchMade(match_H));
            else
                StartCoroutine(AnimMatches(match_H, iconPos));
    
            gridCount -= match_H.Count;
            mat_H = true;
            IncrementScore?.Invoke(100 * match_H.Count);
        }
        else
            match_H.Clear();

        if(match_V.Count >= 3)
        {
            // OrderManager.Instance.CheckMatch(match_V[0].burgerPart);
            // orderMatched = OrderManager.Instance.CheckMatch(match_V[0].burgerPart);
            iconPos = OrderManager.Instance.CheckMatch(match_V[0].burgerPart);

            if(iconPos == null)
                StartCoroutine(AnimateMatchMade(match_V));
            else
                StartCoroutine(AnimMatches(match_V, iconPos));
    
            gridCount -= match_V.Count;
            mat_V = true;
            IncrementScore?.Invoke(100 * match_V.Count);
        }
        else
            match_V.Clear();

        gridCount++;

        if(mat_H && mat_V) gridCount++;

        if (gridCount == gridSize.x * gridSize.y)
        {
            if(splItemUsed) return;

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

    IEnumerator AnimMatches(List<BurgerObject> tweenObjects, GameObject tweenPos)
    {
        // Debug.LogError(tweenPos);
        GameManager.startPlay = false;
        // yield return new WaitForSeconds(0.3f);
        
        // foreach (var item in tweenObjects)
        // {
        //     //Tween together)
        // }

        yield return new WaitForSeconds(.3f);

        foreach (var item in tweenObjects)
        {
            if(item == null) continue;

            item.transform.DOScale(Vector3.one * .4f, .7f);
            item.transform.DOMove(tweenPos.transform.position, .7f).OnComplete(() =>
            {
                DOTween.KillAll();
                tweenPos.transform.DOPunchScale(Vector3.one * 1.2f, 0.4f, 2, 0.5f);
                OrderManager.Instance.UpdateDisplayCount();
            });
            //Tween to tweenPos
        }
        
        //Punch icon//
        yield return new WaitForSeconds(.7f);

        foreach (var item in tweenObjects)
        {
            if(item == null) continue;

            Destroy(item.gameObject);
        }

        tweenObjects.Clear();
        GameManager.startPlay = true;
    }

}