using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    //Ints
    //Floats
    [SerializeField] private float returnSpeed;
    //Weight is stated in grams
	[SerializeField] private float defualtIngredientWeight; //important later on when we will be changing the weight by cutting or other actions
    [SerializeField] private float currentIngredientWeight; 
    //Bools
    private bool droppedOnScale; //creted bool to not call the if statements and functions again
    public bool droppedOnLocation; //creted bool to not call the if statements and functions again

    private bool canDrag = true;
    public bool isDragging;
    private bool isReturning;

    public bool spawnable, cutable, cookable, fillable; //Function of the object

    private bool isOutside()
    {
        //Using Physics2D.Overlap to check if the object is outside of screen bounds or not
        return Physics2D.OverlapCircle(transform.position, 0.1f, whatIsOutside);
    }
    //Strings
    //Components
    //Scripts
    private Scale scale;
    private GameManager gameManager;
    [SerializeField] private Location currentLocationScript;
    [SerializeField] private InstrumentTrigger instrumentTrigger; //secndary trigger script for non spawnable objects

    //Layer masks
    [SerializeField] private LayerMask whatIsOutside;
    //GameObjects
    //Vectors
    public Vector3 lastLocationPosition;
    private Vector3 mousePositionOffset;
    public Vector3 GetMouseWorldPosition()
    {
        //captures mouse position & returns WorldPoint 
        return new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
    }

    private void Start()
    {
        currentIngredientWeight = defualtIngredientWeight;
        lastLocationPosition = transform.position;

        scale = GameObject.FindGameObjectWithTag("Scale").GetComponent<Scale>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if (spawnable) //If the object is from many other objects that are being spawned from spawner
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
            //lastLocationPosition = transform.position;

            //Changes bools when the object is picked up if they are being used
            if(droppedOnScale)
            {
                droppedOnScale = false;
                scale.RemovedObject(currentIngredientWeight);
            }
            else if(droppedOnLocation)
            {
                droppedOnLocation = false;
                if(currentLocationScript != null) //if the object has instance of the location script
                {
                    currentLocationScript.ObjectRemoved();
                    currentLocationScript = null;
                    
                }
                if(instrumentTrigger != null)
                {
                    instrumentTrigger.onLocation = false;
                    instrumentTrigger.location = null;
                }
                
            }

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

        //Checks if the object is in bounds && on location
        if (!isOutside() && instrumentTrigger != null && instrumentTrigger.onLocation)
        {
            currentLocationScript = instrumentTrigger.location.GetComponent<Location>();  //stores the current location script
            currentLocationScript.objectScript = GetComponent<MoveableObject>();
            currentLocationScript.ObjectPlaced(); //lets the location know that there is object placed
            gameManager.isDragging = false;
            isDragging = false;

        }
        else if (isOutside())
        {
            if(spawnable)
            {
                Destroy(gameObject);
            }
            else
            {
                ReturnToLastPosition(false); //false because it is not already returning
            }

        }

        //Checks if the object is on location, if not then it will return to its last position
        StartCoroutine(CheckIfDoppedOnLocation());

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
        else //when mouse is not held down, means it should stop dragging
        {
            gameManager.isDragging = false;
            isDragging = false;
        }
    }
    
    public void ReturnToLastPosition(bool returningAlready)
    {
        if(!returningAlready) //this means that the frist time you call this void, you will put false in the (), the coroutine will say true when it will call this void again
        {
            isReturning = true;
            canDrag = false;
        }

        if(isReturning)
        {
            if (Vector2.Distance(transform.position, lastLocationPosition) > 0.01f) //if position is not close enough to location pos
            {
                StartCoroutine(CallReturnAgain());
            }
            else //if the position is close enough to location pos, the loops breaks
            {
                canDrag = true;
                isReturning = false;
                //checks if the object is on location
                if (instrumentTrigger != null && instrumentTrigger.onLocation)
                {
                    currentLocationScript = instrumentTrigger.location.GetComponent<Location>();  //stores the current location script
                    currentLocationScript.objectScript = GetComponent<MoveableObject>();
                    currentLocationScript.ObjectPlaced(); //lets the location know that there is object placed
                }
                
            }
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
            if(spawnable)
            {
                //If the current location is not removed already - BUG FIX
                if (currentLocationScript != null) //if the object has instance of the location script
                {
                    currentLocationScript.ObjectRemoved();
                    currentLocationScript = null;
                }
                Destroy(gameObject);

            }
            else
            {
                ReturnToLastPosition(false); //false because it is not already returning
            }
        }
        if(other.tag == "Location" && !droppedOnLocation && !isDragging && spawnable) //If stopped dragging on location
        {
                currentLocationScript = other.GetComponent<Location>();  //stores the current location script
                currentLocationScript.objectScript = GetComponent<MoveableObject>();
                currentLocationScript.ObjectPlaced(); //lets the location know that there is object placed
                gameManager.isDragging = false;
                isDragging = false;
        }
        //THIS IS WAY IT WILL WILL WORK, OVERLAP DOESN@T WORK BECAUSE WE DON'T KNOW ON WHAT LOCATION HE IS SPeCIFICLY
        //IMPORTANT, FOR THE OBJECTS THAT ARE NOT SPAWNABLE CREATE OTHER SCRIPT AND CHILDER OF THE OBJECT, THE CHILDREN WILL HAVE TRIGGER VOIDS IN IT USING ITS OWN COLLIDER TO DETECT THAT, ONCE MOUSE IS UP IT WILL PASS IF THE OBJECT IS INSIDE LOCATION, IF SO THEN PLACE IT THERE
    }
    IEnumerator LoopingFirstDrag()
    {
        yield return new WaitForEndOfFrame();
        if (gameManager.isDragging)
        {
            ChangePositionToMouse();
        }
    }
    IEnumerator CallReturnAgain()
    {
        //Changing the position by lerp
        transform.position = Vector2.Lerp(transform.position, lastLocationPosition, returnSpeed * Time.deltaTime);
        yield return new WaitForEndOfFrame();
        //Calls it self again to change the position constantly
        ReturnToLastPosition(true);
    }
    IEnumerator CheckIfDoppedOnLocation()
    {
        //Waiting to give time for doppedOnLocation to be changed
        yield return new WaitForEndOfFrame();
        //If the object is not dropped on location it will return back to original position
        if(!droppedOnLocation)
        {
            ReturnToLastPosition(false);
        }
    }
}//END OF CLASS 
