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
    public float currentIngredientWeight; 
    //Bools
    public bool droppedOnScale; //creted bool to not call the if statements and functions again
    public bool droppedOnLocation; //creted bool to not call the if statements and functions again

    private bool canDrag = true;
    public bool isDragging;
    public bool isReturning;

    public bool spawnable, cutable, cookable, fillable, servingInstrument; //Function of the object

    public bool onInstrumentLocation()
    {
        //If the instrument is on its instrument(spawn) place location 
        if (instrumentTrigger != null && instrumentTrigger.onInstrumentPlaceLocation)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool isOutside()
    {
        //Using Physics2D.Overlap to check if the object is outside of screen bounds or not
        return Physics2D.OverlapCircle(transform.position, 0.1f, whatIsOutside);
    }
    private bool onScale()
    {
        //Using Physics2D.Overlap to check if the object is outside of screen bounds or not
        return Physics2D.OverlapCircle(transform.position, 0.15f, whatIsScale);
    }
    //Strings
    //Components
    [HideInInspector] public SpriteRenderer sr;
    //Scripts
    private Scale scale;
    private GameManager gameManager;
    public Location currentLocationScript;
    [SerializeField] private InstrumentTrigger instrumentTrigger; //secndary trigger script for non spawnable objects
    public Instrument instrument; //for non spawnable objects
    public Ingredient ingredientScript;

    //Layer masks
    [SerializeField] private LayerMask whatIsOutside;
    [SerializeField] private LayerMask whatIsScale;
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

        sr = GetComponent<SpriteRenderer>();
        scale = GameObject.FindGameObjectWithTag("Scale").GetComponent<Scale>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if (spawnable) //If the object is from many other objects that are being spawned from spawner
        {
            ingredientScript = GetComponent<Ingredient>();
            //At first spawn when you get the object from basket it will be dragging already
            gameManager.isDragging = true;
            isDragging = true;
            sr.sortingOrder = 10; //Setting the sorting order to show obj at top
            ChangePositionToMouse();
        }
        else //if its not ingredient then set the last location pos to current pos
        {
            lastLocationPosition = transform.position;
        }
    }
    private void OnMouseDown()
    {
        if (canDrag && !gameManager.isDragging)
        {
            //Let game manager know that you are dragging something
            gameManager.isDragging = true;
            isDragging = true;
            sr.sortingOrder = 10; //Setting the sorting order to show obj at top

            //Changes bools when the object is picked up if they are being used
            if (droppedOnScale)
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
            mousePositionOffset = new Vector3(transform.position.x, transform.position.y, 0) - GetMouseWorldPosition();
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

        //Checks if the object is in bounds(if is not outside)
        if (!isOutside() && instrumentTrigger != null && instrumentTrigger.onLocation) //check if the object has instrumentTrigger && isOnLocaion
        { 
            currentLocationScript = instrumentTrigger.location.GetComponent<Location>();  //stores the current location script
            currentLocationScript.objectScript = GetComponent<MoveableObject>();
            currentLocationScript.instrumentScript = GetComponent<Instrument>();
            currentLocationScript.ObjectPlaced(); //lets the location know that there is object placed
            gameManager.isDragging = false;
            isDragging = false;
            sr.sortingOrder = 1; //Setting the sorting order to not show obj at top

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

        if(!onScale()) //If object is not on scale
        {
            //Checks if the object is on location, if not then it will return to its last position
            StartCoroutine(CheckIfDroppedOnLocation());
        }
        else //if it is on scale
        {
           StartCoroutine(CheckIfDroppedOnScale());
        }

        gameManager.isDragging = false;
        isDragging = false;
        sr.sortingOrder = 1; //Setting the sorting order to not show obj at top


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
            sr.sortingOrder = 1; //Setting the sorting order to not show obj at top

            if (!onScale()) //If the object is not even on scale it checks if it is on location - scale is not LOCATIOn because it doesn not have a precise spot position
            {
                StartCoroutine(CheckIfDroppedOnLocationFirstTime());
            }
            else //if is on scale the lstLocationPosition will be set to its transform.position
            {
                StartCoroutine(CheckIfDroppedOnScale());
            }
        }
    }
    
    public void ReturnToLastPosition(bool returningAlready)
    {
        if(!returningAlready) //this means that the frist time you call this void, you will put false in the (), the coroutine will say true when it will call this void again
        {
            isReturning = true;
            gameManager.isReturning = true;
            canDrag = false;
            sr.sortingOrder = 1; //Setting the sorting order to not show obj at top
        }

        if (isReturning)
        {
            if (Vector2.Distance(transform.position, lastLocationPosition) > 0.01f) //if position is not close enough to location pos
            {
                StartCoroutine(CallReturnAgain());
            }
            else //if the position is close enough to location pos, the loops breaks
            {
                canDrag = true;
                isReturning = false;
                gameManager.isReturning = false;

                if (onScale()) //checks if object in on scale after returned
                {
                    StartCoroutine(CheckIfDroppedOnScale());
                }
                else //means back on location
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
        if (other.tag == "Trash" && !isDragging && !isReturning)
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
                if (instrument.ingredientsTypeWeightState.Count > 0) //if there is something in the instrument
                {
                    instrument.ingredientsTypeWeightState.Clear(); //it will clear/throw out the ingredients and then return
                    currentIngredientWeight = defualtIngredientWeight; //setting weight back to normal
                }
                ReturnToLastPosition(false); //false because it is not already returning
            }
        }
        if(other.tag == "Location" && !droppedOnLocation && !isDragging && spawnable && !isReturning) //If stopped dragging on location and its not returning
        {
                currentLocationScript = other.GetComponent<Location>();  //stores the current location script
                if(GetComponent<Instrument>() != null) //if the object has instrument script then assign it to the location script, will be used when you finished order
                {
                    currentLocationScript.instrumentScript = GetComponent<Instrument>();
                }
                currentLocationScript.objectScript = GetComponent<MoveableObject>();
                currentLocationScript.ObjectPlaced(); //lets the location know that there is object placed
                gameManager.isDragging = false;
                isDragging = false;
                sr.sortingOrder = 1; //Setting the sorting order to not show obj at top

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
    IEnumerator CallReturnAgain()
    {
        //Changing the position by lerp
        transform.position = Vector2.Lerp(transform.position, new Vector3(lastLocationPosition.x, lastLocationPosition.y, 0), returnSpeed * Time.deltaTime);
        yield return new WaitForEndOfFrame();
        //Calls it self again to change the position constantly
        ReturnToLastPosition(true);
    }
    IEnumerator CheckIfDroppedOnLocationFirstTime() //Called only once for the spawnable objects
    {
        //Calling wait 0.05 second to give time (3 frames) for the bool to be set from Location.cs
        yield return new WaitForSeconds(0.05f);
        if(!droppedOnLocation) //means it was not dropped on location yet
        {
            yield return new WaitForSeconds(0.05f); //To give time for instrument script to store values if the object was placed insie of it
            Destroy(gameObject);
        }
    }
    IEnumerator CheckIfDroppedOnLocation() //Used every time when obj is droped
    {
        //Waiting 0.05 seconds (3 frames) to give time for doppedOnLocation to be changed from Location.cs
        yield return new WaitForSeconds(0.05f);
        //If the object is not dropped on location it will return back to original position
        if(!droppedOnLocation)
        {
            ReturnToLastPosition(false);
        }
    }
    IEnumerator CheckIfDroppedOnScale()
    {
        //Waiting 0.05 seconds (3 frames) to give time for doppedOnLocation to be changed
        yield return new WaitForSeconds(0.05f);
        //If still on scale then make it like it
        if(onScale())
        {
            droppedOnScale = true;
            lastLocationPosition = transform.position;
            scale.AddedObject(currentIngredientWeight);
        }
        
    }
}//END OF CLASS 
