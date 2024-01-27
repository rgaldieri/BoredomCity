using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using Unity.FPS.Game;
using UnityEngine.Diagnostics;
using UnityEngine.Events;

public class LineGenerator : MonoBehaviour
{

    public static LineGenerator Instance { get; private set; }

    public GameObject lineParent;

    public GameObject hud;

    public GameObject spritePrefab;

    Vector3 oldPos = Vector3.zero;

    public GameObject linePrefab;

    public LineUI ui;

    public Camera lineCamera;

    public Line activeLine;

    int numberInLayer = 1;

    public float thickness = 0.1f;

    public Color color = UtilsClass.GetColorFromString("D00000");

    public float spriteYOffset = -0.3f;

    public UnityEvent drawingStarted;

    public UnityEvent drawingEnded;

    PaintLineInteraction interactionOrigin;

    private void Awake()
    {
        Instance = this;
    }

    public void NewDrawing(PaintLineInteraction fromInteraction)
    {
        drawingStarted.Invoke();
        ui.SetContainer(lineParent);
        interactionOrigin = fromInteraction;
        hud.SetActive(true);
        lineCamera.enabled = true;
        oldPos = spritePrefab.transform.position;
        PrepareCamera();
        numberInLayer = 1;
    }

    // Update is called once per frame
    void Update()
    {

        if (UtilsClass.IsPointerOverUI())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject newLine = Instantiate(linePrefab);
            if (lineParent != null)
            {
                newLine.transform.SetParent(lineParent.transform);
            }
            activeLine = newLine.GetComponent<Line>();
            SetThickness();
            SetColor();
        }

        if (Input.GetMouseButtonUp(0))
        {
            activeLine = null;
            numberInLayer++;
        }

        if (activeLine != null)
        {

            Ray ray = lineCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {   
                if (hit.collider.gameObject.CompareTag("DrawArea"))
                {
                    // The collider hit has the specified tag
                    Vector3 hitPosition = hit.point; // The point where the raycast hit the collider
                    activeLine.lineRenderer.sortingOrder = numberInLayer;
                    activeLine.UpdateLine(hitPosition);
                }
            }

            // CHECK IF THE DRAWING HITS THE BOX COLLIDER OF PAINTLINEINTERACTION

            // IF IT DOES; THEN DRAW ON THE COLLIDER POS; ELSE DO NOT

            // activeLine.lineRenderer.sortingOrder = numberInLayer;
            // activeLine.UpdateLine(drawPosition);
        }
    }

    public void SetThickness()
    {
        if (activeLine == null)
            return;
        activeLine.SetThickness(thickness);
    }

    public void SetColor()
    {
        if (activeLine == null)
            return;
        activeLine.SetColor(color);
    }

    public void Save()
    {
        Close();
        Shift();
        drawingEnded.Invoke();
        interactionOrigin.paintDone();
    }

    public void Close(){
        spritePrefab.transform.position = oldPos;
        hud.SetActive(false);
        lineCamera.enabled = false;
        GameFlowManager.INSTANCE.enablePlayerCamera();
        GameFlowManager.setActiveState();
    }

    public void CloseWithoutSaving(){
        if(interactionOrigin!=null)
            interactionOrigin.SetDrawableAgain();
        Close();
    }

    public void PrepareCamera()
    {
        Vector3 pos = lineCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
        pos.z = 0f;
        spritePrefab.transform.position = pos;
    }

    public void Shift()
    {
        Vector3 centre = lineCamera.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height / 2));

        Vector3 fixedCentre = new Vector3(centre.x, centre.y, 0f);

        Vector3 targetCentre = lineParent.transform.position;

        // for each line 
        foreach (Transform child in lineParent.transform)
        {
            if (child.gameObject.tag != "Line")
                continue;

            LineRenderer line = child.gameObject.GetComponent<LineRenderer>();
            for (int i = 0; i < line.positionCount; i++)
            {
                Vector3 position = line.GetPosition(i);
                Vector3 distance = position - fixedCentre;
                Vector3 fixedDistance = new Vector3(distance.x, distance.y + spriteYOffset, distance.z);
                line.SetPosition(i, targetCentre + fixedDistance);
            }
            line.alignment = LineAlignment.TransformZ;
            child.SetParent(lineParent.transform);
            child.localPosition = Vector3.zero;
            child.localRotation = Quaternion.identity;
            Line lineComponent = child.gameObject.GetComponent<Line>();
            lineComponent.InitializePoints();
        }
    }
}
