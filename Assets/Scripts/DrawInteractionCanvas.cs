using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawInteractionCanvas : MonoBehaviour
{
    public static DrawInteractionCanvas Instance { get; private set; }
    
    public Canvas canvas;

    void Awake(){
        Instance = this;
    }    
}
