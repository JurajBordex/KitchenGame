using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    //Ints
    public int type;
    public int state;

    public int cutToSlicesWeight, cutToSmallPiecesWeight;
    //Floats
    //Weight is stated in grams
    //Bools
    //Strings
    //Components
    private GameManager gameManager;
    private MoveableObject moveableScript;
    private Scale scale;
    //GameObjects
    //Vectors

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        scale = GameObject.FindGameObjectWithTag("Scale").GetComponent<Scale>();
        moveableScript = GetComponent<MoveableObject>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Instrument" && !moveableScript.isDragging && !gameManager.isDragging && !moveableScript.isReturning && !gameManager.isReturning && !moveableScript.droppedOnScale && !other.GetComponent<MoveableObject>().onInstrumentLocation()  &&/*NOT dropped on location*/ (other.GetComponent<MoveableObject>().droppedOnLocation || other.GetComponent<MoveableObject>().droppedOnScale)) //if nothing is being drag and it the object is not just returning and its dropped on location or scale (so you can't drag the instrument above food and then just drop it when its not on the location or scale)
        {
            if(other.GetComponent<MoveableObject>().droppedOnScale)
            {
                scale.RemovedObject(other.GetComponent<MoveableObject>().currentIngredientWeight);
            }

            other.GetComponent<Instrument>().IngredientPlaced(type, moveableScript.currentIngredientWeight, state, gameObject);
        }
    }

}//END OF CLASS 
