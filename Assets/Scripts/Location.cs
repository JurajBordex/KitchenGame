using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
	//Ints
	//Floats
	//Bools
	[SerializeField] private bool hasObjectPlaced;
	//Strings
	[SerializeField] private string functionName;
	//Components
	[SerializeField] private Transform placePositionPoint;
	//GameObjects
	//Vectors
	public Vector3 placePosition;

    private void Start()
    {
		placePosition = placePositionPoint.position;
    }

    public void ObjectPlaced()
    {
		hasObjectPlaced = true;
		//if the function of this location can do something with the object that was placed it will start coroutine and execute the function
		//in the coroutine there will be something like if(functionName == "CutToSmallerPieces") then we will split the object in wanted number that was given to use when calling the object placed void
		//could also use bools or ints instead of strings
		Debug.Log("OBJECT WAS PLACED ON A LOCATION");
    }
	public void ObjectRemoved()
    {
		hasObjectPlaced = false;
    }

}//END OF CLASS 
