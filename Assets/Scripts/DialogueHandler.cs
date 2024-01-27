using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHandler : MonoBehaviour
{
    public string[] dialogueLines;
    private string dialogue;
    public PaintLineInteraction interaction;

    void Awake()
    {
        interaction = transform.Find("PaintLineInteraction").GetComponent<PaintLineInteraction>();
    }

    void FixedUpdate()
    {
        if(interaction.isInRange)
        {
            if(!interaction.isDone)
            {
                dialogue = dialogueLines[0];
            }
            else
            {
                dialogue = dialogueLines[1];
            }

            Debug.Log(dialogue);
        }
    }
}
