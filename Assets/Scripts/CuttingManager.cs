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
	private void PlaySFX(bool cutToSlices)
    {
		if (moveableScript.ingredientScript.type >= 9 && moveableScript.ingredientScript.type <= 11) //if is any meat
		{
			sfx.PlayCuttingMeat();
		}
		else if (moveableScript.ingredientScript.type >= 0 && moveableScript.ingredientScript.type <= 5) //if is any vegetable
        {
			sfx.PlayCuttingVegetable();
        }
		else if(cutToSlices) //when ingredient is not meat nor vegetable and was cut to slices
		{
			sfx.PlayCuttingOtherSlices();
        }
		else
        {
			sfx.PlayCuttingOtherDices();
        }

	}
    public void CutToSlices()
    {
		moveableScript.currentIngredientWeight = ingredientScript.cutToSlicesWeight; //change the weight
		moveableScript.ingredientScript.state = 1; //change the state to 1
		//change the visuals
		moveableScript.sr.sprite = moveableScript.slicedSprite;
		moveableScript.sr.color = moveableScript.cutSpriteColor;

		PlaySFX(true);
		buttonCuttingToSlices.SetActive(false); //setting just this button to not active in case the player wants to cut the vegetables again
		buttonCuttingToSmallestPieces.transform.position = new Vector3(8.10f, -2.09f, 0); //change position to more centered
    }
	public void CutToSmallestPieces()
    {
		moveableScript.currentIngredientWeight = ingredientScript.cutToSmallPiecesWeight; //change the weight
		if(moveableScript.ingredientScript.type == 10 && moveableScript.ingredientScript.type == 3) //is lamb and roasted
        {
			moveableScript.ingredientScript.state = 5; //change the state to 5
		}
		else
        {
			moveableScript.ingredientScript.state = 2; //change the state to 2
		}
		//change the visuals
		moveableScript.sr.sprite = moveableScript.dicedSprite;
		moveableScript.sr.color = moveableScript.cutSpriteColor;


		PlaySFX(false);
        //IF YOU WANT TO CHANGE THE LOCATION THAN ADD THE POSITION OF GameCanvas TO IT OR ELSE IT WON'T BE RIGHT PRECISE
        buttonCuttingToSmallestPieces.transform.position = new Vector3(9.82f, -2.09f, 0); //change position to right defualt side
		DisableButtons();
	}
	public void EnableButtonForSmallestPieces()
    {
		buttonCuttingToSmallestPieces.transform.position = new Vector3(8.10f, -2.09f, 0); //change position to more centered

		buttonsEnabled = true;
		buttonCuttingToSmallestPieces.SetActive(true);
    }
	public void EnableButtons()
    {
		buttonsEnabled = true;
		buttonCuttingToSlices.SetActive(true);
		buttonCuttingToSmallestPieces.SetActive(true);
		//IF YOU WANT TO CHANGE THE LOCATION THAN ADD THE POSITION OF GameCanvas TO IT OR ELSE IT WON'T BE RIGHT PRECISE
		buttonCuttingToSmallestPieces.transform.position = new Vector3(9.82f, -2.09f, 0); //change position to right defualt side
	}
	public void DisableButtons()
    {
		buttonsEnabled = false;
		buttonCuttingToSlices.SetActive(false);
		buttonCuttingToSmallestPieces.SetActive(false);

	}

}//END OF CLASS 
