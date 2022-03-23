using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oniMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] public float maxSpeed;
    [SerializeField] public int atk;
    [SerializeField] private int MaxHp;
    public int hp;
    public int def;
    float x;
    [SerializeField] private float durationAtk;
    [SerializeField] private bool durationAtkCheck;

    [SerializeField] private Transform sc;
    [SerializeField] private int direction = -1;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject prop;

    [SerializeField] private float cooldownATK;
    [SerializeField] private bool atkCheck = false;

    [SerializeField] private float maxFollowDistance = 10f;
    [SerializeField] private float distancetoStop = 1f;

    [SerializeField] private float noDamage = 0;

    [SerializeField] private Animator anime;
    [SerializeField] private bool walk;

    [SerializeField] private bool inRange;
    [SerializeField] private bool exit;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sp;

    public static oniMovement instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        hp = MaxHp;
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(speed * direction, rb.velocity.y);

        if (noDamage >= 0)
        {
            noDamage -= Time.deltaTime;
        }

        if (atkCheck)
        {
            cooldownATK -= Time.deltaTime;
            if(cooldownATK <= 0)
            {
                atkCheck = false;
            }
        }


        if (player.transform.position.x > transform.position.x - maxFollowDistance && player.transform.position.x < transform.position.x + maxFollowDistance && !exit)
        {
            inRange = true;
            
        }
        else
        {
            inRange = false;
            speed = 0f;
        }

        if (player.transform.position.x >= transform.position.x - distancetoStop && player.transform.position.x <= transform.position.x + distancetoStop && !Player.instance.die)
        {
            durationAtkCheck = true;
        }
        else
        {
            durationAtkCheck = false;
        }
        if (durationAtkCheck)
        {
            durationAtk -= Time.deltaTime;
            if(durationAtk <= 0 && player.transform.position.y <= transform.position.y + 1 && player.transform.position.y >= transform.position.y - 1) 
            {
                Player.instance.take(atk, transform.position.x);
            }
        }
        else
        {
            durationAtk = 0.3f;
        }


        Movement();
        if(speed > 0)
        {
            walk = true;
        }
        else
        {
            walk = false;
        }
        buff();

        anime.SetBool("walk",walk);

        if (exit)
        {
            if (x > transform.position.x)
            {
                if(player.transform.position.x < transform.position.x - distancetoStop)
                {
                    exit = false;
                }
            }
            else if(x < transform.position.x)
            {
                if (player.transform.position.x > transform.position.x + distancetoStop)
                {
                    exit = false;
                }
            }
        }
    }

    void Movement()
    {
        if (inRange)
        {
            if(player.transform.position.x < transform.position.x - distancetoStop)
            {

                speed = maxSpeed;
                if (direction == 1)
                {
                    
                    direction = -1;
                    transform.localScale = new Vector2(-1 * sc.transform.localScale.x, sc.transform.localScale.y);
                }
            }
            else if(player.transform.position.x > transform.position.x + distancetoStop)
            {
                speed = maxSpeed;
                if (direction == -1)
                {
                    
                    direction = 1;
                    transform.localScale = new Vector2(-1 * sc.transform.localScale.x, sc.transform.localScale.y);
                }
            }
            else
            {
                if (!atkCheck && cooldownATK <= 0 && player.transform.position.y <= transform.position.y + 1 && player.transform.position.y >= transform.position.y - 1)
                {
                    anime.SetTrigger("attack");
                    atkCheck = true;
                    
                    cooldownATK = 1f;
                }
                speed = 0;
            }
        }
    }

    void buff()
    {
        if(hp <= 20)
        {
            atk = 40;
            def = 10;
            maxSpeed = 3;
        }
        else if(hp <= 60)
        {
            atk = 25;
            def = 6;
            maxSpeed = 2;
        }
    }

    public void TakeDamage(int damage)
    {
        if (noDamage <= 0)
        {
            sfx.instance.Attack();
            hp -= (damage - oniMovement.instance.def);
            if (hp <= 0)
            {
                Die();
            }
            StartCoroutine(damageEffect());
            noDamage = 0.5f;
        }

    }

    void Die()
    {
        Destroy(gameObject);
        Instantiate(prop, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
        sfx.instance.EnemyDie();
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            exit = true;
            x = player.transform.position.x;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && Player.instance.control)
        {
            Player.instance.take(13, transform.position.x);
        }
    }
    IEnumerator damageEffect()
    {
        yield return new WaitForSeconds(0.1f);
        sp.color = new Color(1f, 0.25f, 0.25f, 1f);
        yield return new WaitForSeconds(0.1f);
        sp.color = new Color(1f, 1f, 1f, 1f);
    }
}
