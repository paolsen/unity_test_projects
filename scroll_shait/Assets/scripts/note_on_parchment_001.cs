using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class note_on_parchment_001 : MonoBehaviour
{

    // Use this for initialization
    public Vector2 upperRight;
    public Vector2 lowerLeft;

    public Canvas ui_canvas;
    public RectTransform parchment;
    public GameObject image; 
    public List<Vector2> curve;
    public float curveError;
    public LineRenderer render;
    public Color black;
    public Color red;
    public LineRenderer line3d;

    void Start()
    {
        //ui_canvas = GetComponent<Canvas>();
        //parchment = ui_canvas.GetComponentInChildren<RectTransform>();
        upperRight = parchment.rect.position + parchment.rect.size / 2;
        lowerLeft = parchment.rect.position - parchment.rect.size / 2;
        black.r = 0; black.g = 0; black.b = 0;
        red.r = 255; red.g = 0; red.b = 0;
        line3d.startColor = black;
        line3d.endColor = red;
        line3d.startWidth = 10;
        line3d.endWidth = 10;
        image.SetActive(false);

    }

    float innerProduct(Vector2 x, Vector2 y)
    {
        return x[0] * y[0] + x[1] * y[1];
    }

    bool lineWithin(Vector2 start, Vector2 stop)
    {
        if (lowerLeft[0] > start[0] || start[1] < lowerLeft[1] || upperRight[0] < start[0] || start[1] > upperRight[1])
            return false;
        if (lowerLeft[0] > stop[0] || stop[1] < lowerLeft[1] || upperRight[0] < stop[0] || stop[1] > upperRight[1])
            return false;
        return true;
    }

    void sometimes()
    {
        if (Input.GetMouseButton(0))
        {
            if (curve.Count < 2)
                curve.Add(Input.mousePosition);
            else
            {
                Vector2 last = curve[-1];
                Vector2 before = curve[-2];
                Vector2 now = Input.mousePosition;
                float dotProd = innerProduct((last - before).normalized, (now - last).normalized);
                if ((1 - curveError) <= dotProd || dotProd <= (1 + curveError))
                    curve.Add(Input.mousePosition);
            }
        }
        for (int i = 0; i < curve.Count; i++)
        {
            if (lineWithin(curve[i], curve[i]))
            {
                Vector3 aug;
                aug.x = curve[i].x;
                aug.y = curve[i].y;
                aug.z = curve[i].y;
                line3d.SetPosition(i, aug);
            }
        }
    }

    int counter = 0;
    // Update is called once per frame
    public bool drawing = false;
    void Update()
    {
        if (drawing || Input.GetKey(KeyCode.E))
        {
            drawing = true;
            //image.SetActive(true);
            //counter++;
            //if (counter == 10)
            //{
                sometimes();
                //counter = 0;
            //}
        }
        if (drawing && Input.GetKey(KeyCode.R))
        {
            //image.SetActive(false);
            drawing = false;
        }
        image.SetActive(drawing);

    }
}
