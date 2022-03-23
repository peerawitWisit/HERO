using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButton : MonoBehaviour
{
   
    public void Retry()
    {
        sfx.instance.Retry();
        StartCoroutine(delayRetry());
    }

    public void MouseEnter()
    {
        this.transform.localScale = new Vector2(1.1f, 1.1f);
    }

    public void MouseExit()
    {
        this.transform.localScale = new Vector2(1f, 1f);
    }

    public void MouseDown()
    {
        this.transform.localScale = new Vector2(0.9f, 0.9f);
    }

    public void MouseUp()
    {
        this.transform.localScale = new Vector2(1f, 1f);
    }

    public void Play()
    {
        sfx.instance.PlayGame();
        StartCoroutine(delayPlay());
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void showCredit()
    {
        sfx.instance.Click();
        credit.instance.creditDetail.SetActive(true);
    }

    public void returnTitle()
    {
        sfx.instance.Click();
        SceneManager.LoadScene("title");
    }

    IEnumerator delayPlay()
    {
        showCursor.instance.FadeOut();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("prologue");
    }

    IEnumerator delayRetry()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
