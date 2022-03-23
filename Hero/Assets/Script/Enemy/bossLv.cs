using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossLv : MonoBehaviour
{
    [SerializeField] private GameObject preBossPoint;
    [SerializeField] public GameObject bossPoint;
    [SerializeField] private bool checkBossPoint;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bossTalk;
    [SerializeField] private bool bossTalkCheck;
    [SerializeField] private float durationShowBoss;

    [SerializeField] public GameObject lockinLV;
    [SerializeField] public GameObject groundSpike;

    [SerializeField] private Text bossText;
    [SerializeField] private int cut = 1;
    [SerializeField] private bool cutcheck = true;
    [SerializeField] private float durationCut = 0;
    [SerializeField] private Animator aniBoss;

    public static bossLv instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        bossTalk.SetActive(false);
        cutcheck = true;
        lockinLV.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(durationShowBoss > 0)
        {
            durationShowBoss -= Time.deltaTime;

        }

        if(durationCut > 0)
        {
            durationCut -= Time.deltaTime;
            if(durationCut <= 0)
            {
                cut++;
                cutcheck = true;
            }
        }

        stopPlayer();
        /*if (Input.GetKeyDown("q"))
        {
            Player.instance.control = true;
        }*/
    }

    void stopPlayer()
    {
        if(player.transform.position.x >= preBossPoint.transform.position.x && !checkBossPoint)
        {
            Player.instance.control = false;
            checkBossPoint = true;
            Player.instance.walk = 3;
            volomnOff.instance.mute();
            //Player.instance.m_animator.SetInteger("AnimState", 0);
            
        }

        if(player.transform.position.x >= bossPoint.transform.position.x && !bossTalkCheck && durationShowBoss <=0)
        {
            
            durationShowBoss = 5f;
            bossTalkCheck = true;
        }

        if(bossTalkCheck && durationShowBoss <= 0)
        {
            if(cut == 1 && cutcheck)
            {
                sfx.instance.Click();
                bossTalk.SetActive(true);
                bossText.text = "You Shouldn't be here.";
                durationCut = 4f;
                cutcheck = false;
            }
            else if(cut == 2 && cutcheck)
            {
                bossText.text = "";
                StartCoroutine(delayText2());
                cutcheck = false;
                
            }
            else if(cut == 3 && cutcheck)
            {
                bossText.text = "...";
                StartCoroutine(delayText3());
                cutcheck = false;
                
            }
            else if(cut == 4 && cutcheck)
            {
                bossText.text = "";
                StartCoroutine(delayText4());
                cutcheck = false;

            }
            else if (cut == 5 && cutcheck)
            {
                bossText.text = "";
                StartCoroutine(delayText5());
                cutcheck = false;
            }
            else if (cut == 6 && cutcheck)
            {
                bossText.text = "";
                StartCoroutine(delayText6());
                cutcheck = false;
            }
            else if (cut == 7 && cutcheck)
            {
                bossText.text = "";
                StartCoroutine(delayText7());
                cutcheck = false;
            }
            else if (cut == 8 && cutcheck)
            {
                bossText.text = "";
                StartCoroutine(delayText8());
                cutcheck = false;
            }
            else if (cut == 9 && cutcheck)
            {
                bossText.text = "";
                StartCoroutine(delayText9());
                cutcheck = false;
            }
            else if(cut == 10 && cutcheck)
            {
                bossTalk.SetActive(false);
                aniBoss.SetBool("fadeOut", true);
                lockinLV.SetActive(true);
                groundSpike.SetActive(false);
                StartCoroutine(Battle());
                cutcheck = false;
                
            }
        }
    }

    IEnumerator delayText2()
    {
        yield return new WaitForSeconds(1f);
        sfx.instance.Click();
        bossText.text = "And you just kill my people.";
        durationCut = 4f;
    }

    IEnumerator delayText3()
    {
        yield return new WaitForSeconds(4f);
        sfx.instance.Click();
        bossText.text = "I have an offer to you.";
        durationCut = 4f;
    }

    IEnumerator delayText4()
    {
        yield return new WaitForSeconds(1f);
        sfx.instance.Click();
        bossText.text = "Turn around ,and go back.\nI promise nobody hurt you.";
        durationCut = 4f;
    }
    IEnumerator delayText5()
    {
        yield return new WaitForSeconds(1f);
        sfx.instance.Click();
        bossText.text = "And I will forget what you did today.";
        durationCut = 4f;
    }
    IEnumerator delayText6()
    {
        yield return new WaitForSeconds(2f);
        Player.instance.m_animator.SetTrigger("Attack1");
        yield return new WaitForSeconds(2f);
        sfx.instance.Click();
        bossText.text = "Refuse??";
        durationCut = 4f;
    }
    IEnumerator delayText7()
    {
        yield return new WaitForSeconds(1f);
        sfx.instance.Click();
        bossText.text = "OK FINE!! You will never pass here.";
        durationCut = 4f;
    }

    IEnumerator delayText8()
    {
        yield return new WaitForSeconds(1f);
        sfx.instance.Click();
        bossText.text = "Prepare yourself and...";
        durationCut = 4f;
    }

    IEnumerator delayText9()
    {
        yield return new WaitForSeconds(3f);
        sfx.instance.Click();
        bossText.text = "DIE!!!";
        bossText.color = Color.red;
        bossText.fontSize = 70;
        durationCut = 4f;
    }

    IEnumerator Battle()
    {
        yield return new WaitForSeconds(2f);
        positionBoss.instance.start = true;
    }
}
