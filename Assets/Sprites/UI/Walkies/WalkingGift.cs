using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingGift : MonoBehaviour
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
        Debug.Log(this.gameObject.name + " Parent animator " + animatorparent);
    }

    // Update is called once per frame
    void Update()
    {
        // match the position and rotation of the child object to the
        // position and rotation of the parent object's animation
        animator.speed = animatorparent.speed;
    }

    //Destroys on Collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Destroyed Loot");
        BoiBucks.boiBucks += 500;
        BoiBucks.updateBoiBucks();
        Destroy(gameObject);
        
    }
}
