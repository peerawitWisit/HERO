using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursor : MonoBehaviour
{
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    public static cursor instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    // Update is called once per frame
    public void hideCursor()
    {
        Cursor.visible = false;
    }

    public void showCursor()
    {
        Cursor.visible = true;
    }
}
