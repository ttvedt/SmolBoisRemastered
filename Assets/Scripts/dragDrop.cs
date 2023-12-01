using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragDrop : MonoBehaviour {
    private Vector3 mousePosOffset;
    private Camera myCamera;

    private Vector3 GetMouseWorldPosition() { return Camera.main.ScreenToWorldPoint(Input.mousePosition); }

    void Start() { myCamera = GameObject.Find("BaseGame").GetComponent<Camera>(); }
  
    public void OnMouseDown() { mousePosOffset = gameObject.transform.position - GetMouseWorldPosition(); }

    public void OnMouseDrag() 
    {
        Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        bool hitValidFloor = false;
        // valid floor needs to be more positive than the bois so mouse clicks bois not valid floor
        // ray needs to hit furniture before valid floor
        if (hit.collider != null && (hit.collider.tag == "validFloor" || hit.collider.tag != "furn")) {
            hitValidFloor = true;
        }

        if (hitValidFloor) { 
            transform.position = GetMouseWorldPosition() + mousePosOffset; 
        }
    }
}