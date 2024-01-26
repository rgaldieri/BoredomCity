using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using Unity.FPS.Game;
using UnityEngine.Diagnostics;

public class LineGenerator : MonoBehaviour
{

    public static LineGenerator Instance { get; private set; }

    public GameObject lineParent;

    public GameObject hud;

    public GameObject linePrefab;

    public LineUI ui;

    public Camera lineCamera;

    public Line activeLine;

    int numberInLayer = 1;

    public float thickness = 0.1f;

    public Color color = UtilsClass.GetColorFromString("D00000");

    private void Awake()
    {
        Instance = this;
    }

    public void NewDrawing(){   
        ui.SetContainer(lineParent);
        hud.SetActive(true);
        lineCamera.enabled=true;
        numberInLayer = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(UtilsClass.IsPointerOverUI()){
            return;
        }

        if( Input.GetMouseButtonDown(0)){
            GameObject newLine = Instantiate(linePrefab);
            if(lineParent!=null){
                newLine.transform.SetParent(lineParent.transform);
            }
            activeLine = newLine.GetComponent<Line>();
            SetThickness();
            SetColor();
        }

        if(Input.GetMouseButtonUp(0)){
            activeLine = null;
            numberInLayer++;
        }

        if(activeLine!=null){

            Vector3 worldPosition = lineCamera.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0;
            activeLine.lineRenderer.sortingOrder = numberInLayer;
            activeLine.UpdateLine(worldPosition);
        }
    }

    public void SetThickness(){
        if(activeLine==null)
            return;
        activeLine.SetThickness(thickness);
    }

    public void SetColor(){
        if(activeLine==null)
            return;
        activeLine.SetColor(color);
    }

    public void Save(){
        Shift();
        hud.SetActive(false);
        lineCamera.enabled=false;
        GameFlowManager.INSTANCE.enablePlayerCamera();
        GameFlowManager.setActiveState();
    }

    public void Shift(){
        Vector3 centre = lineCamera.ScreenToWorldPoint(new Vector2(Screen.width/2, Screen.height/2));
        
        Vector3 fixedCentre = new Vector3(centre.x, centre.y, 0f);

        Vector3 targetCentre = lineParent.transform.position;

        foreach (Transform child in lineParent.transform){
            if(child.gameObject.tag!="Line")
                continue;
            child.localPosition = Vector3.zero;
            child.rotation = Quaternion.identity;

            LineRenderer line = child.gameObject.GetComponent<LineRenderer>();
            for(int i = 0; i < line.positionCount;i++){
                Vector3 position = line.GetPosition(i);
                Vector3 distance = position - fixedCentre;
                line.SetPosition(i, targetCentre + distance);
            }
            line.alignment = LineAlignment.TransformZ;
            child.SetParent(lineParent.transform);
            child.localPosition = Vector3.zero;
            child.localRotation = Quaternion.identity;
            line.alignment = LineAlignment.TransformZ;
            Line lineComponent = child.gameObject.GetComponent<Line>();
            lineComponent.InitializePoints();
        }
    }
}
