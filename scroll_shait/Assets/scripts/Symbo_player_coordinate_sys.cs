using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Symbo_player_coordinate_sys : MonoBehaviour {
    public GameObject player;
    private new Camera camera;
    public Material lineMaterial;
    public float lineWidth = 0.02f;
    public Vector3? lineStartPoint = null; // nullable vector 
    public float depth = 0.33f;
    private List<Vector3?> symbol = new List<Vector3?>();
    public bool mouse_down = false;
    public float sampleRate = 0.05f;
    private float nextSample = 0.0f;
    public Vector3 pos;
    public Quaternion rot;
    public Vector3 displacement = new Vector3(0, 0, 0);
    private Vector3 fo;
    private List<GameObject> objects = new List<GameObject>();
    // Use this for initialization
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && mouse_down == false)
        {
            mouse_down = true;
            var lineStartPoint = GetMouseCameraPoint();
            symbol.Add(lineStartPoint);
            nextSample = Time.time + sampleRate;

        }
        else if (Input.GetMouseButtonUp(1))
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
                objects.Add(gameObject);
                

            }
            return;
        }
        else if (mouse_down == true)
        {
            // start timer;
            if (Time.time > nextSample)
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
                    objects.Add(gameObject);

                }
                return;
            }

        }
        else
        {
            mouse_down = false;
        }
        // positions and rotations of each element is in player coordinate frame
        rot = player.transform.rotation;
        for (int lineIndex =0; lineIndex < symbol.Count; lineIndex++)
        {
            objects[lineIndex].transform.SetPositionAndRotation(player.transform.position + displacement, rot);
        }

    }
    private Vector3 GetMouseCameraPoint()
    {
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        return ray.origin + ray.direction * depth;
    }
}
