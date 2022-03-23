using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magic : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject spike;
    [SerializeField] private GameObject par;

    [SerializeField] private float distance;

    [SerializeField] private Animator ani;

    [SerializeField] private float delay;
    [SerializeField] private float delayV;
    [SerializeField] private bool delayCheck;

    [SerializeField] private float delayFire;
    [SerializeField] private bool delayFireCheck;

    // Start is called before the first frame update
    void Start()
    {
       // par.SetActive(true);
       ani = GetComponent<Animator>();
        player = GameObject.Find("HeroKnight");
    }

    // Update is called once per frame
    void Update()
    {
        

        
        if (player.transform.position.x > transform.position.x - distance && player.transform.position.x < transform.position.x + distance && 
            player.transform.position.y < transform.position.y + distance && player.transform.position.y > transform.position.y - distance && !Player.instance.die)
        {
            if (delayCheck)
            {
                delay -= Time.deltaTime;
                if (delay <= 0)
                {
                    fire();
                    par.SetActive(true);
                    delayCheck = false;
                    delayFireCheck = true;
                    delayFire = 3f;
                }

            }
            if (delayFireCheck)
            {
                delayFire -= Time.deltaTime;
                if (delayFire <= 0)
                {
                    summon();
                    par.SetActive(false);
                    ani.SetBool("isAttack", false);
                    delayCheck = true;
                    delayFireCheck = false;
                    delay = delayV;
                }
            }
        }
        else
        {
            ani.SetBool("isAttack", false);
            delayCheck = true;
            par.SetActive(false);
            delayFireCheck = false;
            delay = 0.1f;
            
        }
    }

    void summon()
    {
        Instantiate(spike,new Vector2(player.transform.position.x,player.transform.position.y - 2f), Quaternion.identity);
        sfx.instance.Magic();
    }

    void fire()
    {
        ani.SetBool("isAttack", true);
    }
}
