using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class credit : MonoBehaviour
{

    public GameObject creditDetail;

    public static credit instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        creditDetail.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("e"))
        {
            creditDetail.SetActive(false);
        }
    }
}
