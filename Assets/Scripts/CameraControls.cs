using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public float maxZoom = 1;
    public float minZoom = 20;
    public float sensitivity = 1;
    public float speed = 30;
    float targetZoom;

    Vector2 mouseClickPos;
    Vector2 mouseCurrentPos;
    public static bool panning = false;
    private Vector3 ResetCamera;
    private Camera myCamera;
    private GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        myCamera = GameObject.Find("BaseGame").GetComponent<Camera>();
        ResetCamera = Camera.main.transform.position;
        targetZoom = Camera.main.orthographicSize;
    }
    void Update()
    {
        if (gameManager.getIsBaseGame())
        {
            zoom(Input.mouseScrollDelta.y);
            //Pan X

            if (Input.touchCount == 2)//touch zoom
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);
                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
                float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float currentMagnitude = (touchZero.position - touchOne.position).magnitude;
                float difference = currentMagnitude - prevMagnitude;
                zoom(difference * 0.01f);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
            }

            // When LMB clicked get mouse click position and set panning to true
            if (Input.GetKeyDown(KeyCode.Mouse0) && !panning)
            {
                // cast rays to detect if you're dragging boi or furn
                Ray rayMouse = myCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hitMouse = Physics2D.Raycast(rayMouse.origin, rayMouse.direction, Mathf.Infinity);
                bool clickHitSomething = hitMouse.collider != null && (hitMouse.collider.tag == "boi" || hitMouse.collider.tag == "furn");
                bool touchHitSomething = false;
                if (Input.touchCount > 0)
                {
                    //Debug.Log("CameraControls.cs Update() if(Input.touchCount>0): touch " + mouseClickPos);
                    Ray rayTouch = myCamera.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit2D hitTouch = Physics2D.Raycast(rayTouch.origin, rayTouch.direction, Mathf.Infinity);
                    touchHitSomething = hitTouch.collider != null && (hitTouch.collider.tag == "boi" || hitTouch.collider.tag == "furn");
                }
                // if not dragging anything
                if (!clickHitSomething && !touchHitSomething)
                {
                    mouseClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    panning = true;
                }
            }

            // If LMB is already clicked, move the camera following the mouse position update
            if (panning)
            {
                mouseCurrentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var distance = mouseCurrentPos - mouseClickPos;
                transform.position += new Vector3(-distance.x, -distance.y, 0);
            }

            // If LMB is released, stop moving the camera
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                panning = false;
            }

            if (Input.GetMouseButton(1))
            {
                Camera.main.transform.position = ResetCamera;
            }
        }
    }

    void zoom(float num)
    {
        //if (num != 0) { Debug.Log("CameraControls.cs zoom(): " + num); }
        //Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - num, 1, 8);
        targetZoom -= num * sensitivity;
        targetZoom = Mathf.Clamp(targetZoom, maxZoom, minZoom);
        float newSize = Mathf.MoveTowards(Camera.main.orthographicSize, targetZoom, speed * Time.deltaTime);
        Camera.main.orthographicSize = newSize;
    }
}


    //targetZoom -= Input.mouseScrollDelta.y * sensitivity;
    //targetZoom = Mathf.Clamp(targetZoom, maxZoom, minZoom);
    //float newSize = Mathf.MoveTowards(cam.orthographicSize, targetZoom, speed * Time.deltaTime);
    //cam.orthographicSize = newSize;

