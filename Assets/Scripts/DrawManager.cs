using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{

    public static DrawManager INSTANCE { get; private set; }

    public static GameObject drawCanvas;

    void Awake(){
        INSTANCE=this;
        drawCanvas = transform.Find("DrawCanvas").gameObject;
    }

    public static void activateDrawCanvas(){
        drawCanvas.SetActive(true);
    }

    public static void deactivateDrawCanvas(){
        drawCanvas.SetActive(false);
    }
    
}
