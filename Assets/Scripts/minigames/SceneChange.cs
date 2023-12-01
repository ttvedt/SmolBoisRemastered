using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    private GameManager gameManager;
    void Start(){
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.setSceneName(SceneManager.GetActiveScene().name);
    }

    public void baseGame(){
        Debug.Log("SceneChange.cs baseGame(): loading base game");
        SceneManager.LoadScene("BaseGame");
    }
    public void Pachinko(){
        Debug.Log("SceneChange.cs Pachinko(): loading pachinko");
        SceneManager.LoadScene("Pachinko");
    }
    public void AdoptionCenter(){
        Debug.Log("SceneChange.cs AdoptionCenter(): loading adoption center");
        SceneManager.LoadScene("AdoptionCenter");
    }
    public void ShoppingCenter(){
        Debug.Log("SceneChange.cs ShoppingCenter(): loading shopping centere");
        SceneManager.LoadScene("ShoppingCenter");
    }
    public void Pong(){
        Debug.Log("SceneChange.cs Pong(): loading pong");
        SceneManager.LoadScene("Pong");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void StartScreen()
    {
        SceneManager.LoadScene("StartScreen");
    }
}