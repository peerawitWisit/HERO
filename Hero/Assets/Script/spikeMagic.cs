using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeMagic : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField] private float speed;

    void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player.instance.take(33, transform.position.x);
        }
    }
}
