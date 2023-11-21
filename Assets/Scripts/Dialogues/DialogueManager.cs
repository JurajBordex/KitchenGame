using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    Queue<string> dialogueSentence;

    void Start()
    {
        dialogueSentence = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log(dialogue.npcName);
    }
}
