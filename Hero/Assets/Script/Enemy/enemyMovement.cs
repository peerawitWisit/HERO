using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform sc;
    [SerializeField] private int direction = 1;

    [SerializeField] private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(speed * direction, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "flip" || collision.gameObject.tag == "enemy" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "spike")
        {
            FilpSprite();
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            FilpSprite();
        }
    }

    void FilpSprite()
    {
        if (direction == 1)
        {
            direction = -1;
            transform.localScale = new Vector2(-1 * sc.transform.localScale.x, sc.transform.localScale.y);
        }
        else
        {
            direction = 1;
            transform.localScale = new Vector2(-1 * sc.transform.localScale.x, sc.transform.localScale.y);
        }
    }
}
