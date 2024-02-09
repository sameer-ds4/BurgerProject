using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
	[SerializeField] private GameObject point;
	[SerializeField] private GameObject cellIndicator;
	[SerializeField] private PlaceInput placeInput;
	[SerializeField] private Grid grid;
	public GameObject fdkjfnb;
	
	// Update is called once per frame
	void Update()
	{
		Vector3 mousePosition = placeInput.GetSlotPosition();
		Vector3Int gridPos = grid.WorldToCell(mousePosition);
		point.transform.position = mousePosition;
		cellIndicator.transform.position = grid.CellToWorld(gridPos);
		
		if(Input.GetMouseButtonDown(0))
		{
			GameObject mm = Instantiate(fdkjfnb);
			mm.transform.position = grid.CellToWorld(gridPos);
		}
	} 
}
