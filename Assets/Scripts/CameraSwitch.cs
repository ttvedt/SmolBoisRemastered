using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using randomizedBoi;

public class CameraSwitch : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject settings;
    public GameObject credits;
    public GameObject adoption;
    public GameObject shopping;
    public GameObject pachinko;
    public GameObject pong;
    public GameObject walking;
    public GameObject game;
    public GameManager gm;
    public RandomizeBoi[] rBois = new RandomizeBoi[3];
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void BaseGame()
    {
        gameManager.enterBaseGame();
        startScreen.SetActive(false);
        settings.SetActive(false);
        adoption.SetActive(false);
        credits.SetActive(false);
        shopping.SetActive(false);
        pachinko.SetActive(false);
        pong.SetActive(false);
        walking.SetActive(false);
        gm.purchaseBois();
        CameraControls.panning = false;
    }

    public void StartScreen()
    {
        gameManager.leaveBaseGame();
        startScreen.SetActive(true);
        settings.SetActive(false);
        adoption.SetActive(false);
        credits.SetActive(false);
        shopping.SetActive(false);
        pachinko.SetActive(false);
        pong.SetActive(false);
        walking.SetActive(false);
    }

    public void Settings()
    {
        gameManager.leaveBaseGame();
        startScreen.SetActive(false);
        settings.SetActive(true);
        adoption.SetActive(false);
        credits.SetActive(false);
        shopping.SetActive(false);
        pachinko.SetActive(false);
        pong.SetActive(false);
        walking.SetActive(false);
    }

    public void adoptionCenter()
    {
        gameManager.leaveBaseGame();
        startScreen.SetActive(false);
        settings.SetActive(false);
        adoption.SetActive(true);
        credits.SetActive(false);
        shopping.SetActive(false);
        pachinko.SetActive(false);
        pong.SetActive(false);
        walking.SetActive(false);
        for(int i = 0; i < 3; ++i)
        {
            if (rBois[i].off == true)
            {
                rBois[i].randomizeGenes();
                //rBois[i].off = true;
                Debug.Log("ADP BITCH");
            }
        }
        
    }

    public void shoppingCenter()
    {
        gameManager.leaveBaseGame();
        startScreen.SetActive(false);
        settings.SetActive(false);
        adoption.SetActive(false);
        credits.SetActive(false);
        shopping.SetActive(true);
        pachinko.SetActive(false);
        pong.SetActive(false);
        walking.SetActive(false);
    }

    public void Credits()
    {
        gameManager.leaveBaseGame();
        startScreen.SetActive(false);
        settings.SetActive(false);
        adoption.SetActive(false);
        credits.SetActive(true);
        shopping.SetActive(false);
        pachinko.SetActive(false);
        pong.SetActive(false);
        walking.SetActive(false);
    }

    public void Pachinko()
    {
        gameManager.leaveBaseGame();
        SetMinigameBoi.Name = 0;
        startScreen.SetActive(false);
        settings.SetActive(false);
        adoption.SetActive(false);
        credits.SetActive(false);
        shopping.SetActive(false);
        pachinko.SetActive(true);
        pong.SetActive(false);
        walking.SetActive(false);
        gameManager.PachinkoStart();
    }

    public void Pong()
    {
        gameManager.leaveBaseGame();
        startScreen.SetActive(false);
        settings.SetActive(false);
        adoption.SetActive(false);
        credits.SetActive(false);
        shopping.SetActive(false);
        pachinko.SetActive(false);
        pong.SetActive(true);
        walking.SetActive(false);
    }

    public void Walking()
    {
        gameManager.leaveBaseGame();
        startScreen.SetActive(false);
        settings.SetActive(false);
        adoption.SetActive(false);
        credits.SetActive(false);
        shopping.SetActive(false);
        pachinko.SetActive(false);
        pong.SetActive(false);
        walking.SetActive(true);
    }
}

   