using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class theEye : MonoBehaviour
{
    [SerializeField] private GameObject laser;
    [SerializeField] private GameObject firePoint;
    [SerializeField] private Animator ani;

    [SerializeField] private float delay;
    [SerializeField] private float delayV;
    [SerializeField] private bool delayCheck = true;

    [SerializeField] private float delayFire;
    [SerializeField] private bool delayFireCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (delayCheck)
        {
            delay -= Time.deltaTime;
            if(delay <= 0)
            {
                fire();
                delayCheck = false;
                delayFireCheck = true;
                delayFire = 1.1f;
            }
        }

        if (delayFireCheck)
        {
            delayFire -= Time.deltaTime;
            if(delayFire <= 0)
            {
                Instantiate(laser, new Vector2(firePoint.transform.position.x, firePoint.transform.position.y), Quaternion.identity);
                ani.SetBool("isAttack", false);
                delayCheck = true;
                delayFireCheck = false;
                delay = delayV;
            }
        }
    }

    void fire()
    {
        ani.SetBool("isAttack", true);
    }
}
