using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserControl : MonoBehaviour
{
    [SerializeField] private float speed = 15f, lifeTime = 5f;
    [SerializeField] private int direction;
    [SerializeField] private GameObject laser;

    [SerializeField] private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        laser.transform.Rotate(0,0,90,Space.World) ; 
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    { 

        rb.velocity = new Vector2(speed * direction, rb.velocity.y); 
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !Player.instance.die)
        {
            Destroy(gameObject);
            Player.instance.take(35, transform.position.x);
        }
        else if(collision.gameObject.tag != "enemy" && collision.gameObject.tag != "item" && collision.gameObject.tag != "flip" && collision.gameObject.tag != "laser")
        {
            Destroy(gameObject);
        }
    }

}
