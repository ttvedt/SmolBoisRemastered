using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileExpansions : MonoBehaviour
{
    private GameManager gameManager;
    public static int Level;
    private Animator animator;
    public GameObject validFloor;
    public GameObject validFloor1;
    public GameObject validFloor2;
    public GameObject validFloor3;
    public GameObject validFloor4;
    public ButtonInfo ButtonRef;
    public Camera myCamera;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("Level", Level);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Alpha0)) & (Level < 4))  //PlaceHolder for expanding in game
        {
            Level += 1;
            animator.SetInteger("Level", Level);
            gameManager.changeMaxPopulation(Level);
        }
        if ((Input.GetKeyDown(KeyCode.Alpha9)) & (Level > 0))  //PlaceHolder for expanding in game
        {
            Level -= 1;
            animator.SetInteger("Level", Level);
            gameManager.changeMaxPopulation(Level);
        }
        if (Input.GetMouseButtonDown(0))
        {
            CastRay();
        }

        void CastRay()
        {
            Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            if (hit.collider != null && hit.collider.tag == "Buy")
            {
                ButtonRef = hit.collider.gameObject.GetComponent<ButtonInfo>();
            }
        }
    }
    public int getLevel() { return Level; }

    public void addFloor()
    {
        if (BoiBucks.boiBucks >= ButtonRef.price && ButtonRef.purchased == 0)
        {
            Level += 1;
            animator.SetInteger("Level", Level);
            gameManager.changeMaxPopulation(Level);
            ButtonRef.purchased = 1;
            BoiBucks.boiBucks -= ButtonRef.price;
            ButtonRef.purchased++;
            ButtonRef.price = 0;
        }
    }
}
