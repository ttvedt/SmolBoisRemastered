using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Respawn : MonoBehaviour
{
    public int add;
    public Spawn spawnScript;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        spawnScript = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Spawn>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.Play();
        if (add == 100)
            ScoreManager.instance.gain100();
        else if (add == 200)
            ScoreManager.instance.gain200();
        else if (add == 300)
            ScoreManager.instance.gain300();
        else
            ScoreManager.instance.gain500();

        if ((boiScript.entertainment += 30000) > 100000)
            boiScript.entertainment = 100000;
        else
            boiScript.entertainment += 30000;
        boiScript.updateNeeds();
        spawnScript.spawnBall();
    }
}
