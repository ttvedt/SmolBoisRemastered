using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class editName : MonoBehaviour
{
    private Text display;
    private GameManager gameManager;
    //private int num = 0;
    private Profile profileObject;
    private GameObject thisBoi;
    private boiScript thisBoiScript;

    void Start(){
        //profile = this.gameObject.transform.parent.parent.gameObject.GetComponent<Profile>();
        profileObject = GameObject.Find("ProfileHandler").GetComponent<Profile>();
        thisBoi = profileObject.getBoi();
        thisBoiScript = thisBoi.GetComponent<boiScript>();
        display = GetComponent<Text>();
        if (display == null) { Debug.LogWarning("editName.cs Start(): null display"); }
        //else { Debug.Log(num + "editName.cs Start(): display set " + display.text); }
        //num++;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update(){
        
    }
    
    public void setText(string s) { 
        //Debug.Log(num + "editName.cs setText(string s): start"); 
        setName(s); 
        //Debug.Log(num + "editName.cs setText(string s): " + s + ", set"); num++; 
    }
    public void setText(int n) { 
        //Debug.Log(num + "editName.cs setText(int n): start"); 
        setName(""); 
        //Debug.Log(num + "editName.cs setText(int n): [blank]"); num++; 
    }
    public void setText() { 
        //Debug.Log(num + "editName.cs setText(): start"); 
        string s = gameManager.randomName(); setName(s); 
        //Debug.Log(num + "editName.cs setText(): " + s + ", set"); num++; 
    }
    public void setText(GameObject t) { 
        //Debug.Log(num + "editName.cs setText(GameObject t): start"); 
        string s = t.GetComponent<editText>().getText(); setName(s); 
        //Debug.Log(num + "editName.cs setText(GameObject t): " + s + ", set"); num++; 
    }
    public string getText() { 
        //Debug.Log(num + "editName.cs getText(): " + display.text); num++; 
        return display.text; 
    }
    public void setName(string s) {
        //bool isStringBlank = false;
        //isStringBlank = s == "" || s == " " || s == "  " || s == "   " || s == "    " || s == "     " || s == "      " || s == "       " || s == "        " || s == "         ";
        /*if (System.String.IsNullOrWhiteSpace(s)) { Debug.Log(num + "editName.cs setName(): [blank], not set"); num++; }
        else{
            Debug.Log(num + "editName.cs setName(): setting " + s + ":"); num++;
            display.text = s;
            Debug.Log(num + "editName.cs setName(): " + s + ", set"); num++;
            //set name of actual boi
            thisBoi = profileObject.getBoi();
            thisBoiScript = thisBoi.GetComponent<boiScript>();
            thisBoiScript.setName(s);
        }*/
        if (!(System.String.IsNullOrWhiteSpace(s))) {
            display.text = s; Debug.Log("editName.cs setName(): " + s);// num++;
            thisBoi = profileObject.getBoi();
            thisBoiScript = thisBoi.GetComponent<boiScript>();
            thisBoiScript.setName(s);
        }
    }
}
