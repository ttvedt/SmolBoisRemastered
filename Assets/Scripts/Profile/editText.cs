using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class editText : MonoBehaviour
{
    public InputField display;// TextMeshProUGUI is stupid (Text doesn't work, cant find textComponent?)
    private GameManager gameManager;
    //int num = 0;
    void Start(){
        display.Select();
        display.characterLimit = 0; // 0 --> infinite
        //display = GetComponent<Text>();
        if (display == null) { Debug.LogWarning("editText.cs Start(): null display"); }
        //else { Debug.Log(num + "editText.cs Start(): display set " + display.text); }
        //num++;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update(){
        
    }

    public void textExpand() {
        int length = display.text.Length;
        // skinny   .i!I
        // short    ,l();:'<>
        // medium   1-=_+?/erqtyuopasdfghjkzxcvbnERTYUOPSDFGHJKLXCVN
        // wide     234567890#$"mwQWADZBM     
        // unsupported  `~@%^&*{}[]\|
        if (length <= 22.1) { display.textComponent.fontSize = 17; } // avg
        else if (length > 120.5) { display.textComponent.fontSize = 3; }
        else if (length > 102.7) { display.textComponent.fontSize = 4; }
        else if (length > 73.7) { display.textComponent.fontSize = 5; }
        else if (length > 57.6) { display.textComponent.fontSize = 6; }
        else if (length > 55.5) { display.textComponent.fontSize = 7; }
        else if (length > 46.0) { display.textComponent.fontSize = 8; }
        else if (length > 43.1) { display.textComponent.fontSize = 9; }
        else if (length > 37.0) { display.textComponent.fontSize = 10; }
        else if (length > 32.4) { display.textComponent.fontSize = 11; }
        else if (length > 31.9) { display.textComponent.fontSize = 12; }
        else if (length > 28.3) { display.textComponent.fontSize = 13; }
        else if (length > 27.3) { display.textComponent.fontSize = 14; }
        else if (length > 24.7) { display.textComponent.fontSize = 15; }
        else if (length > 22.4) { display.textComponent.fontSize = 16; }
    }

    public void textExpandSafe() { // for random names, less likely to cut the end off
        int length = display.text.Length;
        if (length <= 18) { display.textComponent.fontSize = 17; }
        else if (length > 120) { display.textComponent.fontSize = 3; }
        else if (length > 100) { display.textComponent.fontSize = 4; }
        else if (length > 70) { display.textComponent.fontSize = 5; }
        else if (length > 50) { display.textComponent.fontSize = 6; }
        else if (length > 45) { display.textComponent.fontSize = 7; }
        else if (length > 40) { display.textComponent.fontSize = 8; }
        else if (length > 35) { display.textComponent.fontSize = 9; }
        else if (length > 30) { display.textComponent.fontSize = 10; }
        else if (length > 28) { display.textComponent.fontSize = 11; }
        else if (length > 26) { display.textComponent.fontSize = 12; }
        else if (length > 24) { display.textComponent.fontSize = 13; }
        else if (length > 22) { display.textComponent.fontSize = 14; }
        else if (length > 18) { display.textComponent.fontSize = 15; }

        // using the below, the text will never go out of the textbox, but
        // there will almost always be extra white space on the right.
        // tested on "m"s, which are wide, so when using thinner letters
        // (which is almost all of them) there will be too much empty space.
        /*if (length <= 11) { display.textComponent.fontSize = 16; }
        else if (length > 48) { display.textComponent.fontSize = 3; }
        else if (length > 37) { display.textComponent.fontSize = 4; }
        else if (length > 30) { display.textComponent.fontSize = 5; }
        else if (length > 28) { display.textComponent.fontSize = 6; }
        else if (length > 24) { display.textComponent.fontSize = 7; }
        else if (length > 21) { display.textComponent.fontSize = 8; }
        else if (length > 18) { display.textComponent.fontSize = 9; }
        else if (length > 16) { display.textComponent.fontSize = 10; }
        else if (length > 15) { display.textComponent.fontSize = 11; }
        else if (length > 14) { display.textComponent.fontSize = 12; }
        else if (length > 13) { display.textComponent.fontSize = 13; }
        else if (length > 12) { display.textComponent.fontSize = 14; }
        else if (length > 11) { display.textComponent.fontSize = 15; }*/
    }

    public void setText(string s) { 
        //Debug.Log(num + "editText.cs setText(string s): start"); 
        setName(s); 
        //Debug.Log(num + "editText.cs setText(string s): " + s + ", set"); num++; 
    }
    public void setText(int n) { 
        //Debug.Log(num + "editText.cs setText(int n): start"); 
        setName("");
        //Debug.Log(num + "editText.cs setText(int n): [blank]"); num++; 
    }
    public void setText() { 
        //Debug.Log(num + "editText.cs setText(): start"); 
        string s = gameManager.randomName(); setName(s); 
        //Debug.Log(num + "editText.cs setText(): " + s + ", set"); num++; 
    }
    public void setText(GameObject t) { 
        //Debug.Log(num + "editText.cs setText(GameObject t): start"); 
        string s = t.GetComponent<editName>().getText(); setName(s); 
        //Debug.Log(num + "editText.cs setText(GameObject t): " + s + ", set"); num++; 
    }
    public string getText() { 
        //Debug.Log(num + "editText.cs getText(): " + display.text); num++; 
        return display.text; 
    }
    public void setName(string s) {// setting textComponent
        //Debug.Log(num + "editText.cs setName(): " + s + ", set"); num++;
        display.text = s;
    }
}
