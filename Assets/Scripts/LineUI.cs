using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class LineUI : MonoBehaviour
{

    public GameObject drawingContainer;

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
        transform.Find("ExitBtn").GetComponent<Button>().onClick.AddListener(() => { ExitNoSave(); });
    }

    public void SetContainer(GameObject container){
        drawingContainer = container;
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
        LineGenerator.Instance.Save();        
    }

    private void SetThickness(float thickness) {
        LineGenerator.Instance.thickness=thickness;
    }

    private void SetColor(Color color) {
        LineGenerator.Instance.color= color;
    }

    private void ExitNoSave(){
        ResetDrawing();
        LineGenerator.Instance.CloseWithoutSaving();
    }

}
