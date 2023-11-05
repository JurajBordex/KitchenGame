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
    [SerializeField] private Vector2 minPos, maxPos;


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
        /* 
        NOT NEEDED ANYMORE
        float mousePosInUnitsX = Input.mousePosition.x / Screen.width * 53; //????????? whats the * number for ?
        float mousePosInUnitsY = Input.mousePosition.y / Screen.height * 30; //???????? whats the * number for ?
        Debug.Log("X-Axis is:" + mousePosInUnitsX);
        Debug.Log("Y-Axis is:" + mousePosInUnitsY);
        playerPos.x = Mathf.Clamp(mousePosInUnitsX, minPos.x, maxPos.x);
        playerPos.y = Mathf.Clamp(mousePosInUnitsY, minPos.y, maxPos.y);
        */

        Vector2 playerPos = new Vector2(transform.position.x, transform.position.y);

        playerPos.x = Mathf.Clamp(GetMouseWorldPosition().x, minPos.x, maxPos.x);
        playerPos.y = Mathf.Clamp(GetMouseWorldPosition().y, minPos.y, maxPos.y);

        
        //transform.position = Vector2.MoveTowards(transform.position, playerPos, playerSpeed * Time.deltaTime);

        //Looks smoother (:
        transform.position = Vector2.Lerp(transform.position, playerPos, playerSpeed * Time.deltaTime);
    }


}
