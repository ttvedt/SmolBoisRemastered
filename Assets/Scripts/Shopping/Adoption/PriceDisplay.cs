using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using randomizedBoi;
using boiClass;

public class PriceDisplay : MonoBehaviour
{
    //This displays the entire shopping profile.
    public Text display;
    public RandomizeBoi thisboi;
    public Font custom;
    public GameObject profile;
    public boiScript bs;
    public boiScript profileBoi;
    private string[] temp;
    private bool hasSentErrorMessage = false;
    public Camera myCamera;
    
    void Start()
    {
        if (display == null){//set display
            Debug.LogError("PriceDisplay.cs Start(): display is null");
            display = GameObject.Find("Text (Legacy)").GetComponent<Text>();
        }
        GameObject.FindGameObjectWithTag("Music").GetComponent<KeepMusicPlaying>().PlayMusic();
    }

    void Update()
    {
        if (display == null) { 
            if (!hasSentErrorMessage){
                Debug.LogError("PriceDisplay.cs Update(): display is null");
                hasSentErrorMessage = true;
            }
        }
        else { 
            temp = display.text.Split("$");
        }     
        if (Input.GetMouseButtonDown(0))
        {
            CastRay();
            //display.font = custom;
            try { display.text = temp[0] + "$" + thisboi.price; }
            catch { }
        }
    }

    void CastRay()
    {
        Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null && hit.collider.tag == "boi")
        {
            try {
                profile.SetActive(true);
                thisboi = hit.collider.gameObject.GetComponent<RandomizeBoi>();
                bs = hit.collider.gameObject.GetComponent<boiScript>();
                profileBoi.setBoi(bs.thisBoi.getName(), bs.thisBoi.getSpecies(), bs.thisBoi.getGenes()[0], bs.thisBoi.getGenes()[1]);
            }
            catch { }
        }
    }
}
