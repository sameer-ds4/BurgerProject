using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
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
		if(Physics.Raycast(ray, out hit, 100))                  // Checking for Slot occupied or not
		{
			if(hit.collider.tag == "Plate")
			{
				Vector3 spawnPos = hit.collider.transform.position + new Vector3(0, 0.5f, 0);
				string place = hit.collider.gameObject.name;
				PlacePicks(spawnPos, place);
			}
		}
	}

	private void PlacePicks(Vector3 position, string name)          // Passing position and index of the slot.  
	// 2D Index passed as string. "02" for 1st row 3rd column
	{
		char[] place = name.ToCharArray();
		if(GridManager.gridFormed[place[0] - '0', place[1] - '0'] == null)    // Taking index from character array, so adding "- '0'" to reformat ASCII.
		{
			// Getting the first burger from array. instantiate current burger
			BurgerObject burgerSpawned = Instantiate(GameManager.Instance.burgerItemsList[0].burgerObject, position, Quaternion.identity, GameManager.Instance.foodParent);
			GridManager.gridFormed[place[0] - '0', place[1] - '0'] = burgerSpawned;
			CheckMatch?.Invoke(place[0] - '0', place[1] - '0');							// Event for starting matchmaking
			MatchRemoval?.Invoke();														// Event for destroying matches, if any
			RandomBurger?.Invoke();														// Event for generating next burger component
		}
	}
}