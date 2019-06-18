using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class time_based_symbol : MonoBehaviour {
    private new Camera camera;
    public Material lineMaterial;
    public float lineWidth = 0.02f;
    public Vector3? lineStartPoint = null; // nullable vector 
    public float depth = 0.33f;
    private List<Vector3?> symbol = new List<Vector3?>();
    public bool mouse_down = false;
    public float sampleRate = 0.05f;
    private float nextSample = 0.0f;

    // Use this for initialization
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && mouse_down == false)
        {
            mouse_down = true;
            var lineStartPoint = GetMouseCameraPoint();
            symbol.Add(lineStartPoint);
            nextSample = Time.time + sampleRate;

        }
        else if (Input.GetMouseButtonUp(0))
        {
            mouse_down = false;
            if (symbol.Count > 1)
            {
                var gameObject = new GameObject();
                var lineRenderer = gameObject.AddComponent<LineRenderer>();
                lineRenderer.material = lineMaterial;
                // lineRenderer.positionCount = 2;
                lineRenderer.SetPositions(new Vector3[] { symbol[symbol.Count - 2].Value, symbol[symbol.Count - 1].Value });
                lineRenderer.startWidth = lineWidth;
                lineRenderer.endWidth = lineWidth;
            }
            return;
        }
        else if(mouse_down == true)
        {
            // start timer;
            if(Time.time > nextSample)
            {
                nextSample += sampleRate;
                var lineStartPoint = GetMouseCameraPoint();
                symbol.Add(lineStartPoint);
                if (symbol.Count > 1)
                {
                    var gameObject = new GameObject();
                    var lineRenderer = gameObject.AddComponent<LineRenderer>();
                    lineRenderer.material = lineMaterial;
                    // lineRenderer.positionCount = 2;
                    lineRenderer.SetPositions(new Vector3[] { symbol[symbol.Count - 2].Value, symbol[symbol.Count - 1].Value });
                    lineRenderer.startWidth = lineWidth;
                    lineRenderer.endWidth = lineWidth;
                }
                return;
            }

        }
        else
        {
            mouse_down = false;
        }
    }
    private Vector3 GetMouseCameraPoint()
    {
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        return ray.origin + ray.direction * depth;
    }
}
