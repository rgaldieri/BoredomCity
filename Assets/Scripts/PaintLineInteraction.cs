using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;

public class PaintLineInteraction : MonoBehaviour
{
    bool isDone;

    bool isInRange;

    public GameObject prefab;

    [Tooltip("The game object that will contain the drawn mesh")]
    public GameObject drawingParent;

    Canvas interactionCanvas;

    void Start(){
        interactionCanvas = DrawInteractionCanvas.Instance.canvas;
    }

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
        LineGenerator.Instance.spritePrefab = prefab;
        LineGenerator.Instance.NewDrawing();
        GameFlowManager.setDrawingState();
        interactionCanvas.enabled=false;
        isDone = true;
    }

    private void OnTriggerEnter(Collider other){

        if(other.tag == "Player" && !isDone){
            isInRange = true;
            interactionCanvas.enabled=true;

        }

    }

    private void OnTriggerExit(Collider other){

        if(other.tag == "Player" && !isDone){
            isInRange = false;
            interactionCanvas.enabled=false;
        }
    }


}
