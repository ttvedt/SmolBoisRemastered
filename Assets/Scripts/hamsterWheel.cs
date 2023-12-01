using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hamsterWheel : MonoBehaviour
{
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private static Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    public static void begin()
    {
        animator.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
