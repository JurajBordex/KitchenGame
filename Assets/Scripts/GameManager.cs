using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
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
    [SerializeField] private string nextLevelName;
    //Components
    private SFX sfx;
    //GameObjects
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
        SceneManager.LoadScene(nextLevelName);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}//END OF CLASS 
