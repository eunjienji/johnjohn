using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 movement;
    private Animator animator;
    private Rigidbody rb;
    private Quaternion rotation = Quaternion.identity;

    [HeaderAttribute("회전속도")]
    public float turnSpeed = 20f;


   void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        movement.Set(horizontal, 0f, vertical);

        movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        animator.SetBool("isWalk",isWalking);

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movement,turnSpeed* Time.fixedDeltaTime, 0f);
        rotation = Quaternion.LookRotation(desiredForward);
    }

    void OnAnimatorMove()
    {

        rb.MovePosition(rb.position + movement * animator.deltaPosition.magnitude);
        rb.MoveRotation(rotation);
    }
}