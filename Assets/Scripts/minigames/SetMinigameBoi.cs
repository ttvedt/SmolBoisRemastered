using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using boiClass;

public class SetMinigameBoi : MonoBehaviour
{
    public boiScript miniboi;
    public Text boiName;
    public static int Name;
    private bool nullCheck = true;
    // Start is called before the first frame update
    void Start()
    {
        
        //Debug.Log(miniboi.thisBoi.ge)
    }

    // Update is called once per frame
    void Update()
    {
        if (BoughtBois.boughtBoi[0] != null)
        {
            if (miniboi != null){
                miniboi.setBoi(BoughtBois.boughtBoi[0].getName(), BoughtBois.boughtBoi[0].getSpecies(), BoughtBois.boughtBoi[0].getGenes()[0], BoughtBois.boughtBoi[0].getGenes()[1]);
            }else { if (nullCheck) { nullCheck = false; Debug.LogError("SetMinigameBoi.cs Update(): miniboi null"); } }
            if (Name == 0)
                boiName.text = BoughtBois.boughtBoi[0].getName();
            Name = 1;
        }
    }
    public void setName(int n) { Name = n; }
}
