using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instrument : MonoBehaviour
{
	//Ints
	//Floats
	//Bools
	private bool canAddIngredient;
	//Strings
	//Components
	private MoveableObject instrumentsMoveableScript;
	//GameObjects
	//Vectors
	public List<Vector3> ingredientsTypeWeightState; //I FEEL FANCY TODAY .... AND TOMORROW

    private void Start()
    {
		instrumentsMoveableScript = GetComponent<MoveableObject>();
		//Can add ingredient is true on the game start
		canAddIngredient = true;
    }

    public void IngredientPlaced(int ingredientType, int ingredientState, float ingredientWeight, GameObject ingredientObj)
    {
		if(canAddIngredient)
        {
			//Making sure the ingredient won't be added multiple times
			canAddIngredient = false;
			StartCoroutine(EnableToAddIngredientAgain());

			//Adding the ingredient info into 1 Vector3 list
			ingredientsTypeWeightState.Add(new Vector3(ingredientType, ingredientState, ingredientWeight)); //storing the added ingredients int(name), state(coocked, raw...) and weight. This will be later used when we will decide if the dish is made correctly or not
			instrumentsMoveableScript.currentIngredientWeight += ingredientWeight; //changing the total weight of the obj
			Destroy(ingredientObj); //destroying the added ingredient
			//UPDATE THE IMAGE - ADD FOOD INTO THE INSTRUMENT
		}

	}
	IEnumerator EnableToAddIngredientAgain()
    {
		yield return new WaitForSeconds(0.05f); //3 frames
		canAddIngredient = true;
    }

}//END OF CLASS 
