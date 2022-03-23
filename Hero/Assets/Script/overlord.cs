using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class overlord : MonoBehaviour
{
    [SerializeField] private GameObject end;
    [SerializeField] private GameObject check;
    [SerializeField] private GameObject player;
    [SerializeField] private float speed = 1f;
    [SerializeField] private Transform sc;
    [SerializeField] public bool flip = true;
    [SerializeField] public float durationFlip;
    [SerializeField] public float durationTalk;
    [SerializeField] public float durationRead = 0;
    [SerializeField] public bool talkCheck = false;
    [SerializeField] private bool readCheck = false;
    [SerializeField] private float read2;
    [SerializeField] public float durationBack;
    [SerializeField] private GameObject talk;
    [SerializeField] private bool CheckSound;
    [SerializeField] public GameObject BlackEnd;

    [SerializeField] private Animator ani;
    [SerializeField] private bool walk = false;

    [SerializeField] private Text dialog;


    public static overlord instance;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        instance = this;
        talk.SetActive(false);
        ani = GetComponent<Animator>();
        BlackEnd.SetActive(false);
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (flip)
        {
            FilpSprite();
            flip = false;
        }
        if(durationTalk <= 0 && !talkCheck)
        {
            if(Player.instance.cut == 2)
            {
                sfx.instance.Click();
                talk.SetActive(true);
                durationRead = 5;
                talkCheck = true;
            }
            else if(Player.instance.cut == 4)
            {
                Player.instance.cut = 5;
            }
            
        }

        if(durationRead > 0)
        {
            durationRead -= Time.deltaTime;
        }
        else if(durationRead <= 0 && talkCheck && !readCheck)
        {
            talk.SetActive(false);
            readCheck = true;
            Player.instance.cut = 3;
            durationTalk = 5f;
        }

        if(player.transform.position.x > check.transform.position.x && transform.position.x < end.transform.position.x && Player.instance.cut == 3)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            walk = true;
        }
        else
        {
            walk = false;
        }

        if(Player.instance.cut == 4)
        {
            if(read2 > 0)
            {
                read2 -= Time.deltaTime;
            }
            else if(read2 <= 0)
            {
                if (!CheckSound)
                {
                    sfx.instance.Click();
                    CheckSound = true;
                }
                talk.SetActive(true);
                dialog.text = "eh... Mister??\nYou are going to scare me.";
                talkCheck = false;
                if (durationTalk > 0)
                {
                    durationTalk -= Time.deltaTime;
                }
            }
            
        }

        if (!walk)
        {
            ani.SetBool("walk", walk);
        }
        else
        {
            ani.SetBool("walk", walk);
        }
    }

    void FilpSprite()
    {
         transform.localScale = new Vector2(-1 * sc.transform.localScale.x, sc.transform.localScale.y);
    }
}
