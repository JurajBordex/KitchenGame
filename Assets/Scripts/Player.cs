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
    [SerializeField] float scaleChangeMultiplier;
    [SerializeField] private Vector2 minPos, maxPos;
    //Floats
    [SerializeField] float maxScale, minScale;
    //Bools
    private bool scaleIsChanging;
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
        if (GetMouseWorldPosition() != lastMousePos) //does only if the position has changed
        {
            lastMousePos = GetMouseWorldPosition();

            Vector2 playerPos = new Vector2(transform.position.x, transform.position.y);

            playerPos.x = Mathf.Clamp(GetMouseWorldPosition().x, minPos.x, maxPos.x);
            playerPos.y = Mathf.Clamp(GetMouseWorldPosition().y, minPos.y, maxPos.y);

            //Changing position
            transform.position = Vector2.Lerp(transform.position, playerPos, playerSpeed * Time.deltaTime);
            //Changing scale
            if(!scaleIsChanging)
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

                Debug.Log(scale);

                transform.localScale = new Vector3(scale, scale, 0);
            }
        }
    }

    /*IEnumerator ChangeScale()
    {
        scaleIsChanging = true;
        //Changes the scale based on the y position/distance from front table
        //transform.localScale = new Vector3(transform.position.y * scaleChangeMultiplier, transform.position.y * scaleChangeMultiplier, 0);


        float multiplier;
        if(transform.position.y < 0)
        {
            multiplier = (transform.position.y * -1 - minPos.y) / (maxPos.y * -1 - minPos.y);
        }
        else
        {
            multiplier = (transform.position.y - minPos.y) / (maxPos.y * -1 - minPos.y);
        }
        if (transform.position.y < 0)
        {
            multiplier = (transform.position.y * -1 - maxPos.y) / (minPos.y * -1 - maxPos.y);
        }
        else
        {
            multiplier = (transform.position.y - maxPos.y) / (minPos.y * -1 - maxPos.y);
        }

        float scale = minScale + multiplier * (maxScale - minScale);

        Debug.Log(scale);

        transform.localScale = new Vector3(scale, scale, 0);


        yield return new WaitForEndOfFrame();
        scaleIsChanging = false;
    }*/
    
}
