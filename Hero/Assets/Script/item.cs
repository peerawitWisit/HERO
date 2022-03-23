using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    [SerializeField] private bool addHp;
    [SerializeField] private bool addSpeed;
    [SerializeField] private bool sheild;
    [SerializeField] private bool addAtk;
    [SerializeField] private bool key;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (addHp)
            {
                Player.instance.hp += 20;
                if (Player.instance.hp > Player.instance.maxHp)
                {
                    Player.instance.hp = Player.instance.maxHp;
                }
                sfx.instance.Item();
                Destroy(gameObject);
            }
            else if (addSpeed)
            {
                duration.instance.spdCheck = true;
                Player.instance.m_speed = 8;
                duration.instance.spd = 5f ;
                sfx.instance.Item();
                Destroy(gameObject);
            }
            else if (sheild)
            {
                Player.instance.immute = true;
                Player.instance.sheild.SetActive(true);
                sfx.instance.Item();
                Destroy(gameObject);
            }
            else if (addAtk)
            {
                duration.instance.atkCheck = true;
                Player.instance.atk = 40;
                sfx.instance.Item();
                duration.instance.atk = 10f;
                Destroy(gameObject);
            }
            else if (key)
            {
                Player.instance.key = true;
                sfx.instance.Item();
                Destroy(gameObject);
            }
        }
        
    }
}
