using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentTrigger : MonoBehaviour
{
    //Ints
    //Floats
    //Bools
    public bool onLocation;
    //Strings
    //Components
    public Transform location;
    //GameObjects
    //Vectors
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Location" && !onLocation)
        {
            onLocation = true;
            location = other.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Location" && onLocation)
        {
            onLocation = false;
            location = null;
        }
    }



}//END OF CLASS 
