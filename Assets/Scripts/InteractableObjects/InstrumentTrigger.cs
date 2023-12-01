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
    //Strings
    //Components
    public Transform location;
    //GameObjects
    //Vectors
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Location" && !onLocation)
        {
            if(!other.GetComponent<Location>().hasObjectPlaced)
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
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Location" && onLocation)
        {
            onLocation = false;
            onInstrumentPlaceLocation = false;
        }
    }

}//END OF CLASS 
