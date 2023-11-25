using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
	//Ints
	//Floats
	//Bools
	[SerializeField] private bool hasObjectPlaced;

	[SerializeField] private bool cut, fill, place; //the functions of this location
	public bool cook;
	public bool instrumentPlace, servingPlace;
	//Strings
	//Components
	[SerializeField] private Transform placePositionPoint;

	public MoveableObject objectScript;
	public Instrument instrumentScript;
	private GameSession gameSession;
	//GameObjects
	//Vectors
	public Vector3 placePosition;

    private void Start()
    {
		gameSession = GameObject.FindGameObjectWithTag("GameSession").GetComponent<GameSession>();

		placePosition = placePositionPoint.position;

    }
	private void PlaceObjectAtPosition()
    {
		objectScript.transform.position = placePosition;
		objectScript.lastLocationPosition = placePosition; //sets the last location pos
		objectScript.droppedOnLocation = true;
		hasObjectPlaced = true;
	}
	private void WrongTypeOfObject()
    {
		if(objectScript.lastLocationPosition != Vector3.zero) //if the object was not on location before
        {
			objectScript.droppedOnLocation = false;
			objectScript.ReturnToLastPosition(false); //false cuz not happening already
			objectScript = null;
		}
		else if(objectScript != null) //if the object still exists
        {
			Destroy(objectScript.gameObject);
        }
		
	}
    public void ObjectPlaced()
    {
		if(!hasObjectPlaced) //If the location doesn't have object placed already
        {

			if (instrumentPlace && !objectScript.spawnable) //means when the function of this is to store instruments and the object is instrument
            {
				PlaceObjectAtPosition();
            }
			else if(instrumentPlace && objectScript.spawnable) //means the obj is not an instrument
            {
				WrongTypeOfObject();
            }

			if(place)
            {
				PlaceObjectAtPosition();
            }

			if(cut && objectScript.cutable)
            {
				PlaceObjectAtPosition();
				//Give player option to select how many pieces to cut it into
            }
			else if(cut && !objectScript.cutable)
            {
				WrongTypeOfObject();
            }

			if(cook && objectScript.cookable)
            {
				PlaceObjectAtPosition();
				//start cooking
				if(objectScript.instrument.ingredientsTypeWeightState.Count != 0)
                {
					StartCoroutine(Cooking(10));
				}
				//visualize the cooking time in circle graph
			}
			else if(cook && !objectScript.cookable)
            {
				WrongTypeOfObject();
            }

			if(servingPlace && objectScript.servingInstrument)
            {
				PlaceObjectAtPosition();
				//The game session should check for completion
				gameSession.RecipeCompletedTracker(instrumentScript.ingredientsTypeWeightState);
            }
			else if(servingPlace && !objectScript.servingInstrument)
            {
				WrongTypeOfObject();
            }

			if(fill && objectScript.fillable)
            {
				PlaceObjectAtPosition();
				//fill
            }
			else if(fill && !objectScript.fillable)
            {
				WrongTypeOfObject();
            }

			if(!cut && !cook && !fill && !place && !instrumentPlace && !servingPlace)
            {
				Debug.Log("No Function Has Been Set, name of game object : " + this.name);
			}
		}
		else
        {
			WrongTypeOfObject(); //there is a object in this spot already
        }
		
    }
	public void ObjectRemoved()
    {
		hasObjectPlaced = false;
		objectScript = null;
		instrumentScript = null;
    }
	public void StartCooking()
    {
		StartCoroutine(Cooking(10));
    }

	IEnumerator Cooking(float secondsToWait)
    {
		if(instrumentScript.ingredientsTypeWeightState.Count != 0) //cook only if the instrument has ingredients
        {
			yield return new WaitForSeconds(secondsToWait);
			//After done wiating
			for (int i = 0; i < instrumentScript.ingredientsTypeWeightState.Count; i++) //looping every ingredient 
			{
				Vector3 ingredientInfoVector3 = instrumentScript.ingredientsTypeWeightState[i];
				if (ingredientInfoVector3.z != 4) //if the ingredient state can be changed (state is not 4) then change it and its not lamb
				{
					if (ingredientInfoVector3.x != 10) //if is not lamb
					{
						instrumentScript.ingredientsTypeWeightState[i] = new Vector3(ingredientInfoVector3.x, ingredientInfoVector3.y, 3); //cooked
					}
					else if (ingredientInfoVector3.x == 10) //if is lamb
					{
						if (ingredientInfoVector3.z != 2) //if is not cut to small pieces
						{
							instrumentScript.ingredientsTypeWeightState[i] = new Vector3(ingredientInfoVector3.x, ingredientInfoVector3.y, 3); //cooked
						}
						else if (ingredientInfoVector3.z == 2) //if is cut to small pieces
						{
							instrumentScript.ingredientsTypeWeightState[i] = new Vector3(ingredientInfoVector3.x, ingredientInfoVector3.y, 5); //cooked & cut to small pieces
						}
					}
				}
			}
		}
		
	}

}//END OF CLASS 
