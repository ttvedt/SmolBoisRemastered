using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{
    public int itemID;
    public Text priceTXT;
    public int purchased;
    public int colorID;
    public int price;
    public int buyable;
    //public GameObject ShopManager;

    void Update()
    {
       
        //purchased = ShopManager.items[3, itemID];
        //Debug.Log(ShopManager.GetComponent<ShopManager>().items[3, itemID]);
        if (purchased == 0)
        {
            priceTXT.text = "Price: $" + price.ToString();
        }
        if (purchased >= 1 && FurnitureStorage.storedItems[itemID, colorID] == 1)
        {
            priceTXT.text = "Equipped";
        }
        if (purchased >= 1 && FurnitureStorage.storedItems[itemID, colorID] != 1)
        {
            priceTXT.text = "Purchased";
        }
        if (buyable > TileExpansions.Level)
        {
            priceTXT.text = "Expansion Required";
        }
    }
    
}
