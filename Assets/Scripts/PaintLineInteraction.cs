using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;

public class PaintLineInteraction : MonoBehaviour
{
    public bool isDone;

    public bool isInRange;

    bool showPaint;

    public GameObject prefab;

    public Collider boxCollider;

    public Collider interactionCollider;

    [Tooltip("The game object that will contain the drawn mesh")]
    public GameObject drawingParent;

    Canvas interactionCanvas;

    void Start(){
        interactionCanvas = DrawInteractionCanvas.Instance.canvas;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.F)){
            if(isInRange && !isDone){
                showPaint = true;
            }
        }
    }

    void FixedUpdate()
    {
        if(showPaint && !isDone){
            ShowPaint();
        }
    }

    public void SetDrawableAgain(){
        showPaint=false;
        isDone=false;
    }

    private void ShowPaint(){
        if(drawingParent==null)
            drawingParent = transform.gameObject;
        LineGenerator.Instance.lineParent =drawingParent;
        LineGenerator.Instance.spritePrefab = prefab;
        LineGenerator.Instance.NewDrawing(this);
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
