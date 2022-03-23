using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boss : MonoBehaviour
{
    [SerializeField] private float noDamage = 0;

    [SerializeField] private GameObject magic;
    [SerializeField] private GameObject bomb;

    [SerializeField] private GameObject spike;
    [SerializeField] private GameObject fireball;
    [SerializeField] private bool sortFire;
    [SerializeField] private GameObject player;

    [SerializeField] private Slider hpBar;
    [SerializeField] private Slider sheildHpbar;
    [SerializeField] private bool barCheck;

    [SerializeField] private Animator aniBoss;
    [SerializeField] private GameObject sheild;
    [SerializeField] private bool sheildCheck;

    [SerializeField] private float durationStart;
    [SerializeField] private bool start;

    [SerializeField] private int AttackPose = 1;
    [SerializeField] private float ChangeAttack;
    [SerializeField] private bool ChangeCheck;
    [SerializeField] private float fadeOut;
    [SerializeField] private bool first = true;
    [SerializeField] public bool die;

    [SerializeField] private int hp;
    [SerializeField] private int atk;
    [SerializeField] private int sheildHp;
    [SerializeField] private int def;

    [SerializeField] private GameObject[] firePoint1;
    [SerializeField] private GameObject[] firePoint2;

    [SerializeField] private GameObject[] eye;
    [SerializeField] private GameObject[] positionEye;
    [SerializeField] private bool eyeCheck;

    [SerializeField] private GameObject[] monsterSpawn;
    [SerializeField] private bool spawnCheck;
    [SerializeField] private int i = 0;

    [SerializeField] private GameObject[] positionSpawn;
    [SerializeField] private float durationSpawn;
    [SerializeField] private bool DspawnCheck;


    [SerializeField] private SpriteRenderer sp;

    public static boss instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

    }

    // Update is called once per frame
    void Update()
    {
        if (!barCheck)
        {
            hpBar = GameObject.FindGameObjectWithTag("hpBoss").GetComponent<Slider>();
            sheildHpbar = GameObject.FindGameObjectWithTag("sheildBoss").GetComponent<Slider>();
            player = GameObject.FindGameObjectWithTag("Player");
            firePoint1 = GameObject.FindGameObjectsWithTag("firePoint1");
            firePoint2 = GameObject.FindGameObjectsWithTag("firePoint2");
            positionEye = GameObject.FindGameObjectsWithTag("positionEye");
            positionSpawn = GameObject.FindGameObjectsWithTag("spawn");
            barCheck = true;
        }

        if (Player.instance.die)
        {
            audioBoss.instance.audios.Stop();
        }

        hpBar.value = hp;

        sheildHpbar.value = sheildHp;

        if (die)
        {
            sheildHpbar.gameObject.SetActive(false);
        }
        else if (sheildHp >= 0)
        {
            sheildHpbar.gameObject.SetActive(true);
        }

        HpLow();
        durationBoss();
        if (start && !Player.instance.die)
        {
            if (AttackPose == 1 && !die && !ChangeCheck)
            {

                ChangeCheck = true;
                if (first)
                {
                    ChangeAttack = 3f;
                }
                else
                {
                    ChangeAttack = 5.3f;
                }
                attack1();

            }
            else if (AttackPose == 2 && !die && !ChangeCheck)
            {
                attack2();
                ChangeCheck = true;
                ChangeAttack = 5f;
            }
            else if (AttackPose == 3 && !die && !ChangeCheck)
            {
                attack3();
                ChangeCheck = true;
                ChangeAttack = 6f;
            }
        }
    }

    void HpLow()
    {
        if(hp <= 100)
        {
            //StartCoroutine(delaySpawnMonster());
            if (die)
            {
                foreach (GameObject enemyName in gameManager.instance.enemies)
                {
                    if (enemyName.name.Substring(0, 4) != "boss")
                    {
                        Destroy(enemyName);
                    }

                }
            }
            else if (!spawnCheck)
            {
                foreach (GameObject monster in monsterSpawn)
                {
                    Instantiate(monster, new Vector2(positionSpawn[i].transform.position.x, positionSpawn[i].transform.position.y), Quaternion.identity);
                    i++;
                }
                if (i == 2)
                {
                    i = 0;
                    durationSpawn = 15f;
                    spawnCheck = true;
                }

            }
            else if(spawnCheck && DspawnCheck)
            {
                foreach (GameObject monster in monsterSpawn)
                {
                    Instantiate(monster, new Vector2(positionSpawn[i].transform.position.x, positionSpawn[i].transform.position.y), Quaternion.identity);
                    i++;
                }
                if (i == 2)
                {
                    i = 0;
                    durationSpawn = 15f;
                    DspawnCheck = false;
                }
            }

        }

        if(hp <= 250 && !eyeCheck)
        {
            Instantiate(eye[1], new Vector2(positionEye[0].transform.position.x, positionEye[0].transform.position.y), Quaternion.identity);
            Instantiate(eye[0], new Vector2(positionEye[1].transform.position.x, positionEye[1].transform.position.y), Quaternion.identity);
            eyeCheck = true;
        }
    }

    void durationBoss()
    {

        if (durationStart > 0)
        {
            durationStart -= Time.deltaTime;
            if (durationStart <= 0)
            {
                start = true;

            }
        }

        if (noDamage > 0)
        {
            noDamage -= Time.deltaTime;
        }

        if (ChangeAttack > 0)
        {
            ChangeAttack -= Time.deltaTime;
            if (ChangeAttack <= 0)
            {

                AttackPose += 1;
                fadeOut = 1f;
                aniBoss.SetTrigger("fadeOut");
            }
        }

        if (fadeOut > 0)
        {
            fadeOut -= Time.deltaTime;
            if (fadeOut <= 0)
            {
                ChangeCheck = false;
            }
        }

        if(durationSpawn > 0)
        {
            durationSpawn -= Time.deltaTime;
            if(durationSpawn <= 0)
            {
                DspawnCheck = true;
            }
        }

        if (AttackPose > 3)
        {
            AttackPose = 1;
        }
    }

    void attack1()
    {
        transform.position = new Vector2(positionBoss.instance.position1.transform.position.x, positionBoss.instance.position1.transform.position.y);
        GetComponent<SpriteRenderer>().flipX = false;
        StartCoroutine(delaySheild());
    }

    void attack2()
    {
        transform.position = new Vector2(positionBoss.instance.position2.transform.position.x, positionBoss.instance.position2.transform.position.y);
        StartCoroutine(delaySpike());
    }
    void attack3()
    {
        GetComponent<SpriteRenderer>().flipX = true;
        transform.position = new Vector2(positionBoss.instance.position3.transform.position.x, positionBoss.instance.position3.transform.position.y);
        StartCoroutine(delayMagic());
    }

    public void TakeDamage(int damage)
    {
        if (noDamage <= 0 && !sheildCheck)
        {
            sfx.instance.Attack();
            hp -= (damage - def);
            if (hp <= 0)
            {
                Die();
            }
            noDamage = 0.5f;

        }
        else if (sheildCheck)
        {
            sfx.instance.Attack();
            sheildHp -= damage;
            if (sheildHp <= 0)
            {
                sheildCheck = false;
                sheild.SetActive(false);
            }
        }
    }

    void Die()
    {
        bomb.SetActive(true);
        hpBar.gameObject.SetActive(false);
        sheildHpbar.gameObject.SetActive(false);
        die = true;
        sheild.SetActive(false);
        sfx.instance.BossDie();
        Player.instance.control = false;
        Player.instance.walk = 4;
        Player.instance.m_animator.SetInteger("AnimState", 0);
        foreach (GameObject enemyName in gameManager.instance.enemies)
        {
            if(enemyName.name.Substring(0,4) != "boss")
            {
                Destroy(enemyName);
            }
            
        }
        StartCoroutine(delayFlash());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !die)
        {
            Player.instance.take(atk, transform.position.x);
        }
    }

    IEnumerator delaySheild()
    {

        if (first)
        {
            aniBoss.SetTrigger("sheild");
            first = false;
            yield return new WaitForSeconds(1.3f);
        }
        else
        {
            yield return new WaitForSeconds(2.3f);
            aniBoss.SetTrigger("sheild");
            yield return new WaitForSeconds(1.3f);
        }
        sfx.instance.Sheild();
        sheild.SetActive(true);
        sheildCheck = true;
        sheildHp = 100;
    }

    IEnumerator delaySpike()
    {
        yield return new WaitForSeconds(2f);
        aniBoss.SetTrigger("spike");
        yield return new WaitForSeconds(0.5f);
        sfx.instance.Magic();
        Instantiate(spike, new Vector2(player.transform.position.x, player.transform.position.y - 2f), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        aniBoss.SetTrigger("spike");
        yield return new WaitForSeconds(0.5f);
        sfx.instance.Magic();
        Instantiate(spike, new Vector2(player.transform.position.x, player.transform.position.y - 2f), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        aniBoss.SetTrigger("spike");
        yield return new WaitForSeconds(0.5f);
        sfx.instance.Magic();
        Instantiate(spike, new Vector2(player.transform.position.x, player.transform.position.y - 2f), Quaternion.identity);
    }

    IEnumerator delayMagic()
    {
        yield return new WaitForSeconds(2f);
        aniBoss.SetTrigger("magic");
        sfx.instance.Cast();
        magic.SetActive(true);
        yield return new WaitForSeconds(2f);
        magic.SetActive(false);
        if (!sortFire)
        {
            foreach (GameObject fire in firePoint1)
            {
                Instantiate(fireball, new Vector2(fire.transform.position.x, fire.transform.position.y), Quaternion.identity);
            }
            yield return new WaitForSeconds(1.5f);
            foreach (GameObject fire in firePoint2)
            {
                Instantiate(fireball, new Vector2(fire.transform.position.x, fire.transform.position.y), Quaternion.identity);
            }
            yield return new WaitForSeconds(1.5f);
            foreach (GameObject fire in firePoint1)
            {
                Instantiate(fireball, new Vector2(fire.transform.position.x, fire.transform.position.y), Quaternion.identity);
            }
            yield return new WaitForSeconds(1.5f);
            foreach (GameObject fire in firePoint2)
            {
                Instantiate(fireball, new Vector2(fire.transform.position.x, fire.transform.position.y), Quaternion.identity);
                sortFire = true;
            }
        }
        else
        {
            foreach (GameObject fire in firePoint2)
            {
                Instantiate(fireball, new Vector2(fire.transform.position.x, fire.transform.position.y), Quaternion.identity);
            }
            yield return new WaitForSeconds(1.5f);
            foreach (GameObject fire in firePoint1)
            {
                Instantiate(fireball, new Vector2(fire.transform.position.x, fire.transform.position.y), Quaternion.identity);
            }
            yield return new WaitForSeconds(1.5f);
            foreach (GameObject fire in firePoint2)
            {
                Instantiate(fireball, new Vector2(fire.transform.position.x, fire.transform.position.y), Quaternion.identity);
            }
            yield return new WaitForSeconds(1.5f);
            foreach (GameObject fire in firePoint1)
            {
                Instantiate(fireball, new Vector2(fire.transform.position.x, fire.transform.position.y), Quaternion.identity);
                sortFire = false;
            }
        }

    }

    IEnumerator delayFlash()
    {
        yield return new WaitForSeconds(3f);
        positionBoss.instance.Flash();
        audioBoss.instance.audios.Stop();
        bomb.SetActive(false);
        GetComponent<SpriteRenderer>().flipX = false;
        Player.instance.GetComponent<SpriteRenderer>().flipX = false;
        player.transform.position = new Vector2(bossLv.instance.bossPoint.transform.position.x, bossLv.instance.bossPoint.transform.position.y);
        Player.instance.m_speed = 0;
        bossLv.instance.lockinLV.SetActive(false);
        bossLv.instance.groundSpike.SetActive(true);
        transform.position = new Vector2(positionBoss.instance.bossDiePosition.transform.position.x, positionBoss.instance.bossDiePosition.transform.position.y - 0.1f);
        yield return new WaitForSeconds(0.5f);
        aniBoss.SetTrigger("die");
        Player.instance.checkWalk4 = true;
        GetComponent<Collider2D>().enabled = false;
        //player.transform.position = new Vector2();
    }
        
}