using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx : MonoBehaviour
{
    [SerializeField] public AudioSource audios;

    [SerializeField] private AudioClip attack;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip take;
    [SerializeField] private AudioClip enemyDie;
    [SerializeField] private AudioClip playerDie;
    [SerializeField] private AudioClip item;
    [SerializeField] private AudioClip retry;
    [SerializeField] private AudioClip resume;
    [SerializeField] private AudioClip pause;
    [SerializeField] private AudioClip button;
    [SerializeField] private AudioClip win;
    [SerializeField] private AudioClip play;
    [SerializeField] private AudioClip click;
    [SerializeField] private AudioClip magic;
    [SerializeField] private AudioClip bossDie;
    [SerializeField] private AudioClip cast;
    [SerializeField] private AudioClip sheild;

    public static sfx instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        audios = GetComponent<AudioSource>();
        Time.timeScale = 1;
    }


    public void Play(AudioClip clip)
    {
        audios.PlayOneShot(clip);
    }

    public void Attack()
    {
        Play(attack);
    }

    public void Jump()
    {
        Play(jump);
    }

    public void Take()
    {
        Play(take);
    }

    public void EnemyDie()
    {
        Play(enemyDie);
    }

    public void PlayerDie()
    {
        Play(playerDie);
    }

    public void Item()
    {
        Play(item);
    }

    public void Retry()
    {
        Play(retry);
    }

    public void Resume()
    {
        Play(resume);
    }

    public void Pause()
    {
        Play(pause);
    }

    public void PlayGame()
    {
        Play(play);
    }

    public void Click()
    {
        Play(click);
    }

    public void Win()
    {
        Play(win);
    }

    public void Button()
    {
        Play(button);
    }

    public void Magic()
    {
        Play(magic);
    }

    public void BossDie()
    {
        Play(bossDie);
    }

    public void Cast()
    {
        Play(cast);
    }

    public void Sheild()
    {
        Play(sheild);
    }
}
