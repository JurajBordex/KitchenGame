using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
	//Ints
	//Floats
    //Weight is stated in grams
	[SerializeField] private float defualtIngredientWeight; //important later on when we will be changing the weight by cutting or other actions
    [SerializeField] private float currentIngredientWeight; 
    //Bools
    private bool droppedOnScale; //creted bool to not call the if statement and function again
    //Strings
    //Components
    private Scale scale;
    private Drag drag;
    //GameObjects
    //Vectors

    private void Start()
    {
        currentIngredientWeight = defualtIngredientWeight;

        scale = GameObject.FindGameObjectWithTag("Scale").GetComponent<Scale>();
        drag = GetComponent<Drag>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!droppedOnScale && !drag.isDragging && other.tag == "Scale" )
        {
            scale.AddedObject(currentIngredientWeight);
            droppedOnScale = true;
        }
        if (!drag.isDragging && other.tag == "Trash")
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (droppedOnScale && drag.isDragging && other.tag == "Scale" )
        {
            scale.RemovedObject(currentIngredientWeight);
            droppedOnScale = false;
        }
    }

}//END OF CLASS 
