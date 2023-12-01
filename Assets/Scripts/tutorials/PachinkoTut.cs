using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PachinkoTut : MonoBehaviour
{
    public GameObject zero_;
    public GameObject one_;
    public GameObject two_;
    public GameObject three_;
    public GameObject overall;
    public GameObject welcome;
    public GameObject space;
    public GameObject cost;
    public GameObject repeat;
    public GameObject poor;

    void Start()
    {
        if (BoiBucks.boiBucks < 200)
            poor.SetActive(true);
    }

    public void one()
    {
        zero_.SetActive(false);
        one_.SetActive(true);
        welcome.SetActive(false);
        space.SetActive(true);
    }

    public void two()
    {
        one_.SetActive(false);
        two_.SetActive(true);
        space.SetActive(false);
        cost.SetActive(true);
    }

    public void three()
    {
        two_.SetActive(false);
        three_.SetActive(true);
        cost.SetActive(false);
        repeat.SetActive(true);
    }

    public void four()
    {
        three_.SetActive(false);
        repeat.SetActive(false);
        overall.SetActive(false);
    }

    public void again()
    {
        overall.SetActive(true);
        welcome.SetActive(true);
        zero_.SetActive(true);
    }
}
