using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHp;
    public int hp;
    public int def;
    public int atk;
    [SerializeField] private float noDamage = 0;

    [SerializeField] private GameObject prop;
    [SerializeField] private bool dummy;

    [SerializeField] private bool eye;
    [SerializeField] private bool speedEye;
    [SerializeField] private bool slowEye;


    [SerializeField] private SpriteRenderer sp;

    public static Enemy instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        hp = maxHp;
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if(noDamage >= 0)
        {
            noDamage -= Time.deltaTime;
        }

    }

    public void TakeDamage(int damage)
    {
        if(noDamage <= 0)
        {
            sfx.instance.Attack();
            hp -= (damage - def);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !dummy && Player.instance.control)
        {
            Player.instance.take(atk,transform.position.x);
        }
    }

    IEnumerator damageEffect()
    {
        if (eye)
        {
            if (slowEye)
            {
                yield return new WaitForSeconds(0.1f);
                sp.color = new Color(1f, 0.25f, 0.25f, 1f);
                yield return new WaitForSeconds(0.1f);
                sp.color = new Color(0.6169811f, 0.8803355f, 1f, 1f);
            }
            else if (speedEye)
            {
                yield return new WaitForSeconds(0.1f);
                sp.color = new Color(1f, 0.25f, 0.25f, 1f);
                yield return new WaitForSeconds(0.1f);
                sp.color = new Color(1f, 0.4943396f, 0.4943396f, 1f);
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
                sp.color = new Color(1f, 0.25f, 0.25f, 1f);
                yield return new WaitForSeconds(0.1f);
                sp.color = new Color(1f, 1f, 1f, 1f);
            }
            
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
            sp.color = new Color(1f, 0.25f, 0.25f, 1f);
            yield return new WaitForSeconds(0.1f);
            sp.color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
