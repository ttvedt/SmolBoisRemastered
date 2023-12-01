using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boiAnimate : MonoBehaviour
{
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveY", -1);
        animator.SetBool("waddle", true);
    }
    void FixedUpdate()
    {
        animator.SetFloat("moveY", -1);
        animator.SetBool("waddle", true);
        UpdateAnimation();
    }
    void UpdateAnimation()
    {
        animator.SetFloat("moveY", -1);
        animator.SetBool("waddle", true);
        if (change != Vector3.zero){
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("waddle", true);
        }
        else{
            animator.SetBool("waddle", false);
        }
    }
}
