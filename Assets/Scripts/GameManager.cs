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
    //Vectors
    private void Start()
    {
		sfx = GameObject.FindGameObjectWithTag("SFX").GetComponent<SFX>();
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
        SceneManager.LoadScene(sceneIndex);
    }

}//END OF CLASS 
