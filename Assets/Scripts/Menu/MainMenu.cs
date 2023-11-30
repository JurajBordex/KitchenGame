using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float nextSceneDelayTime = 1.0f;

    [SerializeField] private Animator transition;
    public void PlayGame()
    {
        StartCoroutine(LoadNextScene());
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }

    IEnumerator LoadNextScene()
    {
        transition.SetTrigger("FadeIn");
        yield return new WaitForSeconds(nextSceneDelayTime);
        LoadNextSceneInSequence();
    }

    void LoadNextSceneInSequence()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        FindObjectOfType<MusicList>().LoadNextMusic();
        FindObjectOfType<Ambient>().PlayAmbient();
    }



}//END OF CLASS
