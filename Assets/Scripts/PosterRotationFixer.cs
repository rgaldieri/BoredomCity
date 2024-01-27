using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using Unity.VisualScripting;
using UnityEngine;

public class PosterRotationFixer : MonoBehaviour
{
    public GameObject poster;

    Quaternion defaultRotation;

    public void Start(){
        LineGenerator.Instance.drawingStarted.AddListener(() => { Rotate();});
        LineGenerator.Instance.drawingEnded.AddListener(() => { Reset();});
        defaultRotation = poster.transform.rotation;
    }

    public void Reset(){
        poster.transform.rotation = defaultRotation;
    }

    public void Rotate(){
        poster.transform.rotation = Quaternion.identity;
    }

}
