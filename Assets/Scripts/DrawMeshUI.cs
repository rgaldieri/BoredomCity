using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using Unity.FPS.Game;

public class DrawMeshUI : MonoBehaviour {

    public GameObject drawingContainer;

    public GameObject parentPrefab;

    private void Awake() {
        transform.Find("Thickness1Btn").GetComponent<Button>().onClick.AddListener(() => { SetThickness(0.01f); });
        transform.Find("Thickness2Btn").GetComponent<Button>().onClick.AddListener(() => { SetThickness(0.02f); });
        transform.Find("Thickness3Btn").GetComponent<Button>().onClick.AddListener(() => { SetThickness(0.04f); });
        transform.Find("Thickness4Btn").GetComponent<Button>().onClick.AddListener(() => { SetThickness(0.08f); });

        transform.Find("Color1Btn").GetComponent<Button>().onClick.AddListener(() => { SetColor(UtilsClass.GetColorFromString("7F50B0")); });
        transform.Find("Color2Btn").GetComponent<Button>().onClick.AddListener(() => { SetColor(UtilsClass.GetColorFromString("39A0ED")); });
        transform.Find("Color3Btn").GetComponent<Button>().onClick.AddListener(() => { SetColor(UtilsClass.GetColorFromString("F5D491")); });
        transform.Find("Color4Btn").GetComponent<Button>().onClick.AddListener(() => { SetColor(UtilsClass.GetColorFromString("4E937A")); });
        transform.Find("Color5Btn").GetComponent<Button>().onClick.AddListener(() => { SetColor(UtilsClass.GetColorFromString("D00000")); });

        transform.Find("ResetBtn").GetComponent<Button>().onClick.AddListener(() => { ResetDrawing(); });
        transform.Find("UndoBtn").GetComponent<Button>().onClick.AddListener(() => { UndoLastDrawing(); });
        transform.Find("SaveBtn").GetComponent<Button>().onClick.AddListener(() => { Save(); });

    }

    private void ResetDrawing(){
        if(drawingContainer==null){
            transform.Find("ResetBtn").GetComponent<Button>().enabled = false;
            return;
        }
        foreach (Transform child in drawingContainer.transform){
            Destroy(child.gameObject);
        }
            
    }

    private void UndoLastDrawing(){
        if(drawingContainer==null){
            transform.Find("UndoBtn").GetComponent<Button>().enabled = false;
            return;
        }
    
        int children = drawingContainer.transform.childCount;
        if(children==0)
            return;
            
        GameObject lastStroke = drawingContainer.transform.GetChild(children-1).gameObject;
        Destroy(lastStroke);
    }

    private void Save(){
        if(parentPrefab.activeSelf)
            parentPrefab.SetActive(false);
        DrawManager.deactivateDrawCanvas();
        GameFlowManager.setActiveState();
        DrawMeshFull.Instance.DeactivateDrawCamera();
        DrawMeshFull.Instance.moveMesh(drawingContainer);
    }

    private void SetThickness(float thickness) {
        DrawMeshFull.Instance.SetThickness(thickness);
    }

    private void SetColor(Color color) {
        DrawMeshFull.Instance.SetColor(color);
    }

}