using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class positionBoss : MonoBehaviour
{
    public GameObject position1;
    public GameObject position2;
    public GameObject position3;

    public GameObject nameBoss;

    [SerializeField] private Slider hpBar;
    [SerializeField] private Slider sheildHpbar;

    [SerializeField] public GameObject flash;
    [SerializeField] public GameObject bossDiePosition;

    public bool start;
    [SerializeField] private GameObject Boss;

    public static positionBoss instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        hpBar.gameObject.SetActive(false);
        sheildHpbar.gameObject.SetActive(false);
        nameBoss.SetActive(false);
        flash.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            hpBar.gameObject.SetActive(true);
            sheildHpbar.gameObject.SetActive(true);
            audioBoss.instance.audios.Play(0);
            Player.instance.walk = 0;
            nameBoss.SetActive(true);
            StartCoroutine(delaySpawnBoss());
            start = false;
            
        }
    }

    public void Flash()
    {
        flash.SetActive(true);
    }

    IEnumerator delaySpawnBoss()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(Boss, new Vector2(position1.transform.position.x, position1.transform.position.y), Quaternion.identity);
    }
}
