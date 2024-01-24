using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using Unity.FPS.Game;
using UnityEditor.Animations;

public class DrawMeshFull : MonoBehaviour {

    public static DrawMeshFull Instance { get; private set; }

    [SerializeField] private Material drawMeshMaterial;

    public GameObject drawingContainer = null;

    public Camera drawCamera;

    public DrawMeshUI meshUI;

    private GameObject lastGameObject;
    private int lastSortingOrder;
    private Mesh mesh;
    private Vector3 lastMouseWorldPosition;
    private float lineThickness = 0.02f;
    private Color lineColor = UtilsClass.GetColorFromString("D00000");

    private void Awake() {
        Instance = this;
        // Find Camera as a Sibling
    }

    public void InitializeDrawing(GameObject parent){
        drawingContainer = parent;
        meshUI.drawingContainer = parent;
    }

    public void ActivateDrawCamera(){
        GameFlowManager.INSTANCE.disablePlayerCamera();
        drawCamera.enabled=true;
    }

    public void DeactivateDrawCamera(){
        drawCamera.enabled=false;
        GameFlowManager.INSTANCE.enablePlayerCamera();

    }

    private void Update() {
        if(GameFlowManager.currentState!=GameState.drawing)
            return;
        if (!UtilsClass.IsPointerOverUI()) {
            // Only run logic if not over UI
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            if (Input.GetMouseButtonDown(0)) {
                // Mouse Down
                CreateMeshObject();
                mesh = MeshUtils.CreateMesh(mouseWorldPosition, mouseWorldPosition, mouseWorldPosition, mouseWorldPosition);
                mesh.MarkDynamic();
                lastGameObject.GetComponent<MeshFilter>().mesh = mesh;
                Material material = new Material(drawMeshMaterial);
                material.color = lineColor;
                lastGameObject.GetComponent<MeshRenderer>().material = material;
            }

            if (Input.GetMouseButton(0)) {
                // Mouse Held Down
                float minDistance = 0.001f;
                if (Vector2.Distance(lastMouseWorldPosition, mouseWorldPosition) > minDistance) {
                    // Far enough from last point
                    Vector2 forwardVector = (mouseWorldPosition - lastMouseWorldPosition).normalized;

                    lastMouseWorldPosition = mouseWorldPosition;

                    MeshUtils.AddLinePoint(mesh, mouseWorldPosition, lineThickness);
                }
            }

            if (Input.GetMouseButtonUp(0)) {
                // Mouse Up
                MeshUtils.AddLinePoint(mesh, mouseWorldPosition, 0f);
            }
        }
    }

    private void CreateMeshObject() {
        lastGameObject = new GameObject("SingleStrokeMesh", typeof(MeshFilter), typeof(MeshRenderer));
        if(drawingContainer!=null){
            drawingContainer.transform.rotation = Quaternion.identity;
            lastGameObject.transform.SetParent(drawingContainer.transform);
        }
        lastSortingOrder++;
        lastGameObject.GetComponent<MeshRenderer>().sortingOrder = lastSortingOrder;
    }

    public void SetThickness(float lineThickness) {
        this.lineThickness = lineThickness;
    }

    public void SetColor(Color lineColor) {
        this.lineColor = lineColor;
    }

    public void moveMesh(GameObject container){

        Vector3 targetPosition = container.transform.position;

        Vector3 sourceCenter = calculateCenterOfChildren(container);

        Debug.Log("Target Position" + targetPosition);
        Debug.Log("Source Center" + sourceCenter);
        foreach(Transform child in drawingContainer.transform){
            Mesh mesh = child.gameObject.GetComponent<MeshFilter>().mesh;
            Vector3[] vertices = mesh.vertices;

             for (int i = 0; i < vertices.Length; i++) {
                Vector3 vertexToCentre = vertices[i] - sourceCenter;
                Vector3 finalTargetPosition = targetPosition + vertexToCentre;
                Vector3 translationVector = finalTargetPosition - vertices[i];
                vertices[i] += translationVector;
            }
            mesh.vertices = vertices;
            mesh.RecalculateBounds();
            //child.transform.rotation = container.transform.rotation;
            //child.transform.position = targetPosition;
        }

    }

    private Vector3 calculateCenter(Vector3[] vertices){
        Vector3 sumOfVertices = Vector3.zero;
        foreach (Vector3 vertex in vertices) {
            sumOfVertices += vertex;
        }
        Vector3 centroid = sumOfVertices / vertices.Length;
        return centroid;
    }

    private Vector3 calculateCenterOfChildren(GameObject obj){
        Vector3 sumOfCentroids = Vector3.zero;
        foreach(Transform child in drawingContainer.transform){
            Mesh mesh = child.gameObject.GetComponent<MeshFilter>().mesh;
            sumOfCentroids += calculateCenter(mesh.vertices);
        }
        return sumOfCentroids / drawingContainer.transform.childCount;
    }

}