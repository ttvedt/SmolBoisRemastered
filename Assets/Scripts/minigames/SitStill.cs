using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitStill : MonoBehaviour
{
    public GameObject me;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        me.transform.position = new Vector3(-81.13f, -50.43f, -1089.775f);
    }
}
