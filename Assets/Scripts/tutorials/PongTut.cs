using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongTut : MonoBehaviour
{
    public GameObject zero_;
    public GameObject one_;
    public GameObject two_;
    public GameObject welcome;
    public GameObject controls;
    public GameObject revisit;
    public GameObject overall;
    public GameObject ball;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void one()
    {
        zero_.SetActive(false);
        one_.SetActive(true);
        welcome.SetActive(false);
        controls.SetActive(true);
    }

    public void two()
    {
        one_.SetActive(false);
        two_.SetActive(true);
        controls.SetActive(false);
        revisit.SetActive(true);
    }

    public void three()
    {
        two_.SetActive(false);
        revisit.SetActive(false);
        overall.SetActive(false);
        ball.SetActive(true);
    }

    public void again()
    {
        overall.SetActive(true);
        zero_.SetActive(true);
        welcome.SetActive(true);
        ball.SetActive(false);
    }
}
