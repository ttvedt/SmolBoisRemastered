using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject pachinkoBallPrefab;
    private GameObject spawnedBall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnBall()
    {
        Destroy(GameObject.FindWithTag("Pachinko"));
        spawnedBall = Instantiate(pachinkoBallPrefab);
        spawnedBall.transform.Translate(new Vector3(0, -49, 0));
    }
}
