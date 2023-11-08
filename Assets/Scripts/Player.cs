using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Caching Components")]
    Rigidbody2D myRidigBody;

    [Header("Player Settings")]
    [SerializeField] float playerSpeed;
    [SerializeField] float aditionalMovementTime = 0.4f;
    [SerializeField] float scaleChangeMultiplier;
    [SerializeField] private Vector2 minPos, maxPos;
    //Floats
    [SerializeField] float maxScale, minScale;
    //Bools
    private bool scaleIsChanging;
    private bool mouseIsStable; //means  that the position of mouse is not changing
    private bool changingStableBool;
    //Vectors
    private Vector3 lastMousePos;

    void Start()
    {
        myRidigBody = GetComponent<Rigidbody2D>();
    }
    public Vector3 GetMouseWorldPosition()
    {
        //captures mouse position & returns WorldPoint 
        return new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
    }
    void Update()
    {
        if (GetMouseWorldPosition() != lastMousePos && mouseIsStable) //does change the stable bool if the position has changed and mouse is set to stable already
        {
            mouseIsStable = false;
        }
        else if(GetMouseWorldPosition() == lastMousePos && !mouseIsStable && !changingStableBool) //starts coroutine to change the stable bool
        {
            changingStableBool = true;
            StartCoroutine(MouseStable());
        }

        //Updating voids
        if(!mouseIsStable) //if mouse is not on one spot
        {
            MovementAndScale();
        }
    }
    private void MovementAndScale()
    {
            lastMousePos = GetMouseWorldPosition();

            Vector2 playerPos = new Vector2(transform.position.x, transform.position.y);

            playerPos.x = Mathf.Clamp(GetMouseWorldPosition().x, minPos.x, maxPos.x);
            playerPos.y = Mathf.Clamp(GetMouseWorldPosition().y, minPos.y, maxPos.y);

            //Changing position
            transform.position = Vector2.Lerp(transform.position, playerPos, playerSpeed * Time.deltaTime);
            //Changing scale
            if (!scaleIsChanging)
            {
                //StartCoroutine(ChangeScale());
                float multiplier;

                //Calculating how to change the scale
                if (transform.position.y < 0)
                {
                    multiplier = (transform.position.y * -1 - maxPos.y * -1) / (minPos.y * -1 - maxPos.y * -1);
                }
                else
                {
                    multiplier = (transform.position.y - maxPos.y * -1) / (minPos.y * -1 - maxPos.y * -1);
                }

                float scale = minScale + multiplier * (maxScale - minScale);

                transform.localScale = new Vector3(scale, scale, 0);
            }
    }
    IEnumerator MouseStable()
    {
        yield return new WaitForSeconds(aditionalMovementTime);
        mouseIsStable = true;
        changingStableBool = false;
    }
    
}
