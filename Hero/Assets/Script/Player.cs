using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject start;
    [SerializeField] private GameObject titleLv;
    [SerializeField] private GameObject end;
    [SerializeField] private GameObject step;
    [SerializeField] private GameObject bossPoint;

    [SerializeField] private GameObject bossIdle;

    [SerializeField] private bool stopBoss;
    
    [SerializeField] private float durationEnd;
    [SerializeField] private bool attackCheck;
    [SerializeField] public bool checkWalk4;
    [SerializeField] private float beforeEndGame = 5f;

    public bool control = false;
    [SerializeField] public int walk = 0;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private Transform[] attackPoint;
    [SerializeField] private int direction = 0;
    [SerializeField] private LayerMask enemy;
    [SerializeField] public int cut= 1;

    [SerializeField] float speed = 2f;
    [SerializeField] public float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;
    [SerializeField] bool m_noBlood = false;
    [SerializeField] public bool m_Attack = false;
    [SerializeField] public bool immute = false;
    [SerializeField] public bool key = false;
    [SerializeField] public bool die = false;
    [SerializeField] public GameObject sheild;
    [SerializeField] private bool notake;
    [SerializeField] private float durationTake;

    [SerializeField] public GameObject fadeOut;

    public Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_HeroKnight m_groundSensor;
    private bool m_grounded = false;
    private bool m_rolling = false;
    private int m_currentAttack = 0;
    private float m_timeSinceAttack = 0.0f;
    private float m_delayToIdle = 0.0f;

    [SerializeField] private SpriteRenderer sp;

    public static Player instance;

    public int atk;
    public int maxHp;
    public int hp;
    public int def;

    private void Awake()
    {
        instance = this;
        sp = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        walk = 1;
        control = false;
        hp = maxHp;
        notake = false;
        bossIdle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(hp < 0)
        {
            hp = 0;
        }

        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;


        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        if (control)
        {
            if (durationTake > 0)
            {
                durationTake -= Time.deltaTime;
            }
            else if(durationTake <= 0)
            {
                notake = false;
            }

            // -- Handle input and movement --
            float inputX = Input.GetAxis("Horizontal");

            // Swap direction of sprite depending on walk direction
            if (inputX > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                direction = 0;
            }

            else if (inputX < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                direction = 1;
            }

            // Move
            if (!m_rolling)
                m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

            

            // -- Handle Animations --


            
                

            //Attack
            if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.5f && !m_rolling && !gameManager.instance.pause)
            {
                attack();
                
                m_currentAttack++;
                m_Attack = true;
                // Loop back to one after third attack
                if (m_currentAttack > 3)
                {
                    m_currentAttack = 1;
                }
                // Reset Attack combo if time since last attack is too large
                if (m_timeSinceAttack > 1.0f)
                {
                    m_currentAttack = 1;
                }

                // Call one of three attack animations "Attack1", "Attack2", "Attack3"
                m_animator.SetTrigger("Attack" + m_currentAttack);

                // Reset timer
                m_timeSinceAttack = 0.0f;
                StartCoroutine(delayAttack());

            }





            //Jump
            else if (Input.GetKeyDown("space") && m_grounded && !m_rolling)
            {
                m_animator.SetTrigger("Jump");
                sfx.instance.Jump();
                m_grounded = false;
                m_animator.SetBool("Grounded", m_grounded);
                m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
                m_groundSensor.Disable(0.2f);
            }

            //Run
            else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            {
                // Reset timer
                m_delayToIdle = 0.05f;
                m_animator.SetInteger("AnimState", 1);
            }

            //Idle
            else
            {
                // Prevents flickering transitions to idle
                m_delayToIdle -= Time.deltaTime;
                if (m_delayToIdle < 0)
                    m_animator.SetInteger("AnimState", 0);
            }
        }
        else
        {
            if(walk == 1)
            {
                walk1();
            }
            else if(walk == 2)
            {
                walk2();
            }
            else if (walk == 3)
            {
                walk3();
            }
            else if(walk == 4)
            {
                walk4();
            }
            else if(walk == 5)
            {
                walk5();
            }
            else if(walk == 0)
            {
                control = true;
            }
        }
    }

    private void walk1()
    {
        if (transform.position.x < start.transform.position.x)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            m_animator.SetInteger("AnimState", 1);
        }
        else
        {
            titleLv.SetActive(true);
            duration.instance.intro -= Time.deltaTime;
            m_animator.SetInteger("AnimState", 0);
            if (duration.instance.intro <= 0)
            {
                titleLv.SetActive(false);
                walk = 0;
                CameraMove.instance.lockCamera = false;


            }
        }
    }

    private void walk2()
    {
        if (transform.position.x < end.transform.position.x)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            m_animator.SetInteger("AnimState", 1);
            volomnOff.instance.mute();
            
        }
    }

    private void walk3()
    {
        if (transform.position.x < bossPoint.transform.position.x)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            foreach(GameObject enemyName in gameManager.instance.enemies)
            {
                Destroy(enemyName);
            }
            m_animator.SetInteger("AnimState", 1);
            
        }
        else if(!stopBoss)
        {
            transform.position = new Vector2(bossPoint.transform.position.x, bossPoint.transform.position.y);
            m_animator.SetInteger("AnimState", 0);
            stopBoss = true;
            CameraMove.instance.lockCamera = true;
            StartCoroutine(delayShowBoss());
            
            //audioBoss.instance.castle();
            //playOneTime = true;
        }
    }

    private void walk4()
    {
        if (checkWalk4)
        {
            if(beforeEndGame > 0)
            {
                beforeEndGame -= Time.deltaTime;
            }
            else
            {
                transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
                m_animator.SetInteger("AnimState", 1);
            }
        }
    }
    private void walk5()
    {
        if(cut == 1)
        {
            if (transform.position.x < start.transform.position.x)
            {
                transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
                m_animator.SetInteger("AnimState", 1);
            }
            else
            {
                overlord.instance.durationFlip -= Time.deltaTime;
                m_animator.SetInteger("AnimState", 0);
                if (overlord.instance.durationFlip <= 0)
                {
                    overlord.instance.flip = true;
                    cut = 2;
                }
            }
        }
        else if(cut == 2)
        {
            if (overlord.instance.durationTalk > 0)
            {
                overlord.instance.durationTalk -= Time.deltaTime;
            }
            
        }
        else if(cut == 3)
        {
            if (overlord.instance.durationBack > 0)
            {
                overlord.instance.durationBack -= Time.deltaTime;
            }

            if (transform.position.x < step.transform.position.x && overlord.instance.durationBack <= 0)
            {
                transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
                m_animator.SetInteger("AnimState", 1);
            }
            else if(transform.position.x >= step.transform.position.x)
            {
                m_animator.SetInteger("AnimState", 0);
                cut = 4;
            } 
        }
        else if(cut == 5)
        {
            if (!attackCheck)
            {
                sfx.instance.Attack();
                m_animator.SetTrigger("Attack" + 1);
                attackCheck = true;
            }
            
            if(durationEnd > 0)
            {
                durationEnd -= Time.deltaTime;
            }
            else if(durationEnd <= 0)
            {
                overlord.instance.BlackEnd.SetActive(true);
                StartCoroutine(delayEnding());
            }
        }
        
    }

    public void dieOnly(int damage)
    {
        hp -= (damage - def);
        if(hp <= 0)
        {
            Die();
        }
    }

    public void take(int damage, float positionE)
    {
        if (!immute && !die && !notake) 
        {
            m_animator.SetTrigger("Hurt");
            sfx.instance.Take();
            notake = true;
            durationTake = 1f;
            if (damage <= def + 2)
            {
                hp -= 2;
            }
            else
            {
                hp -= (damage - def);
            }

            if (transform.position.x <= positionE)
            {
                m_body2d.AddForce(new Vector2(-4000, 100));
            }
            else
            {
                m_body2d.AddForce(new Vector2(4000, 100));
            }
            if (hp <= 0)
            {
                hp = 0;
                Die();
            }
        }
        else if(immute && !die)
        {
            sheild.SetActive(false);
            immute = false;
            sfx.instance.Take();
            if (transform.position.x <= positionE)
            {
                m_body2d.AddForce(new Vector2(-4000, 100));
            }
            else
            {
                m_body2d.AddForce(new Vector2(4000, 100));
            }
        }
        
        
    }

    public void Forspike(int damage, float positionE)
    {
        if (!immute && !die)
        {
            m_animator.SetTrigger("Hurt");
            sfx.instance.Take();
            if (damage <= def + 2)
            {
                hp -= 2;
            }
            else
            {
                hp -= (damage - def);
            }

            if (transform.position.x <= positionE)
            {
                m_body2d.AddForce(new Vector2(-4000, 100));
            }
            else
            {
                m_body2d.AddForce(new Vector2(4000, 100));
            }
            if (hp <= 0)
            {
                hp = 0;
                Die();
            }
        }
        else if (immute && !die)
        {
            sheild.SetActive(false);
            immute = false;
            sfx.instance.Take();
            if (transform.position.x <= positionE)
            {
                m_body2d.AddForce(new Vector2(-4000, 100));
            }
            else
            {
                m_body2d.AddForce(new Vector2(4000, 100));
            }
        }


    }

    void attack()
    {
       Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackPoint[direction].position,attackRange, enemy);
        
        foreach(Collider2D enemy in hitEnemy)
        {
            if(enemy.name.Substring(0,3) == "oni")
            {
                enemy.GetComponent<oniMovement>().TakeDamage(atk);
            }
            else if (enemy.name.Substring(0, 3) == "bos")
            {
                enemy.GetComponent<boss>().TakeDamage(atk);
            }
            else
            {
                enemy.GetComponent<Enemy>().TakeDamage(atk);
            }
            
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint[direction].position, attackRange);
    }

    private void Die()
    {
        m_animator.SetBool("noBlood", m_noBlood);
        m_animator.SetTrigger("Death");
        sfx.instance.PlayerDie();
        volomnOff.instance.mute();
        die = true;
        duration.instance.gameover = 3;
        control = false;
        walk = -1;
    }


    IEnumerator delayAttack()
    {
        yield return new WaitForSeconds(0.5f);
        m_Attack = false;
    }

    IEnumerator delayEnding()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("title");
    }

    IEnumerator delayShowBoss()
    {
        yield return new WaitForSeconds(2f);
        bossIdle.SetActive(true);
    }
}

