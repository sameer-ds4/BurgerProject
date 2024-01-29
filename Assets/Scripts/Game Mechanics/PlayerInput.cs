using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // public BurgerObject[] burgerPart;
    public Camera mainCam;

    public delegate void Matchmaking(int x, int y);
    public static event Matchmaking CheckMatch;

    public delegate void RemoveMatch();
    public static event RemoveMatch MatchRemoval;

    public delegate void RandomizeComps();
    public static event RandomizeComps RandomBurger;

    private void Update() 
    {
        if (!GameManager.startPlay) return;

        if(Input.GetMouseButtonDown(0))
        {
            CheckPlate();
            // Debug.LogError(GridManager.gridCount);
        }    
    }

    private void CheckPlate()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100))
        {
            if(hit.collider.tag == "Plate")
            {
                Vector3 spawnPos = hit.collider.transform.position + new Vector3(0, 0.5f, 0);
                string place = hit.collider.gameObject.name;
                PlacePicks(spawnPos, place);
            }
        }
    }

    private void PlacePicks(Vector3 position, string name)
    {
        // Debug.LogError(name);
        char[] place = name.ToCharArray();
        if(GridManager.gridFormed[place[0] - '0', place[1] - '0'] == null)
        {
            BurgerObject burgerSpawned = Instantiate(GameManager.Instance.burgerItemsList[0].burgerObject, position, Quaternion.identity, GameManager.Instance.foodParent);
            // BurgerObject plateFormed = Instantiate(burgerPart[Random.Range(0, burgerPart.Length)], position, Quaternion.identity);
            GridManager.gridFormed[place[0] - '0', place[1] - '0'] = burgerSpawned;
            CheckMatch?.Invoke(place[0] - '0', place[1] - '0');
            MatchRemoval?.Invoke();
            RandomBurger?.Invoke();
        }
    }
}