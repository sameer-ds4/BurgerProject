using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCafe
{
	public Dictionary<Vector3Int, PlacementData> placedObjects = new();
	
	public void AddObjectAt(Vector3Int gridPos, Vector2Int objSize, int iD, int placementIndex)
	{
		List<Vector3Int> positionToOccupy = CalculatePositions(gridPos, objSize);
		PlacementData data = new PlacementData(positionToOccupy, iD, placementIndex);
	}
	
	private List<Vector3Int> CalculatePositions(Vector3Int gridPos, Vector2Int size)
	{
		return null;
	}
}


public class PlacementData
{
	public List<Vector3Int> OccupiedPositions;
	public int Id { get; private set;}
	public int PlacedObjectIndex { get; private set; }
	
	public PlacementData(List<Vector3Int> occupiedPositions,int iD, int placedObjectIndex)
	{
		this.OccupiedPositions = occupiedPositions;
		Id = iD;
		PlacedObjectIndex = placedObjectIndex;
	}
}
