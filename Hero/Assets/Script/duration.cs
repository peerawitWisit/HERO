using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class duration : MonoBehaviour
{
    [SerializeField] public float intro = 3f;
    [SerializeField] public float atk = 10f;
    [SerializeField] public bool atkCheck = false;
    [SerializeField] public float spd = 5f;
    [SerializeField] public bool spdCheck = false;

    [SerializeField] public float gameover = 0f;

    public static duration instance;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        intro = 3f;
    }

    void Update()
    {
        if (atkCheck)
        {
            atk -= Time.deltaTime;
            if(atk <= 0)
            {
                atk = 10f;
                atkCheck = false;
                Player.instance.atk = 20;
            }
        }

        if (spdCheck)
        {
            spd -= Time.deltaTime;
            if(spd <= 0)
            {
                spd = 5f;
                spdCheck = false;
                Player.instance.m_speed = 4;
            }
        }

        if(gameover > 0 && Player.instance.die)
        {
            gameover -= Time.deltaTime;
        }

    }


}
