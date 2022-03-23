using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOnly : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !Player.instance.die)
        {
            Player.instance.dieOnly(1000);
        }
    }
}
