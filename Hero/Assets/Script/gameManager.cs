using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    [SerializeField] private GameObject HpUi;
    [SerializeField] private GameObject AtkUi;
    [SerializeField] private GameObject SheildUi;
    [SerializeField] private GameObject SpeedUi;
    [SerializeField] private GameObject KeyUi;
    [SerializeField] private GameObject GameOverUi;
    [SerializeField] private GameObject ResumeUi;

    [SerializeField] public GameObject fadeOut;

    [SerializeField] public GameObject[] enemies;
    [SerializeField] private bool bossDie = false;
    [SerializeField] private GameObject prop;

    public bool pause = false;
    public static gameManager instance;
    // Start is called before the first frame update
    void Start()
    {
        fadeOut.SetActive(false);
        instance = this;
        HpUi.SetActive(false);
        AtkUi.SetActive(false);
        SheildUi.SetActive(false);
        SpeedUi.SetActive(false);
        KeyUi.SetActive(false);
        GameOverUi.SetActive(false);
        ResumeUi.SetActive(false);
        bossDie = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (duration.instance.intro <= 0)
            HpUi.SetActive(true);

        UIatk();

        UIsheild();
        
        UIspeed();

        UIkey();

        GameOver();

        enemies = GameObject.FindGameObjectsWithTag("enemy");
        if (bossDie)
        {
            for(int i = 0; i < enemies.Length; i++)
            {
                Destroy(enemies[i]);
                Instantiate(prop,new Vector2(enemies[i].transform.position.x,enemies[i].transform.position.y),Quaternion.identity);
            }

        }

        if(Input.GetKeyDown(KeyCode.Escape) && !pause && Player.instance.control)
        {
            showPause();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && Player.instance.control && pause)
        {
            hidePause();
            cursor.instance.hideCursor();
        }

        if (!pause && !Player.instance.die)
        {
            cursor.instance.hideCursor();
        }
    }



    void UIatk()
    {
        if (duration.instance.atkCheck)
        {
            AtkUi.SetActive(true);
        }
        else
        {
            AtkUi.SetActive(false);
        }
    }

    void UIsheild()
    {
        if (Player.instance.immute)
        {
            SheildUi.SetActive(true);
        }
        else
        {
            SheildUi.SetActive(false);
        }
    }

    void UIspeed()
    {
        if (duration.instance.spdCheck)
        {
            SpeedUi.SetActive(true);
        }
        else
        {
            SpeedUi.SetActive(false);
        }
    }

    void UIkey()
    {
        if (Player.instance.key)
        {
            KeyUi.SetActive(true);
        }
        else
        {
            KeyUi.SetActive(false);
        }
    }

    void GameOver()
    {
        if(duration.instance.gameover <= 0 && Player.instance.die)
        {
            GameOverUi.SetActive(true);
            cursor.instance.showCursor();
        }
    }

    void showPause()
    {
        cursor.instance.showCursor();
        sfx.instance.Pause();
        Time.timeScale = 0;
        ResumeUi.SetActive(true);
        pause = true;
    }

    public void hidePause()
    {
        
        sfx.instance.Resume();
        pause = false;
        Time.timeScale = 1;
        ResumeUi.SetActive(false);
    }

    public void FadeOut()
    {
        fadeOut.SetActive(true);
    }
}
