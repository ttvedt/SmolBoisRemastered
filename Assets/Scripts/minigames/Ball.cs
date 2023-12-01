using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private static Rigidbody2D rb2d;
    public GameObject spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Invoke("GoBall", 2);
        if (rb2d == null) { Debug.LogError("Ball.cs Start(): null Rigidbody2D"); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        RestartGame();
    }

    void GoBall()
    {
        float rand = Random.Range(0, 2);
        if (rand < 1)
        {
            try { rb2d.AddForce(new Vector2(20, -15)); } catch { }
        }
        else
        {
            try { rb2d.AddForce(new Vector2(-20, -15)); } catch { }
            }
    }

    public void ResetBall()
    {
        try { rb2d.velocity = Vector2.zero; } catch { }
        transform.position = spawnPoint.transform.position;
    }

    public void RestartGame()
    {
        ResetBall();
        Invoke("GoBall", 1);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Player"))
        {
            Vector2 vel;
            vel.x = rb2d.velocity.x;
            vel.y = (rb2d.velocity.y / 2) + (coll.collider.attachedRigidbody.velocity.y / 3);
            rb2d.velocity = vel;
        }
    }
}
