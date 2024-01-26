using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;

public class PaintLineInteraction : MonoBehaviour
{
    bool isDone;

    bool isInRange;

    [Tooltip("The game object that will contain the drawn mesh")]
    public GameObject drawingParent;

    public Canvas interactionCanvas;

    void FixedUpdate()
    {
        if(isInRange && !isDone){
            if(Input.GetKeyDown(KeyCode.F)){
                ShowPaint();
            }
        }
    }

    private void ShowPaint(){
        if(drawingParent==null)
            drawingParent = transform.gameObject;
        LineGenerator.Instance.lineParent =drawingParent;
        LineGenerator.Instance.NewDrawing();
        GameFlowManager.setDrawingState();
        interactionCanvas.gameObject.SetActive(false);
        isDone = true;
    }

    private void OnTriggerEnter(Collider other){

        if(other.tag == "Player" && !isDone){
            isInRange = true;
            interactionCanvas.gameObject.SetActive(true);
        }

    }

    private void OnTriggerExit(Collider other){

        if(other.tag == "Player" && !isDone){
            isInRange = false;
            interactionCanvas.gameObject.SetActive(false);
        }
    }
}
