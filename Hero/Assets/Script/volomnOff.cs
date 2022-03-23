using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class volomnOff : MonoBehaviour
{
    [SerializeField] public AudioSource audios;

    public static volomnOff instance;
    private void Awake()
    {
        instance = this;
        audios = GetComponent<AudioSource>();
    }

    public void mute()
    {
        audios.volume = 0;
        audios.Stop();
    }
}
