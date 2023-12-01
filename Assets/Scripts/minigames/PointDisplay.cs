using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointDisplay : MonoBehaviour
{
    private Text display;
    public static int points = 0;

    // Start is called before the first frame update
    void Start()
    {
        display = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        string[] temp = display.text.Split("\n");
        display.text = temp[0] + "\n" + points;
    }
}
