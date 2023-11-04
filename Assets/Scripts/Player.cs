using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Caching Components")]
    Rigidbody2D myRidigBody;

    [Header("Player Settings")]
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] float minX = 0;
    [SerializeField] float maxX = 32;
    [SerializeField] float minY = 0;
    [SerializeField] float maxY = 18;
    

    void Start()
    {
        myRidigBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float mousePosInUnitsX = Input.mousePosition.x / Screen.width * 53;
        float mousePosInUnitsY = Input.mousePosition.y / Screen.height * 30;
        Debug.Log("X-Axis is:" + mousePosInUnitsX);
        Debug.Log("Y-Axis is:" + mousePosInUnitsY);
        Vector2 playerPos = new Vector2(transform.position.x, transform.position.y);
        playerPos.x = Mathf.Clamp(mousePosInUnitsX, minX, maxX);
        playerPos.y = Mathf.Clamp(mousePosInUnitsY, minY, maxY);
        //transform.position = playerPos; //for testing
        transform.position = Vector2.MoveTowards(transform.position, playerPos, playerSpeed * Time.deltaTime);
    }


}
