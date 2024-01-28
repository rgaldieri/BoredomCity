using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using Unity.VisualScripting;
using UnityEngine;

public class PosterRotationFixer : MonoBehaviour
{

    public GameObject poster;

    public bool isDrawing;

    public Vector3 defaultRotation;

    public void Start(){
        LineGenerator.Instance.drawingStarted.AddListener(() => { drawingStarted();});
        LineGenerator.Instance.drawingEnded.AddListener(() => { drawingEnded();});
        defaultRotation = poster.transform.rotation.eulerAngles;
    }

    void Update(){
        if(isDrawing){
            Rotate();
            return;
        }
        Reset();
    }

    public void Reset(){
        poster.transform.rotation = Quaternion.Euler(defaultRotation);
    }

    public void drawingStarted(){
        isDrawing = true;
    }

    public void drawingEnded(){
        isDrawing=false;
    }

    public void Rotate(){
        poster.transform.rotation = Quaternion.identity;
    }

}
