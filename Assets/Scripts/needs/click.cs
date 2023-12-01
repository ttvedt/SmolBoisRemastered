using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class click : MonoBehaviour
{
    public EmptyBowl emptyBowlScript;
    public ReplaceBowl replaceBowlScript;
    void Start()
    {
        emptyBowlScript = GameObject.FindGameObjectWithTag("EmptyBowl").GetComponent<EmptyBowl>();
        replaceBowlScript = GameObject.FindGameObjectWithTag("bowlSpawnPoint").GetComponent<ReplaceBowl>();
    }

    public void OnButtonPress()
    {
        if(GameObject.FindGameObjectWithTag("EmptyBowl") != null)
        {
            Debug.Log("click.cs OnButtonPress(): spawning full bowl, spent 50 boi bucks");
            replaceBowlScript.spawnFullBowl();
            BoiBucks.boiBucks -= 50;
            BoiBucks.updateBoiBucks();
        }
    }
}
