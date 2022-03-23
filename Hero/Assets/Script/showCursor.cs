using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showCursor : MonoBehaviour
{

    [SerializeField] public GameObject fadeOut;

    public static showCursor instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Cursor.visible = true;
        fadeOut.SetActive(false);
    }

    public void FadeOut()
    {
        fadeOut.SetActive(true);
    }
}
