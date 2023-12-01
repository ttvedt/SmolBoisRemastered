using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyBowl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("EmptyBowl.cs Start(): empty bowl created");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fillBowl()
    {
        Debug.Log("EmptyBowl.cs fillBowl(): empty bowl destroyed");
        Destroy(this.gameObject);
    }
}
