using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class end1 : MonoBehaviour
{
    [SerializeField] private bool lv1;
    [SerializeField] private bool lv2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && lv1)
        {
            sfx.instance.Win();
            StartCoroutine(delayLv2());
        }
        else if (collision.gameObject.tag == "Player" && lv2)
        {
            sfx.instance.Win();
            StartCoroutine(delayLv3());
        }
        else if(collision.gameObject.tag == "Player")
        {
            sfx.instance.Win();
            StartCoroutine(delayEndGame());
        }
    }

    IEnumerator delayLv2()
    {
        yield return new WaitForSeconds(1f);
        gameManager.instance.FadeOut();
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("level2");
    }

    IEnumerator delayLv3()
    {
        yield return new WaitForSeconds(1f);
        gameManager.instance.FadeOut();
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("level3");
    }
    IEnumerator delayEndGame()
    {
        yield return new WaitForSeconds(1f);
        gameManager.instance.FadeOut();
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("end");
    }
}
