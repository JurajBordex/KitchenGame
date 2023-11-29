using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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



}//END OF CLASS 
