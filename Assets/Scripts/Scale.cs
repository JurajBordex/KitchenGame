using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scale : MonoBehaviour
{
	//Ints
	//Floats
	[SerializeField] private float totalWeight;
	//Bools
	//Strings
	//Components
	[SerializeField] private TextMeshProUGUI totalWeightText;
	//GameObjects
	//Vectors

	public void AddedObject(float addedWeight)
    {
		totalWeight += addedWeight;
		totalWeightText.text = totalWeight.ToString() + "g";
	}
	public void RemovedObject(float removedWeight)
	{
		totalWeight -= removedWeight;
		totalWeightText.text = totalWeight.ToString() + "g";
	}



}//END OF CLASS 
