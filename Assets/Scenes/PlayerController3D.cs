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


    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        direction = transform.forward * vertical + transform.right * horizontal;

        if(Input.GetKeyDown(KeyCode.Space) && !isJumping)
            JumpPlayer();

    }

    private void FixedUpdate() 
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        rb.velocity = direction * walkSpeed * Time.deltaTime;
    }

    private void JumpPlayer()
    {
        rb.AddForce(new Vector3(0, jumpForce, 0) * Time.deltaTime);
        isJumping = true;
    }

    private void SwitchAnimation(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }
}
