using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    //Ints
    //Floats
    //Bools
    private bool canDrag = true;
    public bool isDragging;
    private bool isOutside()
    {
        return Physics2D.OverlapCircle(transform.position, 0.1f, whatIsOutside);
    }
    //Strings
    //Components
    private GameManager gameManager;
    //Layer masks
    [SerializeField] private LayerMask whatIsOutside;
    //GameObjects
    //Vectors
    private Vector3 mousePositionOffset;

    public Vector3 GetMouseWorldPosition()
    {
        //captures mouse position & returns WorldPoint 
        return new Vector3 (Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
    }

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        //At first spawn when you get the object from basket it will be dragging already
        gameManager.isDragging = true;
        isDragging = true;
        ChangePositionToMouse();
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
        else if(isOutside())
        {
            Destroy(gameObject);
        }

        gameManager.isDragging = false;
        isDragging = false;

    }
    private void ChangePositionToMouse()
    {
        transform.position = GetMouseWorldPosition() + mousePositionOffset;
        StartCoroutine(LoopingFirstDrag());
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
