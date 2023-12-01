using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class overlapFix : MonoBehaviour
{
    static int overlap;
    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        if (overlap == 1)
            source.Stop();
        else
            source.Play();
        overlap = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
