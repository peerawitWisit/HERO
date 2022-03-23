using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioBoss : MonoBehaviour
{
    [SerializeField] public AudioSource audios;
    [SerializeField] private AudioClip castle3;

    public static audioBoss instance;
    private void Awake()
    {
        instance = this;
        audios = GetComponent<AudioSource>();
        //audios.Pause();

       
    }

    private void PlayAu(AudioClip clip)
    {
        audios.PlayOneShot(clip);
    }

    public void castle()
    {
        audios.clip = castle3;
        audios.Play(0);
    }

    public void mute()
    {
        audios.volume = 0;
    }
}
