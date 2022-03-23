using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] public bool lockCamera = true;

    [SerializeField] private float maxLeft;
    [SerializeField] private float maxRight;
    [SerializeField] private float maxTop;
    [SerializeField] private float maxBottom;
    [SerializeField] private bool vertical = false;
    [SerializeField] private bool horizontal = false;

    [SerializeField] private GameObject start;

    public static CameraMove instance;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        transform.position = new Vector3(start.transform.position.x, start.transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        HV();
        if (!lockCamera)
        {
            cameraMove();
        }
    }

    void HV()
    {
        if (Player.transform.position.x > maxLeft - 0.1f && Player.transform.position.x < maxRight + 0.1f)
        {
            horizontal = true;
        }
        else
        {
            horizontal = false;
        }

        if (Player.transform.position.y > maxBottom - 0.1f && Player.transform.position.y < maxTop + 0.1f)
        {
            vertical = true;
        }
        else
        {
            vertical = false;
        }
    }

    void cameraMove()
    {
        if(horizontal && vertical)
        {
            transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 1.5f, transform.position.z);
        }
        else if(horizontal && !vertical)
        {
            transform.position = new Vector3(Player.transform.position.x, transform.position.y, transform.position.z);
        }
        else if(vertical && !horizontal)
        {
            transform.position = new Vector3(transform.position.x, Player.transform.position.y + 1.5f, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
