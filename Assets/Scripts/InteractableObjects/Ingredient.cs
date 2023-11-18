using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    //Ints
    [SerializeField] private int type;
    [SerializeField] private int state;
    //Floats
    //Weight is stated in grams
    //Bools
    //Strings
    //Components
    private GameManager gameManager;
    private MoveableObject moveableScript;
    //GameObjects
    //Vectors

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        moveableScript = GetComponent<MoveableObject>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Instrument" && !moveableScript.isDragging && !gameManager.isDragging && !moveableScript.isReturning && !gameManager.isReturning && other.GetComponent<MoveableObject>().droppedOnLocation) //if nothing is being drag and it the object is not just returning and its dropped on location(so you can't drag the instrument above food and then just drop it when its not on the location)
        {
            other.GetComponent<Instrument>().IngredientPlaced(type, state, moveableScript.currentIngredientWeight, gameObject);
        }
    }

}//END OF CLASS 