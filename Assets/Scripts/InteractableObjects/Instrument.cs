using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instrument : MonoBehaviour
{
	//Ints
	//Floats
	//Bools
	private bool canAddIngredient;
	[SerializeField] private bool servingInstrument;
	
	//Strings
	//Components
	private MoveableObject instrumentsMoveableScript;
	private GameManager gameManager;
	private Scale scale;
	public SpriteRenderer plateFillSr;
	//Vectors
	public List<Vector3> ingredientsTypeWeightState; //I FEEL FANCY TODAY .... AND TOMORROW

    private void Start()
    {
		gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
		scale = GameObject.FindGameObjectWithTag("Scale").GetComponent<Scale>();
		instrumentsMoveableScript = GetComponent<MoveableObject>();

		//Can add ingredient is true on the game start
		canAddIngredient = true;
    }
    public void IngredientPlaced(int ingredientType, float ingredientWeight, int ingredientState, GameObject ingredientObj)
    {
		if(canAddIngredient)
        {
			//Making sure the ingredient won't be added multiple times
			canAddIngredient = false;
			StartCoroutine(EnableToAddIngredientAgain());

			ingredientObj.GetComponent<MoveableObject>().ChangeLocationToNotHaveObj();

			//Adding the ingredient info into 1 Vector3 list
			ingredientsTypeWeightState.Add(new Vector3(ingredientType, ingredientWeight,ingredientState)); //storing the added ingredients int(name), state(coocked, raw...) and weight. This will be later used when we will decide if the dish is made correctly or not
			instrumentsMoveableScript.currentIngredientWeight += ingredientWeight; //changing the total weight of the obj
			Destroy(ingredientObj); //destroying the added ingredient

			//If the object is dropped on location and the location is cooking spot
			if(instrumentsMoveableScript.droppedOnLocation && instrumentsMoveableScript.currentLocationScript.cook)
            {
				//Telling location that it should start cooking
				instrumentsMoveableScript.currentLocationScript.StartCooking();
            }
			else if(instrumentsMoveableScript.droppedOnScale)
            {
				scale.AddedObject(instrumentsMoveableScript.currentIngredientWeight);
			}
			//Update the visual color
			if(!plateFillSr.gameObject.activeSelf) //if not active fill
            {
				plateFillSr.gameObject.SetActive(true);
            }
			plateFillSr.color = ingredientObj.GetComponent<MoveableObject>().cutSpriteColor;
		}

	}

	private void OnTriggerStay2D(Collider2D other)
	{
		//Passing meal onto plate or other instruments
		if (other.tag == "Instrument" && !instrumentsMoveableScript.isDragging && !gameManager.isDragging && !instrumentsMoveableScript.isReturning && !gameManager.isReturning && other.GetComponent<MoveableObject>().droppedOnLocation) //if nothing is being drag and it the object is not just returning and its dropped on location(so you can't drag the instrument above food and then just drop it when its not on the location)
		{
			//Assigning other instrument script to not search for it multiple times
			Instrument otherInstrument = other.GetComponent<Instrument>();

			//Adding the ingredients in this instrument to the other instrument/plate/bowl
			for (int i = 0; i < ingredientsTypeWeightState.Count; i++)
            {
				otherInstrument.ingredientsTypeWeightState.Add(ingredientsTypeWeightState[i]);
			}

			otherInstrument.instrumentsMoveableScript.currentIngredientWeight += instrumentsMoveableScript.currentIngredientWeight;
			instrumentsMoveableScript.currentIngredientWeight = instrumentsMoveableScript.defualtIngredientWeight;

			otherInstrument.plateFillSr.gameObject.SetActive(true);

			//CHANGE VISUALS OF OTHER AND THIS INSTRUMENT

			//Return the instrument to last pos
			ingredientsTypeWeightState.Clear();

			plateFillSr.gameObject.SetActive(false); //hide plate fill

			gameManager.isReturning = true;
			instrumentsMoveableScript.ReturnToLastPosition(false);
		}
	}
	IEnumerator EnableToAddIngredientAgain()
    {
		yield return new WaitForSeconds(0.05f); //3 frames
		canAddIngredient = true;
    }

}//END OF CLASS 
