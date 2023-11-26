using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingManager : MonoBehaviour
{
	//Ints
	//Floats
	//Bools
	public bool buttonsEnabled;
	//Strings
	//Components
	public MoveableObject moveableScript;
	public Ingredient ingredientScript;

	private SFX sfx;

	[SerializeField] private GameObject buttonCuttingToSlices, buttonCuttingToSmallestPieces;
    //GameObjects
    //Vectors
    private void Start()
    {
		sfx = GameObject.FindGameObjectWithTag("SFX").GetComponent<SFX>();
    }
    public void CutToSlices()
    {
		moveableScript.currentIngredientWeight = ingredientScript.cutToSlicesWeight; //change the weight
		moveableScript.ingredientScript.state = 1; //change the state to 1
		//change the visuals
		//play sfx
		buttonCuttingToSlices.SetActive(false); //setting just this button to not active in case the player wants to cut the vegetables again
		buttonCuttingToSmallestPieces.transform.position = new Vector3(-15.44f, -2.44f, 0); //change position to more centered
    }
	public void CutToSmallestPieces()
    {
		moveableScript.currentIngredientWeight = ingredientScript.cutToSmallPiecesWeight; //change the weight
		moveableScript.ingredientScript.state = 2; //change the state to 2
        //change the visuals
        //play sfx

        //IF YOU WANT TO CHANGE THE LOCATION THAN ADD THE POSITION OF GameCanvas TO IT OR ELSE IT WON'T BE RIGHT PRECISE
        buttonCuttingToSmallestPieces.transform.position = new Vector3(-12.83f, -2.44f, 0); //change position to right defualt side
		DisableButtons();
	}
	public void EnableButtonForSmallestPieces()
    {
		buttonCuttingToSmallestPieces.transform.position = new Vector3(-15.44f, -2.44f, 0); //change position to more centered

		buttonsEnabled = true;
		buttonCuttingToSmallestPieces.SetActive(true);
    }
	public void EnableButtons()
    {
		buttonsEnabled = true;
		buttonCuttingToSlices.SetActive(true);
		buttonCuttingToSmallestPieces.SetActive(true);
		//IF YOU WANT TO CHANGE THE LOCATION THAN ADD THE POSITION OF GameCanvas TO IT OR ELSE IT WON'T BE RIGHT PRECISE
		buttonCuttingToSmallestPieces.transform.position = new Vector3(-12.83f, -2.44f , 0); //change position to right defualt side
	}
	public void DisableButtons()
    {
		buttonsEnabled = false;
		buttonCuttingToSlices.SetActive(false);
		buttonCuttingToSmallestPieces.SetActive(false);

	}

}//END OF CLASS 
