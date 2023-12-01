using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public float min = 2f;
    public float max = 3f;
    public float defaultGravity = 1f;
    public bool dropped = false;
    public GameObject poor;
    private GameManager gameManager;
    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        min = transform.position.x - 7.75f;
        max = transform.position.x + 7.75f;
        //GameObject.FindGameObjectWithTag("Music").GetComponent<KeepMusicPlaying>().PlayMusic();

    }
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        if (!dropped)
        {
            Physics2D.gravity = new Vector2(0, 0);
            transform.position = new Vector3(Mathf.PingPong(Time.time * 2, max - min) + min, transform.position.y, transform.position.z);
            bool canDrop = gameManager.canPachinkoDrop(); // not just entered pachinko
            bool isClick = Input.GetKeyDown(KeyCode.Space); // mouse click
            bool isTouch = (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began); // or touch
            bool haveMoney = BoiBucks.boiBucks >= 200; // have enough money
            //Debug.Log("Drop.cs Update(): canDrop " + canDrop + ", isClick " + isClick + ", isTouch " + isTouch + ", haveMoney " + haveMoney);
            if (canDrop && (isClick || isTouch) && haveMoney)
            {
                Debug.Log("Drop.cs Update(): DROP");
                ScoreManager.instance.gamble();
                Physics2D.gravity = new Vector2(0, -9.8f);
                dropped = true;
            }
        }
        
    }
    public bool isDropped() { return dropped; }
}
