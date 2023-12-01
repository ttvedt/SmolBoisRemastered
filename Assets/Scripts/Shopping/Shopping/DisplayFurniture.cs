using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayFurniture : MonoBehaviour
{
    public int ID;
    public SpriteRenderer spriteRenderer;
    public Sprite color1;
    public Sprite color2;
    public Sprite color3;
    public Sprite color4;
    public Sprite color5;
    public Sprite color6;
    public Sprite color7;
    public Sprite color8;
    public Sprite color9;
    public PolygonCollider2D thisCollider;
    private bool overlap = true;
    private Vector3 v = new Vector3(0f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        overlap = true;
        //Debug.Log("Furniture Display has been called");
        thisCollider = GetComponent<PolygonCollider2D>();
       
        StartCoroutine(turnOffOverlap());
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 16; ++i)
        {
            if (FurnitureStorage.storedItems[ID, i] == 1)
            {
                ChangeSprite(i);
                thisCollider.enabled = true;
                break;
            }
        }
    }

    IEnumerator turnOffOverlap()
    {  
        yield return new WaitForSeconds(0.5f);
        overlap = false;
    }

    void ChangeSprite(int colorID)
    {
        switch (colorID)
        {
            case 1:
                spriteRenderer.sprite = color1;
                break;
            case 2:
                spriteRenderer.sprite = color2;
                break;
            case 3:
                spriteRenderer.sprite = color3;
                break;
            case 4:
                spriteRenderer.sprite = color4;
                break;
            case 5:
                spriteRenderer.sprite = color5;
                break;
            case 6:
                spriteRenderer.sprite = color6;
                break;
            case 7:
                spriteRenderer.sprite = color7;
                break;
            case 8:
                spriteRenderer.sprite = color8;
                break;
            case 9:
                spriteRenderer.sprite = color9;
                break;
            default:
                Debug.Log("error, invalid colorID");
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (overlap)
        {
            col.gameObject.transform.position = v;
            Debug.Log("DisplayFurniture.cs OnCollisionEnter2D(): Let's see when this is called");
        }
       
    }
}

