using System;
using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;

public class PaintInteraction : MonoBehaviour
{

    bool isDone;

    bool isInRange;

    GameObject drawMesh;

    [Tooltip("The game object that will contain the drawn mesh")]
    public GameObject drawingParent;

    public Canvas interactionCanvas;

    // Update is called once per frame

    void Start(){
        if(drawMesh==null)
            drawMesh = DrawMeshFull.Instance.gameObject;
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
        DrawManager.activateDrawCanvas();
        DrawMeshFull.Instance.InitializeDrawing(drawingParent);
        interactionCanvas.gameObject.SetActive(false);
        DrawMeshFull.Instance.ActivateDrawCamera();
        GameFlowManager.setDrawingState();
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
