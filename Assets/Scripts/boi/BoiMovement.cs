using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoiMovement : MonoBehaviour
{
    private boiScript thisBoi;
    GameObject moveTo;
    public GameObject[] gameObjectChoicesFood;
    public GameObject[] gameObjectChoicesWater;
    public GameObject[] gameObjectChoicesBoi;
    public int choiceNumberFood=0;
    public int choiceNumberWater=0;
    public int choiceNumberBoi=0;
    public float speed;
    private Animator animator;
    private float moveCooldown = -1.0f; // can't wander for (waitTime - moveCooldown) seconds after spawning in
    private float waitTime = 2.0f; // (waitTime) seconds
    [SerializeField]
    private bool justMoved = true;
    private float x,y;
    private Vector3 pos;
    private Vector3 spawnPos;
    private float chanceOfReturningToSpawn = 0.05f; // 5% chance of returning to spawnpoint
    private bool setMove = true;
    [SerializeField]
    private float wander = 3.0f;
    private float moveDurationCooldown = 0.0f;
    private float moveDuration = 2.0f;
    private float dt;
    private GameManager gameManager;
    private bool isBaseGame = false;

    //private bool failCheckFood = false;
    //private bool failCheckWater = false;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        thisBoi = GetComponent<boiScript>();
        animator = GetComponent<Animator>();
        Time.timeScale = 1.0f;
        //Scene currentScene = SceneManager.GetActiveScene();
        //sceneName = currentScene.name;

        waitTime = Random.Range(0.5f, 4.0f);
        x = randomLocation(transform.position.x);
        y = randomLocation(transform.position.y);
        pos = new Vector3(x, y, 0);
        spawnPos = new Vector3(x, y, 0);

        // get objects with tag and apply it to object array
        //gameObjectChoicesFood = GameObject.FindGameObjectsWithTag("Food");
        // choose one of the gameObjects in array
        //choiceNumberFood = Random.Range(0, gameObjectChoicesFood.Length);

        // get objects with tag and apply it to object array
        //gameObjectChoicesWater = GameObject.FindGameObjectsWithTag("Water");
        // choose one of the gameObjects in array
        //choiceNumberWater = Random.Range(0, gameObjectChoicesWater.Length);
        isBaseGame = gameManager.getIsBaseGame();
    }

    void Update()
    {
        isBaseGame = gameManager.getIsBaseGame();
        dt = Time.deltaTime;
        if (isBaseGame && PlayerPrefs.GetInt("food") < 33333) {
            feed();
        }
        else if (isBaseGame && PlayerPrefs.GetInt("water") < 33333) {
            thirst();
        }
        else if (isBaseGame && PlayerPrefs.GetInt("entertainment") >= 33333 && thisBoi.canBreed()) {
            breed();
        }
        else {
            // TODO: wander
            if (justMoved) {
                moveCooldown += dt;
                if (moveCooldown >= waitTime) {
                    //animator.SetBool("waddle", false);
                    moveCooldown = moveCooldown - waitTime;
                    justMoved = false;
                    setMove = true;
                }
            }
            else {
                waitTime = Random.Range(0.5f,4.0f);
                if (moveDurationCooldown >= moveDuration) {
                    moveDurationCooldown = 0;
                    moveDuration = Random.Range(0.01f, .9f);
                    justMoved = true;
                }
                else {
                    if (setMove) {
                        x = randomLocation(transform.position.x);
                        y = randomLocation(transform.position.y);
                        pos = new Vector3(x, y, 0);//transform.position.z);
                        if (Random.value < chanceOfReturningToSpawn) { pos = spawnPos; Debug.Log("BoiMovement.cs Update(): returning to spawn"); }
                        setMove = false;
                    }
                    move(pos);
                    moveDurationCooldown += dt;
                }
            }
        }
    }

    void feed()
    {
        gameObjectChoicesFood = GameObject.FindGameObjectsWithTag("FullFoodBowl");
        choiceNumberFood = Random.Range(0, gameObjectChoicesFood.Length);
        move(gameObjectChoicesFood[choiceNumberFood].gameObject.transform.position);
    }

    void thirst()
    {
        gameObjectChoicesWater = GameObject.FindGameObjectsWithTag("FullWaterBowl");
        choiceNumberWater = Random.Range(0, gameObjectChoicesWater.Length);
        move(gameObjectChoicesWater[choiceNumberWater].gameObject.transform.position);
    }

    void breed() {
        //get objects with tag and apply it to object array
        gameObjectChoicesBoi = GameObject.FindGameObjectsWithTag("boi");
        //choose one of the gameObjects in array
        choiceNumberBoi = Random.Range(0, gameObjectChoicesBoi.Length);
        move(gameObjectChoicesBoi[choiceNumberBoi].gameObject.transform.position);
    }

    void move(Vector3 target) {
        animator.SetFloat("moveX", target.x);
        animator.SetFloat("moveY", target.y);
        animator.SetBool("waddle", true);
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    float randomLocation(float pos) { return Random.Range(pos - wander, pos + wander); }
}

