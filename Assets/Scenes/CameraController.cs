using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Vector3 playerOffset;
    [SerializeField] private Transform targetObject;

    float horInput;
    float vertInput;


    private void Update() 
    {
        horInput += Input.GetAxis("Mouse X");
        vertInput += Input.GetAxis("Mouse Y");

        var targetRotation = Quaternion.Euler(vertInput, horInput, 0);

        transform.position = targetObject.position - targetRotation * playerOffset;
        transform.rotation = targetRotation;
    }
}