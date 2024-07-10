using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController3D : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Animator animator;


    private Vector3 direction;
    private bool isJumping;

    private void Start() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;    
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        direction = mainCam.transform.forward * vertical + mainCam.transform.right * horizontal;
    }

    private void FixedUpdate() 
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        rb.velocity = direction * walkSpeed * Time.deltaTime;

        //Use velocity to detect motion and trigger aniamtions. 
        // if(rb.velocity.magnitude > 0)
        // {
        //     animator.SetTrigger("walk");
        // }
        // else
        // {
        //     animator.SetTrigger("Idle");
        // }
    }

    private void JumpPlayer()
    {
        Debug.LogError("sdlkfvbsdf");
        rb.AddForce(new Vector3(0, jumpForce, 0) * Time.deltaTime);
        isJumping = true;
    }

    private void SwitchAnimation(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }
}
