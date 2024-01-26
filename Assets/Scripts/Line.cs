using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using Unity.FPS.Game;
using UnityEngine.UIElements;

public class Line : MonoBehaviour
{
    // Start is called before the first frame update

    [Tooltip("Minimum distance between points")]
    public float minDistance = 0.01f;

    public LineRenderer lineRenderer;

    public GameFlowManager gameManager;

    public GameObject[] pointsInSpace;

    List<Vector2> points;

    void Awake()
    {
        gameManager = GameFlowManager.INSTANCE;
    }

    public void SetThickness(float thickness)
    {
        lineRenderer.SetWidth(thickness, thickness);
    }

    public void SetColor(Color color)
    {
        lineRenderer.material.color = color;
    }

    private void LateUpdate()
    {
        if (gameManager == null)
            return;
        if (GameFlowManager.GetGameState() == GameState.active && lineRenderer != null && Camera.main != null)
        {
            AdjustLinePointsToFaceCamera();
        }
    }

    public void InitializePoints(){
        int numPositions = lineRenderer.positionCount;
        Vector3[] positions = new Vector3[numPositions];
        lineRenderer.GetPositions(positions);
        pointsInSpace = new GameObject[numPositions];
        for(int i = 0; i<positions.Length;i++){
            Vector3 pos = positions[i];
            GameObject pointGameObject = new GameObject("PointInSpace");
            pointGameObject.transform.SetParent(transform);
            pointGameObject.transform.position = pos;
            pointsInSpace[i] = pointGameObject;
        }
    }

    private void AdjustLinePointsToFaceCamera()
    {
        int numPositions = lineRenderer.positionCount;
        Vector3[] positions = new Vector3[numPositions];
        lineRenderer.GetPositions(positions);

        for(int i = 0; i< positions.Length;i++ ){
            positions[i] = pointsInSpace[i].transform.position;
        }
        lineRenderer.SetPositions(positions);
        // Camera mainCamera = gameManager.GetPlayerCamera();
        // Transform parentTransform = transform.parent.transform;

        // int numPositions = lin   zeRenderer.positionCount;
        // Vector3[] positions = new Vector3[numPositions];
        // lineRenderer.GetPositions(positions);

        // for (int i = 0; i < numPositions; i++)
        // {
        //     Vector3 radiusVector = positions[i] - parentTransform.position; // Vector from center to point
        //     Vector3 parentPosition = new Vector3(positions[i].x, 0f, positions[i].z);
        //     Vector3 cameraPosition = new Vector3(mainCamera.transform.position.x, 0f, mainCamera.transform.position.z);

        //     float angleToFaceCamera = parentTransform.rotation.eulerAngles.y;
        //     Debug.Log("AngleToCamera: " + angleToFaceCamera);

        //     Vector3 direction = (parentPosition - cameraPosition).normalized; // Direction from A to B  

        //     Quaternion lineRotation = Quaternion.LookRotation(direction); // Rotation that looks in the direction of B

        //     float currentRotation =  lineRotation.eulerAngles.y;
        //     Debug.Log("Current rotation: " + currentRotation);
        //     float rotationAngle = angleToFaceCamera - currentRotation;

        //     //Debug.Log("Rotating of: " + rotationAngle);
        //     // Rotate the radius vector around an axis (e.g., Y axis here)
        //     // You can change the axis and angle as needed
        //     Quaternion rotation = Quaternion.AngleAxis(rotationAngle, parentTransform.up); // Rotate 1 degree around Y-axis
        //     Vector3 rotatedVector = rotation * radiusVector;

        //     positions[i] = parentTransform.position + rotatedVector; // Reposition the point
        // }

        // lineRenderer.SetPositions(positions);
    }

    public void UpdateLine(Vector2 position)
    {
        if (points == null)
        {
            points = new List<Vector2>();
            SetPoint(position);
            return;
        }
        
        float distance = Vector2.Distance(points.Last(), position);
        if (distance > minDistance)
        {
            SetPoint(position);
        }
    }


    public void SetPoint(Vector2 point)
    {
        points.Add(point);

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPosition(points.Count - 1, point);
    }

}
