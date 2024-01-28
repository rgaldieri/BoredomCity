using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHandler : MonoBehaviour
{
    public SpriteRenderer[] dialogueLines;

    public SpriteRenderer[] dialogueLinesPostPaint;
    public SpriteRenderer[] activeDialogues;
    public PaintLineInteraction interaction;

    public bool isActiveDialogue;

    private int dialogueIndex = 0;

    public float timeForDialogue;
    float timer;

    void FixedUpdate(){
        if(!isActiveDialogue){
            return;
        }
        if(hasNextDialogue()){
            if(timer>0)
                timer = timer-Time.deltaTime;
            if(timer<=0){
                timer = timeForDialogue;
                nextDialogue();
            }
        }
    }

    void Awake()
    {
        interaction = transform.Find("PaintLineInteraction").GetComponent<PaintLineInteraction>();
        interaction.paintingDone.AddListener(() => {paintedDialogue();});
        interaction.playerInRange.AddListener(() => {showDialogue();});
        interaction.playerOutOfRange.AddListener(() => {closeDialogue();});
        if(dialogueLines.Length>0)
            activeDialogues = dialogueLines;
    }

    void resetDialogue(){
        
    }

    bool hasNextDialogue(){
        int nextIndex = dialogueIndex+1;
        if(nextIndex>activeDialogues.Length-1){
            return false;
        }
        return true;
    }

    void nextDialogue(){
        activeDialogues[dialogueIndex].enabled = false;
        dialogueIndex++;
        dialogueIndex = Math.Min(dialogueIndex,activeDialogues.Length-1);
        activeDialogues[dialogueIndex].enabled = true;
        timer = timeForDialogue;
    }

    void showDialogue(){
        timer = timeForDialogue;
        isActiveDialogue = true;
        activeDialogues[dialogueIndex].enabled = true;
    }

    void closeDialogue(){
        isActiveDialogue = false;
        activeDialogues[dialogueIndex].enabled = false;
        resetDialogue();
    }

    void paintedDialogue(){
        activeDialogues[dialogueIndex].enabled = false;
        activeDialogues = dialogueLinesPostPaint;
        activeDialogues[dialogueIndex].enabled = true;
    }
}
