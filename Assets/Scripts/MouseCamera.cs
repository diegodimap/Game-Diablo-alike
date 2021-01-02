using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCamera : MonoBehaviour
{

    public Texture2D cursorTexture;
    public GameObject mousePoint;
    private CursorMode mode = CursorMode.ForceSoftware;
    private Vector2 hotSpot = Vector2.zero;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        try { 
            Cursor.SetCursor(cursorTexture, hotSpot, mode);
        }catch(Exception e) {
            e.GetBaseException();
        }
    }
}
