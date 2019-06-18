using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class LineClickFromVideo : MonoBehaviour {
    private new Camera camera;
    public Material lineMaterial;
    public float lineWidth;
    public Vector3? lineStartPoint = null; // nullable vector 
    public float depth =5f;

	// Use this for initialization
	void Start () {
        camera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            lineStartPoint = GetMouseCameraPoint();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if(!lineStartPoint.HasValue)
            {
                return;
            }
            var lineEndPoint =  GetMouseCameraPoint();
            var gameObject = new GameObject();
            var lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.material = lineMaterial;
            // lineRenderer.positionCount = 2;
            lineRenderer.SetPositions(new Vector3[] { lineStartPoint.Value, lineEndPoint});
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
            //var ligth = gameObject.AddComponent<Light>();
            lineStartPoint = null;
        }
	}
    private Vector3 GetMouseCameraPoint()
    {
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        return ray.origin + ray.direction * depth;
    }
}
