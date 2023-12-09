using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	//Ints
	//Floats
	//Bools
	public bool isDragging;
	public bool isReturning;
    //Strings
    //Components
    private SFX sfx;

    [SerializeField] Animator transition;
    [SerializeField] MusicList musicList;
    //Vectors
    private void Start()
    {
		sfx = GameObject.FindGameObjectWithTag("SFX").GetComponent<SFX>();
        musicList = GameObject.FindGameObjectWithTag("MusicList").GetComponent<MusicList>();

    }

    public void PlayFridgeOpenSFX()
    {
        sfx.PlayFridgeOpen();
    }
    public void PlayFridgeCloseSFX()
    {
        sfx.PlayFridgeClose();
    }
    public void PlayBookOpenSFX()
    {
        sfx.PlayBookOpen();
    }

    public void PlayBookCloseSFX()
    {
        sfx.PlayBookClosed();
    }

    public void LoadNextLevel()
    {
        StartCoroutine(TransitionToLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void RestartLevel()
    {
        StartCoroutine(TransitionToLevel(SceneManager.GetActiveScene().buildIndex));
    }
    IEnumerator TransitionToLevel(int sceneIndex)
    {
        transition.SetTrigger("FadeIn");
        yield return new WaitForSeconds(0.95f);
        musicList.LoadNextMusic();
        SceneManager.LoadScene(sceneIndex);
    }

}//END OF CLASS 
