using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceInput : MonoBehaviour
{
	[SerializeField] private Camera mainCam;
	public Vector3 lastPosition;
	[SerializeField] private LayerMask layerMask;
	
	
	public Vector3 GetSlotPosition()
	{
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = mainCam.nearClipPlane;
		

		Ray ray = mainCam.ScreenPointToRay(mousePosition);

		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 1000, layerMask))
			lastPosition = hit.point;

		return lastPosition;
	}
} 