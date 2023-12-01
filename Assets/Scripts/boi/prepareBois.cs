using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prepareBois : MonoBehaviour
{
    private GameObject gameManager;
    private GameManager gameManagerScript;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
        gameManagerScript.purchaseBois();
    }

    void Update()
    {
        
    }
}
