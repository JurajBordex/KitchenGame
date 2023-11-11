using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
	//Ints
	//Floats
    //Weight is stated in grams
	[SerializeField] private float defualtIngredientWeight; //important later on when we will be changing the weight by cutting or other actions
    [SerializeField] private float currentIngredientWeight; 
    //Bools
    private bool droppedOnScale; //creted bool to not call the if statements and functions again
    private bool droppedOnLocation; //creted bool to not call the if statements and functions again

    [SerializeField] private bool isSpawnable;
    private bool canDrag = true;
    public bool isDragging;
    private bool isOutside()
    {
        return Physics2D.OverlapCircle(transform.position, 0.1f, whatIsOutside);
    }
    //Strings
    //Components
    private Scale scale;
    private GameManager gameManager;
    //Layer masks
    [SerializeField] private LayerMask whatIsOutside;
    //GameObjects
    //Vectors
    private Vector3 mousePositionOffset;
    public Vector3 GetMouseWorldPosition()
    {
        //captures mouse position & returns WorldPoint 
        return new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
    }

    private void Start()
    {
        currentIngredientWeight = defualtIngredientWeight;

        scale = GameObject.FindGameObjectWithTag("Scale").GetComponent<Scale>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if (isSpawnable) //If the object is from many other objects that are being spawned from spawner
        {
            //At first spawn when you get the object from basket it will be dragging already
            gameManager.isDragging = true;
            isDragging = true;
            ChangePositionToMouse();
        }
    }
    private void OnMouseDown()
    {
        if (canDrag && !gameManager.isDragging)
        {
            //Let game manager know that you are dragging something
            gameManager.isDragging = true;
            isDragging = true;

            //Get mouse position offset so you will hold the card where you start dragging it
            mousePositionOffset = gameObject.transform.position - GetMouseWorldPosition();
        }
    }
    private void OnMouseDrag()
    {
        //Updating the objects position
        if (canDrag && gameManager.isDragging)
        {
            transform.position = GetMouseWorldPosition() + mousePositionOffset;
        }
    }
    private void OnMouseUp()
    {

        //Checks if the object is in bounds
        if (!isOutside())
        {
            //Here goes code for checking where is the object being placed at
        }
        else if (isOutside())
        {
            Destroy(gameObject);
        }

        gameManager.isDragging = false;
        isDragging = false;

    }
    private void ChangePositionToMouse()
    {
        if (Input.GetKey(KeyCode.Mouse0)) //if the mouse left button is sitll being pressed
        {
            transform.position = GetMouseWorldPosition() + mousePositionOffset;
            StartCoroutine(LoopingFirstDrag());
        }
        else //when mouse is not held down and it should stop dragging
        {
            gameManager.isDragging = false;
            isDragging = false;
        }
    }
    

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Scale" && !droppedOnScale && !isDragging)
        {
            scale.AddedObject(currentIngredientWeight);
            droppedOnScale = true;
        }
        if (other.tag == "Trash" && !isDragging)
        {
            Destroy(gameObject);
        }
        if(other.tag == "Location" && !droppedOnLocation && !isDragging) //If stopped dragging on location
        {
            other.GetComponent<Location>().ObjectPlaced(); //lets the location know that there is object placed
            transform.position = other.GetComponent<Location>().placePosition; //places the object to the given pos
            droppedOnLocation = true;
            gameManager.isDragging = false;
            isDragging = false;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Scale" && droppedOnScale && isDragging)
        {
            scale.RemovedObject(currentIngredientWeight);
            droppedOnScale = false;
        }
        if(other.tag == "Location" && droppedOnLocation && isDragging)
        {
            other.GetComponent<Location>().ObjectRemoved();
            droppedOnLocation = false;
        }
    }
    IEnumerator LoopingFirstDrag()
    {
        yield return new WaitForEndOfFrame();
        if (gameManager.isDragging)
        {
            ChangePositionToMouse();
        }
    }

}//END OF CLASS 
