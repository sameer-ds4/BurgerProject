using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public ColorPick[] picks;
    public Camera mainCam;

    public delegate void Matchmaking(int x, int y);
    public static event Matchmaking CheckMatch;

    public delegate void RemoveMatch();
    public static event RemoveMatch MatchRemoval;

    private void Update() 
    {
        if(Input.GetMouseButtonDown(0))
        {
            CheckPlate();
        }    
    }

    private void CheckPlate()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 5))
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
        char[] place = name.ToCharArray();
        if(GridManager2.gridFormed[place[0] - '0', place[1] - '0'] == null)
        {
            ColorPick plateFormed = Instantiate(picks[Random.Range(0, picks.Length)], position, Quaternion.identity);
            GridManager2.gridFormed[place[0] - '0', place[1] - '0'] = plateFormed;
            CheckMatch?.Invoke(place[0] - '0', place[1] - '0');
            MatchRemoval?.Invoke();
        }
    }
}