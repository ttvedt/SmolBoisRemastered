using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class validFloor : MonoBehaviour
{
    [SerializeField]
    private float returnOffset = 3.0f; //cannot be -1.0
    private TileExpansions room;
    //private Vector3 centerOfBoisGameObject = new Vector3(-1.42, -1.71, 0);
    private Vector3 centerInWorld = new Vector3(0.00468266f, -0.8423561f, 99.99294f);
    void Start(){
        //Debug.Log("validFloor.cs Start(): start");
        //room = GameObject.Find("Room").GetComponent<TileExpansions>(); 
        //room = this.gameObject.transform.parent.gameObject.GetComponent<TileExpansions>();
        room = GetComponentInParent(typeof(TileExpansions)) as TileExpansions;
    }
    void Update(){
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("boi")){
            //Debug.Log("validFloor.cs OnTriggerEnter2D(): " + other.GetComponent<boiScript>().getMe() + " entered valid floor");
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("boi")){
            //Debug.Log("validFloor.cs OnTriggerStay2D(): " + other.GetComponent<boiScript>().getMe() + " is touching valid floor");
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.LogWarning("validFloor.cs OnTriggerExit2D(): start");
        GameObject other = collision.gameObject;
        if (other.CompareTag("boi")){// && sceneName == "BaseGame"){
            float level = (float)room.getLevel();
            Vector3 newPos = (((level + returnOffset) * (other.transform.position)) + centerInWorld) / (level + returnOffset + 1f);
            //Debug.LogWarning("validFloor.cs OnTriggerExit2D(): Level " + level + ", " + other.GetComponent<boiScript>().getMe() + " tried to escape, returning boi to valid floor: " + newPos);
            other.transform.position = newPos;
        }
    }
}
