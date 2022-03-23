using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volumn0 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            volomnOff.instance.mute();
            Player.instance.control = false;
            Player.instance.walk = -1;
        }

    }
}
