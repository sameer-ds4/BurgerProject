using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera m_Camera;
    [SerializeField] private Vector3 playerOffset;
    [SerializeField] private float dampingFactor;
    [SerializeField] private Transform targetObject;

    void FixedUpdate()
    {
        Vector3 desiredPosition = new Vector3(0, targetObject.position.y + playerOffset.y, playerOffset.z);
		// Vector3 smoothedPosition = Vector3.Slerp(transform.position, desiredPosition, dampingFactor);
		transform.position = desiredPosition;

        Debug.LogError(desiredPosition);
    }
}
