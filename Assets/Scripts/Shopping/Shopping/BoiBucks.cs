using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoiBucks : MonoBehaviour
{
    public static int boiBucks = 1000;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("BoiBucks.cs Start(): " + boiBucks);
        boiBucks = PlayerPrefs.GetInt("BoiBucks");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void updateBoiBucks()
    {
        Debug.Log("BoiBucks.cs updateBoiBucks(): " + boiBucks);
        PlayerPrefs.SetInt("BoiBucks", boiBucks);
        boiBucks = PlayerPrefs.GetInt("BoiBucks");
        PlayerPrefs.Save();
    }
}
