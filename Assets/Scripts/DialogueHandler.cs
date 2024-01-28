using System;
using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;

public class DialogueHandler : MonoBehaviour
{
    public SpriteRenderer[] dialogueLines;

    public SpriteRenderer[] dialogueLinesPostPaint;
    public SpriteRenderer[] activeDialogues;
    public PaintLineInteraction interaction;

    public bool isActiveDialogue;

    public float dialogueRange;

    private int dialogueIndex = 0;

    public float timeForDialogue;
    public float timer;

    public bool isDrawing;

    void FixedUpdate(){
        checkDialogue();
        if(hasNextDialogue() && isActiveDialogue){
            if(timer>0)
                timer = timer-Time.fixedDeltaTime;
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
        //interaction.playerInRange.AddListener(() => {showDialogue();});
        //interaction.playerOutOfRange.AddListener(() => {closeDialogue();});
        timer = timeForDialogue;
        if(dialogueLines.Length>0)
            activeDialogues = dialogueLines;
    }

    void resetDialogue(){
        dialogueIndex = 0;
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
        isActiveDialogue = true;
        activeDialogues[dialogueIndex].enabled = true;
    }

    void closeDialogue(){   
        isActiveDialogue = false;
        if(activeDialogues!=null)
            activeDialogues[dialogueIndex].enabled = false;
    }

    void paintedDialogue(){
        activeDialogues[dialogueIndex].enabled = false;
        resetDialogue();
        activeDialogues = dialogueLinesPostPaint;
        activeDialogues[dialogueIndex].enabled = true;
    }

    public void checkDialogue(){
        if(isInDialogueRange()){
            showDialogue();
            return;
        }
        closeDialogue();
    }

    public bool isInDialogueRange(){
        return Vector3.Distance(transform.position, GameFlowManager.INSTANCE.playerCamera.transform.position) <= dialogueRange;
    }
}
