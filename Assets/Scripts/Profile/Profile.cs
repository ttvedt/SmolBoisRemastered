using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using boiClass;

public class Profile : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject profile;
    public GameObject foodBar;
    public GameObject exitGameBar;
    public GameObject pachinko;
    public GameObject pong;
    public GameObject other;
    public Text boiName;
    public Text food;
    public Text water;
    public Text entertainment;
    public boiScript thisboi;
    public Font custom;
    public Image img;
    public Sprite newSprite;
    public boiScript profileBoi;
    public GameObject boiBody;
    private int boiValue = 500;
    private int boiMaxValue = 500;
    private int boiMinValue = 50;
    private int roundPenalty = 200;
    private int solidPenalty = 300;
    private float boiFood = 0;
    private float boiWater = 0;
    private float boiEntertainment = 0;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //Debug.Log("Profile.cs Start(): running from " + this.gameObject.name);
        if (profile == null)
        {//grab profile
            Debug.LogWarning("Profile.cs Start(): null profile");
            profile = this.gameObject;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CastRay();
        }
    }

    public GameObject getBoi() { return boiBody; }

    void CastRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null && hit.collider.tag == "boi" && hit.collider.tag != "Popup")
        {
            if (profile == null)
            {
                Debug.LogError("Profile.cs CastRay(): null profile");
                return;
            }
            profile.SetActive(true);
            boiBody = hit.collider.gameObject;
            thisboi = hit.collider.gameObject.GetComponent<boiScript>();
            boiName.text = (thisboi.thisBoi.getName());
            boiFood = thisboi.thisBoi.getFood() / 1000;
            boiWater = thisboi.thisBoi.getWater() / 1000;
            boiEntertainment = thisboi.thisBoi.getEntertainment() / 1000;
            string[] temp = food.text.Split(":");
            food.text = temp[0] + ":" + Mathf.Round(boiFood);
            temp = water.text.Split(":");
            water.text = temp[0] + ":" + Mathf.Round(boiWater);
            temp = entertainment.text.Split(":");
            entertainment.text = temp[0] + ":" + Mathf.Round(boiEntertainment);
            //newSprite = hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite;
            //changeSprite();
            profileBoi.setBoi(thisboi.thisBoi.getName(), thisboi.thisBoi.getSpecies(), thisboi.thisBoi.getGenes()[0], thisboi.thisBoi.getGenes()[1]);
            BoughtBois.boughtBoi[0] = thisboi.thisBoi;
        }
    }

    public void close()
    {
        profile.SetActive(false);
    }

    public void showGames()
    {
        foodBar.SetActive(false);
        exitGameBar.SetActive(true);
        pachinko.SetActive(true);
        pong.SetActive(true);
        other.SetActive(true);
    }

    public void hideGames()
    {
        foodBar.SetActive(true);
        exitGameBar.SetActive(false);
        pachinko.SetActive(false);
        pong.SetActive(false);
        other.SetActive(false);
    }

    public void sell()
    {
        boiValue = boiMaxValue;
        // round bois have less value
        if (thisboi.thisBoi.getSpecies() == species.Round) { boiValue -= roundPenalty; }
        // solid color bois have less value
        if (thisboi.thisBoi.getGenes()[0] == thisboi.thisBoi.getGenes()[1]) { boiValue -= solidPenalty; }
        // multiply value by needs to reduce value based on how empty the needs are.          / 100*100*100
        if (boiValue > 0) { boiValue = (int)(boiValue * boiFood * boiWater * boiEntertainment / 1000000); }
        // (if value is negative, don't multiply by needs) to get multipliers between 0 and 1, divide each need by 100
        boiValue += boiMinValue;// add on minimum value to make bois with almost full needs still be worth full value
        // value cannot go above maximum
        if (boiValue > boiMaxValue) { boiValue = boiMaxValue; }
        // reduce population by 1
        gameManager.populationSubtractOne();
        int pop = gameManager.getPopulation();
        int mpop = gameManager.getMaxPopulation();
        // print what the boi's needs were at
        thisboi.thisBoi.printNeeds();
        // print transaction receipt and new current population
        Debug.Log("Profile.cs sell(): sold " + thisboi.getMe() + " for " + boiValue + ". population now at " + 100 * pop / mpop + "% capacity: " + pop + "/" + mpop);
        // add sale value to wallet
        BoiBucks.boiBucks += boiValue;
        BoiBucks.updateBoiBucks();
        Destroy(boiBody);// remove the boi from the game
        profile.SetActive(false);// close the profile of the sold boi
    }
}
