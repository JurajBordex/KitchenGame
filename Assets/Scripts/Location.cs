using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
	//Ints
	//Floats
	//Bools
	[SerializeField] private bool hasObjectPlaced;

	[SerializeField] private bool cut, cook, fill, place, instrumentPlace; //the functions of this location
	//Strings
	//Components
	[SerializeField] private Transform placePositionPoint;

	public MoveableObject objectScript;
	//GameObjects
	//Vectors
	public Vector3 placePosition;

    private void Start()
    {
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
		objectScript.droppedOnLocation = false;
		objectScript.ReturnToLastPosition(false); //false cuz not happening already
		objectScript = null;
		
	}
    public void ObjectPlaced()
    {
		if(!hasObjectPlaced) //If the 
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
            }
			else if(cook && !objectScript.cookable) //ADD IF THERE IS COOKING INSTRUMENT ON IT
            {
				WrongTypeOfObject();
            }

			if(fill && objectScript.fillable)
            {
				PlaceObjectAtPosition();
            }
			else if(fill && !objectScript.fillable)
            {
				WrongTypeOfObject();
            }

			if(!cut && !cook && !fill && !place && !instrumentPlace)
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
    }

}//END OF CLASS 
