using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        FindObjectOfType<DialogueManager>().DisplayNextSentence();
    }
}
