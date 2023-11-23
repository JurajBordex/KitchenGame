using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    Queue<string> dialogueSentence;

    [SerializeField] TextMeshPro npcNameText;
    [SerializeField] TextMeshPro dialogueText;

    void Start()
    {
        dialogueSentence = new Queue<string>();
        
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting Conversation with " + dialogue.npcName);

        npcNameText.text = dialogue.npcName;

        dialogueSentence.Clear();

        foreach(string sentence in dialogue.dialogueBoxes)
        {
            dialogueSentence.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(dialogueSentence.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = dialogueSentence.Dequeue();
        StopAllCoroutines();
        StartCoroutine(AnimateText(sentence));
    }

    IEnumerator AnimateText(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }

    }

    void EndDialogue()
    {
        Debug.Log("End of Conversation");
    }
}
