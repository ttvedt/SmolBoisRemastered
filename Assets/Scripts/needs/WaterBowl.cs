using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBowl : MonoBehaviour
{
    public static float WaterSource;
    private Animator animator;
    private boiScript boiS;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        WaterSource += 50000;
        animator.SetFloat("WaterSource", WaterSource);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        boiS = other.GetComponent<boiScript>();
        //Debug.Log("WaterBowl.cs OnCollisionEnter2D()");
        if(WaterSource > 9999)
        {
            WaterSource -= 10000;
            animator.SetFloat("WaterSource", WaterSource);
            Debug.Log("WaterBowl.cs OnCollisionEnter2D(): WaterSource > 9999");
            boiS.thirst();
        }
    }

    public void OnButtonPress()
    {
        if (WaterSource <= 1)
        {
            Debug.Log("WaterBowl.cs OnButtonPress(): spawning full bowl, spent 50 boi bucks");
            WaterSource += 50000;
            animator.SetFloat("WaterSource", WaterSource);
            BoiBucks.boiBucks -= 25;
            BoiBucks.updateBoiBucks();
        }
    }
}