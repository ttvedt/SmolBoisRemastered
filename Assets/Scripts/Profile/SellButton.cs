using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject boiBody;
    public boiScript thisboi;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CastRay();
        }
    }

    void CastRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null && hit.collider.tag == "boi")
        {
            boiBody = hit.collider.gameObject;
            thisboi = boiBody.GetComponent<boiScript>();
        }
    }

    public void sell()
    {
        if (thisboi.thisBoi.getGenes()[0] == thisboi.thisBoi.getGenes()[1])
        {
            BoiBucks.boiBucks -= 200;
            Destroy(boiBody);
        }
        else
        {
            BoiBucks.boiBucks -= 500;
            Destroy(boiBody);
        }
    }
}
