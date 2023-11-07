using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float nextSceneDelayTime = 1.0f;
    [SerializeField] float quitGameDelayTime = 0.5f;

    public void PlayGame()
    {
        StartCoroutine(LoadNextScene());
    }

    public void QuitGameButton()
    {
        StartCoroutine(QuitGame());
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(nextSceneDelayTime);
        LoadNextSceneInSequence();
    }

    IEnumerator QuitGame()
    {
        yield return new WaitForSeconds(quitGameDelayTime);
        Application.Quit();
    }
    void LoadNextSceneInSequence()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
