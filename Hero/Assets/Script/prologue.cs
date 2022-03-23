using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class prologue : MonoBehaviour
{
    [SerializeField] private Text pl;
    [SerializeField] private float durationText;
    [SerializeField] private int textN = 0;
    bool check = true;

    [SerializeField] private GameObject fadeOut;
    [SerializeField] private Text press;
    bool checkText = true;

    // Start is called before the first frame update
    void Start()
    {
        textN = 0;
        fadeOut.SetActive(false);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(durationText > 0)
        {
            durationText -= Time.deltaTime;
        }
        else
        {
            if (checkText)
            {
                press.text = "Press E to Continue...";
                checkText = false;
            }
            if(textN == 1)
            {
                if (check)
                {
                    sfx.instance.Click();
                    check = false;
                }
                pl.text = "You are Hero.You is used by the king\nto going to defeat demon lord.";
                check = false;
            }
            else if(textN == 2)
            {
                if (check)
                {
                    sfx.instance.Click();
                    check = false;
                }
                pl.text = "You have no choice so you have to go to demon lord castle\nfor complete your mission.";
            }
            else if(textN == 3)
            {
                fadeOut.SetActive(true);
                StartCoroutine(delayLv1());
            }
            if (Input.GetKeyDown("e"))
            {
                check = true;
                pl.text = "";
                durationText = 0.5f;
                textN++;
            }
        }
    }

    IEnumerator delayLv1()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("level1");
    }
}
