using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using Unity.VisualScripting;
using UnityEngine;

public class PosterRotationFixer : MonoBehaviour
{

    public GameObject poster;

    public bool lookAt;

    public Vector3 defaultRotation;

    public PaintLineInteraction paint;

    public void Start(){
        LineGenerator.Instance.drawingStarted.AddListener(() => { Rotate();});
        LineGenerator.Instance.drawingEnded.AddListener(() => { enableLookAt();});
        // paint = transform.Find("PaintLineInteraction").GetComponent<PaintLineInteraction>();
        // paint.paintingDone.AddListener(() => { enableLookAt();});
        defaultRotation = poster.transform.rotation.eulerAngles;
    }

    void Update(){

        // TROVA COMBINAZIONE
        if(lookAt)
            Reset();
    }

    public void Reset(){
        poster.transform.rotation = Quaternion.Euler(defaultRotation);
   }

    public void enableLookAt(){
        lookAt=true;
    }

    public void Rotate(){
        poster.transform.rotation = Quaternion.identity;
    }

}
