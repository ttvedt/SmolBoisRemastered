using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayEntertainment : MonoBehaviour
{
    private Text display;

    // Start is called before the first frame update
    void Start()
    {
        display = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        string[] temp = display.text.Split(":");
        display.text = temp[0] + ":" + (boiScript.entertainment/1000);
    }
}
