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
		//Checking weight limit
		if(totalWeight <= 9999)
        {
			totalWeightText.text = totalWeight.ToString() + "g";
		}
		else
        {
			totalWeightText.text = "Error";
        }
	}
	public void RemovedObject(float removedWeight)
	{
		totalWeight -= removedWeight;
		//Checking weight limit
		if (totalWeight <= 9999)
		{
			totalWeightText.text = totalWeight.ToString() + "g";
		}
		else
		{
			totalWeightText.text = "Error";
		}
	}



}//END OF CLASS 
