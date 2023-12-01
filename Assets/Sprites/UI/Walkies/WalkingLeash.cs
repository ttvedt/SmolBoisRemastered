using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingLeash : MonoBehaviour
{
    private Animator animator; // the Animator component of the parent object
    private Animator animatorparent; // the Transform component of the child object
    public GameObject WalkiesBackground;
//public GameObject WalkiesLeash;

// Start is called before the first frame update
void Start()
{
        WalkiesBackground = GameObject.Find("WalkiesBackground");
        transform.SetParent(WalkiesBackground.transform);
        animatorparent = transform.parent.GetComponent<Animator>();
        animator = GetComponent<Animator>();
}

// Update is called once per frame
void Update()
{
    // match the position and rotation of the child object to the
    // position and rotation of the parent object's animation
    animator.speed = animatorparent.speed;
}  
}
