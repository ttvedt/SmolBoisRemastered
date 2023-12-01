using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBowl : MonoBehaviour
{
    public static float FoodSource;
    private Animator animator;
    private boiScript boiS;

    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();
        FoodSource += 50000;
        animator.SetFloat("FoodSource", FoodSource);
        //public GameObject boiScript;
        //Public GameObject boiScript;
        GameObject.FindGameObjectWithTag("Music").GetComponent<KeepMusicPlaying>().PlayMusic();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        boiS = other.GetComponent<boiScript>();
        if (FoodSource > 9999)
        { FoodSource -= 10000;
          animator.SetFloat("FoodSource", FoodSource);
          Debug.Log("FoodBowl.cs OnCollisionEnter2D(): FoodSource > 9999");
        }
    }

    public void OnButtonPress()
    {
        if (FoodSource <= 1)
        {
            Debug.Log("FoodBowl.cs OnButtonPress(): spawning full bowl, spent 50 boi bucks");
            FoodSource += 50000;
            animator.SetFloat("FoodSource", FoodSource);
            BoiBucks.boiBucks -= 25;
            BoiBucks.updateBoiBucks();
        }
    }
}