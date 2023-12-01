using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceBowl : MonoBehaviour
{
    public GameObject fullBowlPrefab;
    public GameObject halfFullPrefab;
    public GameObject emptyBowlPrefab;
    private GameObject spawnedBowl;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnFullBowl()
    {
        Debug.Log("ReplaceBowl.cs spawnFullBowl(): destroying empty bowl");
        Destroy(GameObject.FindWithTag("EmptyBowl"));
        spawnedBowl = Instantiate(fullBowlPrefab);
    }

    public void spawnEmptyBowl()
    {
        if(GameObject.FindGameObjectWithTag("FullBowl") != null)
        {
            Debug.Log("ReplaceBowl.cs spawnEmptyBowl(): destroying full bowl");
            Destroy(GameObject.FindWithTag("FullBowl"));
            spawnedBowl = Instantiate(halfFullPrefab);
        }
        else
        {
            Debug.Log("ReplaceBowl.cs spawnEmptyBowl(): destroying half full bowl");
            Destroy(GameObject.FindWithTag("HalfFull"));
            spawnedBowl = Instantiate(emptyBowlPrefab);
        }
    }
}
