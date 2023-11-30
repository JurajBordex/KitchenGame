using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentTrigger : MonoBehaviour
{
    //Ints
    //Floats
    //Bools
    public bool onLocation;
    public bool onInstrumentPlaceLocation;

    public bool CloseToLocation()
    {
        return Physics2D.OverlapCircle(transform.position, 0.2f, whatIsLocation);
    }
    //Strings
    //Components
    public Transform location;

    [SerializeField] private LayerMask whatIsLocation;
    //GameObjects
    //Vectors
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Location" && !onLocation)
        {
            onLocation = true;
            location = other.transform;
            //Checking if the location has a instrument holder function
            if (other.GetComponent<Location>().instrumentPlace)
            {
                onInstrumentPlaceLocation = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Location" && onLocation)
        {
            onLocation = false;
            onInstrumentPlaceLocation = false;
        }
    }
    public void CheckIfOnLocation()
    {
        if(CloseToLocation())
        {
            onLocation = true;
        }
    }


}//END OF CLASS 
