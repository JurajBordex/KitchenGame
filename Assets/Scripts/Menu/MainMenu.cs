using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float nextSceneDelayTime = 1.0f;
    [SerializeField] float quitGameDelayTime = 1.0f;

    [Header("Settings")]
    [SerializeField] Button[] menuButtons;

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
        DeactiveAllButtons();
        yield return new WaitForSeconds(nextSceneDelayTime);
        LoadNextSceneInSequence();
    }

    IEnumerator QuitGame()
    {
        DeactiveAllButtons();
        yield return new WaitForSeconds(quitGameDelayTime);
        Application.Quit();
    }
    void LoadNextSceneInSequence()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void DeactiveAllButtons()
    {
        for(int i = 0; i< menuButtons.Length; i++)
        {
            menuButtons[i].interactable = false;
        }
    }
}
