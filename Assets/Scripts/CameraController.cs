using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Ints
    //Floats
    [SerializeField] private float cameraMoveSpeed;
    [SerializeField] private float centerCameraX, maxCameraX, maxXLimit;
    //Bools
    //Strings
    //Components
    private Transform player;
    //GameObjects
    //Vectors
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        /*
        if(transform.position != player.transform.position && transform.position.x < maxCamXPos && transform.position.x > maxCamXPos * -1) //if the position has changed & is in bounds(x less that max and mix X)
        {
            //Changes the camera position based on players position
            //transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, -10), cameraMoveSpeed * Time.deltaTime);
            //transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x, transform.position.y, -10), cameraMoveSpeed * Time.deltaTime);
        }
        //NOT COOL, DOESN'T WORK VERY WELL
        */

        if(transform.position.x != centerCameraX && player.position.x > maxXLimit * -1 && player.position.x < maxXLimit) //if camera is not in middle and player is in middle
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(centerCameraX, transform.position.y, -10), cameraMoveSpeed * Time.deltaTime);
        }
        else if(player.position.x < maxXLimit * -1) //if player is on left edge
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(maxCameraX * -1, transform.position.y, -10), cameraMoveSpeed * Time.deltaTime);

        }
        else if(player.position.x > maxXLimit) //if player is on right edge
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(maxCameraX, transform.position.y, -10), cameraMoveSpeed * Time.deltaTime);
        }

    }



}//END OF CLASS 
