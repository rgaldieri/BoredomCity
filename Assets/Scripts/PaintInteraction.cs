using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintInteraction : MonoBehaviour
{

    bool isInRange;

    public Canvas interactionCanvas;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isInRange){
            if(Input.GetKeyDown(KeyCode.F)){
                ShowPaint();
            }
        }
    }


    private void ShowPaint(){
        Debug.Log("ENTRA!");
    }

    private void OnTriggerEnter(Collider other){

        if(other.tag == "Player"){
            isInRange = true;
            interactionCanvas.gameObject.SetActive(true);
        }

    }

    private void OnTriggerExit(Collider other){

        if(other.tag == "Player"){
            isInRange = false;
            interactionCanvas.gameObject.SetActive(false);
        }
    }

}
