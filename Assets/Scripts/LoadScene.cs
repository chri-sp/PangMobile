using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    void OnEnable()
    {
        GameManager.OnGameOver += LoadMenuSceneDeath;
        GameManager.OnVictory += LoadMenuSceneVictory;
    }

    void OnDisable()
    {
        GameManager.OnGameOver -= LoadMenuSceneDeath;
        GameManager.OnVictory -= LoadMenuSceneVictory;
    }

    public void LoadGameScene()
    {
        SceneManager.LoadSceneAsync(1);
    }

    void LoadMenuSceneDeath()
    {
        StartCoroutine(MenuLoadWaitDeath());
    }

    void LoadMenuSceneVictory()
    {
        StartCoroutine(MenuLoadWaitVictory());
    }

    IEnumerator MenuLoadWaitDeath()
    {
        yield return new WaitForSecondsRealtime(3.5f);
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(0);
    }

    IEnumerator MenuLoadWaitVictory()
    {
        yield return new WaitForSecondsRealtime(.2f);
        GameManager.Instance.PauseGame();

        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}