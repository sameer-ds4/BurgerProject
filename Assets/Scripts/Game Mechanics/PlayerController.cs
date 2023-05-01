using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Positioning Components")]
    private Vector3 pos;
    private Transform foodObjects;

    [Header("Slot Detail")]
    public float slotIndex;

    private void Start()
    {
        foodObjects = GameManager.Instance.foodParent;
        slotIndex = Mathf.Pow(GridManager.Instance.gridSize, 2);
    }

    private void Update()
    {
        OnMousebuttDown();
    }

    private void OnMousebuttDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(pos);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                PlaceBurgerComponent(hit);
            }
        }
    }

    void PlaceBurgerComponent(RaycastHit hit)
    {
        for (int i = 0; i < slotIndex; i++)
        {
            if (hit.collider.gameObject == GridManager.Instance.blocks[i].block && !GridManager.Instance.blocks[i].status)
            {
                BurgerRandomizer.Instance.Randomize();
                GameObject poc = BurgerRandomizer.Instance.currentCompenent;
                Instantiate(poc, hit.transform.position + new Vector3(0, 0.6f, 0), Quaternion.identity, foodObjects);
                GridManager.Instance.blocks[i].foodComp = poc;
                GridManager.Instance.blocks[i].status = true;
            }
        }

        //GridManager.Instance.CheckMatches();
    }
}
