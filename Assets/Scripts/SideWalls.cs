using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWalls : MonoBehaviour
{
    public AudioSource audioSource;
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.name == "boiPrefab (1)")
        {
            audioSource.Play();
            string wallName = transform.name;
            PongGame.Score(wallName);
            hitInfo.gameObject.SendMessage("RestartGame", 1.0f, SendMessageOptions.RequireReceiver);
        }
    }
}
