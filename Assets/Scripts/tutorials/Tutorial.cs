using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour

{
    public GameObject zero_;
    public GameObject one_;
    public GameObject two_;
    public GameObject three_;
    public GameObject four_;
    public GameObject five_;
    public GameObject six_;
    public GameObject eight_;
    public GameObject nine_;
    public GameObject ten_;
    public GameObject eleven_;
    public GameObject thirteen_;
    public GameObject fourteen_;
    public GameObject fifteen_;
    public GameObject welcome;
    public GameObject aveNeeds;
    public GameObject profile;
    public GameObject important;
    public GameObject breeding;
    public GameObject food1;
    public GameObject food2;
    public GameObject water;
    public GameObject entertain;
    public GameObject furniture;
    public GameObject change;
    public GameObject set;
    public GameObject adopt;
    public GameObject repeat;
    public GameObject overall;
    public GameObject exit;

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
        aveNeeds.SetActive(true);
    }
    public void two()
    {
        one_.SetActive(false);
        two_.SetActive(true);
        aveNeeds.SetActive(false);
        profile.SetActive(true);
    }

    public void three()
    {
        two_.SetActive(false);
        three_.SetActive(true);
        profile.SetActive(false);
        important.SetActive(true);
    }

    public void four()
    {
        three_.SetActive(false);
        four_.SetActive(true);
        important.SetActive(false);
        breeding.SetActive(true);
    }

    public void five()
    {
        four_.SetActive(false);
        five_.SetActive(true);
        breeding.SetActive(false);
        food1.SetActive(true);
    }

    public void six()
    {
        five_.SetActive(false);
        six_.SetActive(true);
        food1.SetActive(false);
        food2.SetActive(true);
    }

    public void seven()
    {
        six_.SetActive(false);
        eight_.SetActive(true);
        food2.SetActive(false);
        water.SetActive(true);
    }

    public void nine()
    {
        eight_.SetActive(false);
        nine_.SetActive(true);
        water.SetActive(false);
        entertain.SetActive(true);
    }

    public void ten()
    {
        nine_.SetActive(false);
        ten_.SetActive(true);
        entertain.SetActive(false);
        furniture.SetActive(true);
    }

    public void eleven()
    {
        ten_.SetActive(false);
        eleven_.SetActive(true);
        furniture.SetActive(false);
        change.SetActive(true);
    }

    public void twelve()
    {
        eleven_.SetActive(false);
        thirteen_.SetActive(true);
        change.SetActive(false);
        set.SetActive(true);
    }

    public void fourteen()
    {
        thirteen_.SetActive(false);
        fourteen_.SetActive(true);
        set.SetActive(false);
        adopt.SetActive(true);
    }

    public void fifteen()
    {
        fourteen_.SetActive(false);
        fifteen_.SetActive(true);
        adopt.SetActive(false);
        repeat.SetActive(true);
    }

    public void sixteen()
    {
        fifteen_.SetActive(false);
        repeat.SetActive(false);
        overall.SetActive(false);
    }

    public void again()
    {
        overall.SetActive(true);
        zero_.SetActive(true);
        welcome.SetActive(true);
    }    
}
