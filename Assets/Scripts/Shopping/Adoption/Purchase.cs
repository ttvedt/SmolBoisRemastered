using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using boiClass;
using randomizedBoi;

public class Purchase : MonoBehaviour
{
    private GameManager gameManager;
    public smolBoi thisboi;
    public int price;
    public GameObject profile;
    public GameObject boiBody;
    public boiScript bs;
    public Camera myCamera;
    public RandomizeBoi rb;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        thisboi = new smolBoi();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CastRay();
        }
    }

    void CastRay()
    {
       Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null && hit.collider.tag == "boi")
        {
            try
            {
                thisboi = hit.collider.gameObject.GetComponent<RandomizeBoi>().thisboi;
                price = hit.collider.gameObject.GetComponent<RandomizeBoi>().price;
                boiBody = hit.collider.gameObject;
                bs = hit.collider.gameObject.GetComponent<boiScript>();
                rb = hit.collider.gameObject.GetComponent<RandomizeBoi>();
            }
            catch { }
        }
        
    }

    public void purchaseBoi()
    {
        float pop = gameManager.getPopulation();
        float mpop = gameManager.getMaxPopulation();
        if (BoiBucks.boiBucks >= price && pop/mpop < 1f)
        {
            bs.setBoi(thisboi.getName(), thisboi.getSpecies(), thisboi.getGenes()[0], thisboi.getGenes()[1]);
            BoughtBois.boughtBoi[BoughtBois.numPurchased] = bs.thisBoi;      
            BoiBucks.boiBucks -= price;
            BoiBucks.updateBoiBucks();
            profile.SetActive(false);
            boiBody.SetActive(false);
            rb.off = true;
            //Destroy(boiBody);
            BoughtBois.numPurchased++;
            Debug.Log("Purchase.cs purchaseBoi(): bought " + BoughtBois.numPurchased);
            
        }
        else
        {
            Debug.Log("Purchase.cs purchaseBoi(): cannot purchase boi.");
        }
    }
}

