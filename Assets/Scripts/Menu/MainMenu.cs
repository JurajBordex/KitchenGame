using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float nextSceneDelayTime = 1.0f;

    public void PlayGame()
    {
        StartCoroutine(LoadNextScene());
    }

    public void QuitGameButton()
    {
        Debug.Log("I Quit");
        Application.Quit();
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(nextSceneDelayTime);
        LoadNextSceneInSequence();
    }

    void LoadNextSceneInSequence()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
