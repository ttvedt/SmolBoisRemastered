using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour
{

    public static int[,] items = new int[5, 16];
    public int thisID;
    public int colorID;
    public ButtonInfo ButtonRef;
    public AudioSource audioSource;
    public Camera myCamera;
    // Start is called before the first frame update
    void Start()
    {
        //IDs
        items[1, 1] = 1;
        items[1, 2] = 2;
        items[1, 3] = 3;
        items[1, 4] = 4;
        items[1, 5] = 5;
        items[1, 6] = 6;
        items[1, 7] = 7;
        items[1, 8] = 8;
        items[1, 9] = 9;
        items[1, 10] = 10;
        items[1, 11] = 11;
        items[1, 12] = 12;
        items[1, 13] = 13;
        items[1, 14] = 14;
        items[1, 15] = 15;

        //prices
        items[2, 1] = 200;
        items[2, 2] = 200;
        items[2, 3] = 300;
        items[2, 4] = 400;
        items[2, 5] = 600;
        items[2, 6] = 500;

        //purchased bool
        items[3, 1] = 0;
        items[3, 2] = 0;
        items[3, 3] = 0;
        items[3, 4] = 0;
        items[3, 5] = 0;
        items[3, 6] = 0;

        //colors
        items[4, 1] = 0;
        items[4, 2] = 0;
        items[4, 3] = 0;
        items[4, 4] = 0;
        items[4, 5] = 0;
        items[4, 6] = 0;
        items[4, 7] = 0;
        items[4, 8] = 0;
        items[4, 9] = 0;
        items[4, 10] = 0;
        items[4, 11] = 0;
        items[4, 12] = 0;
        items[4, 13] = 0;
        items[4, 14] = 0;
        items[4, 15] = 0;
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
        Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null && hit.collider.tag == "Buy")
        {
            ButtonRef = hit.collider.gameObject.GetComponent<ButtonInfo>();
            thisID = hit.collider.gameObject.GetComponent<ButtonInfo>().itemID;
            colorID = hit.collider.gameObject.GetComponent<ButtonInfo>().colorID;
        }

    }

    public void Buy()
    {
        if (BoiBucks.boiBucks >= ButtonRef.price && ButtonRef.buyable <= TileExpansions.Level)
        {
            audioSource.Play();
            BoiBucks.boiBucks -= ButtonRef.price;
            ButtonRef.purchased++;
            ButtonRef.price = 0;
            items[4, thisID] = colorID;
            for(int i = 0; i < 10; ++i)
            {
                FurnitureStorage.storedItems[thisID, i] = 0;
            }
            FurnitureStorage.storedItems[thisID, colorID] = 1;
            Debug.Log("[" + thisID + ", " + colorID + "]");           
        }
    }
}
