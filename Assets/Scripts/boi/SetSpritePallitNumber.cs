﻿using UnityEngine;

public class SetSpritePallitNumber : MonoBehaviour
{
    public int pallit = 1;

	// Use this for initialization
	void Start ()
	{
	    SetColor();
	}

    [ContextMenu("Set Pallet Number")]
    void SetColor()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color32((byte)pallit, 0, 0, 0);
    }
    public void SetColor(int p)
    {
        pallit = p;
        SetColor();
    }
}
