using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHandler : MonoBehaviour
{
    public SpriteRenderer[] dialogueLines;
    private SpriteRenderer activeDialogue;
    public PaintLineInteraction interaction;

    void Awake()
    {
        interaction = transform.Find("PaintLineInteraction").GetComponent<PaintLineInteraction>();
        interaction.paintingDone.AddListener(() => { paintedDialogue(); });
        if(dialogueLines.Length>0)
            activeDialogue = dialogueLines[0];
    }

    void FixedUpdate()
    {
        if(activeDialogue == null)
            return;
        if(interaction.isInRange)
        {
            activeDialogue.enabled = true;
        } else {
            activeDialogue.enabled = false;
        }
    }

    void paintedDialogue(){
        Debug.Log("Painted");
        if(dialogueLines.Length>1)
            activeDialogue = dialogueLines[1];
    }
}
