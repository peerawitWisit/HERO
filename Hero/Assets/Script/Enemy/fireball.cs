using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
    [SerializeField] private float lifeTime = 8f;

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !boss.instance.die)
        {
            Player.instance.take(30, transform.position.x);
            Destroy(gameObject);
        }
    }
}
