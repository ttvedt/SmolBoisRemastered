
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingMiniGame : MonoBehaviour
{
    // The animator component that will be controlled by this script.
    public Animator animator;

    // the prefab to spawn
    public GameObject prefabToSpawn; 

    // the local position to spawn the prefab at
    Vector3 localPosition = new Vector3(3, -2, 0); 


    // The maximum speed at which the animation can play
    public float maxSpeed = 50.0f;

    // The current speed of the animation
    public static float currentSpeed = 0.0f;

    // The amount that the current speed decays each frame when the player is not clicking
    public float decayRate = 0.1f;

    // The target number that the player needs to reach to earn a reward
    public int targetNumber = 100;

    // The current number that the player has reached
    private int currentNumber = 0;

    private int debugNum = 0;

    public AudioSource audioSource;

    void Start()
    {
        // Get a reference to the animator component
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Update the current speed based on how fast the player is clicking
        currentSpeed = Input.GetMouseButton(0) ? Mathf.Min(currentSpeed + Time.deltaTime, maxSpeed) : Mathf.Max(currentSpeed - decayRate * Time.deltaTime, 0.0f);
        
        // Set the speed of the animation to the current speed
        animator.speed = currentSpeed;

        // Update the current number based on the current speed
        currentNumber += Mathf.RoundToInt(currentSpeed/2);

        // If the player has reached the target number, give them a reward and reset the current number
        if (currentNumber >= targetNumber)
        {
            GiveReward();
            currentNumber = 0;
        }

        // Update the debug statement
        Debug.Log("WalkingMiniGame.cs Update() " + debugNum + ": Current speed: " + currentSpeed + " - Target Progress: " + (currentNumber) + "/" + (targetNumber));
        debugNum++;
    }

    // This method can be used to give the player a reward
    void GiveReward()
    {
        audioSource.Play();
        // TODO: Implement reward logic here
        Instantiate(prefabToSpawn, localPosition, transform.rotation, transform);
        BoiBucks.boiBucks += 500;
        Debug.Log("WalkingMiniGame.cs GiveReward(): Loot Get!");
    }
}

