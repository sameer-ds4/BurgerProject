using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    [Header("Positioning Components")]
    private Vector3 pos;
    private Transform foodObjects;

    [Header("Slot Detail")]
    public float slotIndex;
    
    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foodObjects = GameManager.Instance.foodParent;
        // slotIndex = Mathf.Pow(GridManager.Instance.gridSize, 2);
    }

    // private void Update()
    // {
    //     OnMousebuttDown();
    // }

    // private void OnMousebuttDown()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         pos = Input.mousePosition;
    //         Ray ray = Camera.main.ScreenPointToRay(pos);

    //         RaycastHit hit;
    //         if (Physics.Raycast(ray, out hit))
    //         {
    //             // PlaceBurgerComponent(hit);
    //         }
    //     }
    // }

    // public BurgerObject PlaceBurgerComponent(Transform target)
    // {
    //     BurgerRandomizer.Instance.Randomize();
    //     BurgerObject poc = BurgerRandomizer.Instance.currentCompenent;
    //     Instantiate(poc, target.position, Quaternion.identity);
    //     return poc;   
    // }

    // void PlaceBurgerComponent(RaycastHit hit)
    // {
    //     // for (int i = 0; i < slotIndex; i++)
    //     // {
    //     //     if (hit.collider == GridManager.Instance.cubes[i] && !GridManager.Instance.cubes[i].cube.status)
    //     //     {
    //     //         BurgerRandomizer.Instance.Randomize();
    //     //         BurgerObject poc = BurgerRandomizer.Instance.currentCompenent;
    //     //         Instantiate(poc, hit.transform.position + new Vector3(0, 0.6f, 0), Quaternion.identity, foodObjects);
    //     //         GridManager.Instance.cubes[i].cube.burgerpart = poc;
    //     //         GridManager.Instance.cubes[i].cube.status = true;
    //     //     }
    //     // }

    //     // GridManager.Instance.CheckMatches();
    // }
}
