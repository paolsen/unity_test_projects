using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSymbol : MonoBehaviour {

    // Use this for initialization
    public GameObject player;
    public float difference = 0;
    public int counter = 0;
    Vector3 lastScreenPoint;
    Vector3 thisScreenPoint = new Vector3(0, 0, 0);
    public Vector3 localOffset;
    public float x;
    public float y;
    public float z;
    GameObject myLine;
    //public float X_sens = player.GetComponentInParent<MouseLook>().XSensitivity;
    void Start () {
		
	}
    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
    {
        myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.useWorldSpace = false;
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.SetColors(color, color);
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);

    }
 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("x") && Input.GetButton("Fire1"))//  && Input.GetButton("Fire1")
        {

                if (counter == 0)
            {
                lastScreenPoint = Input.mousePosition;
                counter++;
            }
            else
            {
                lastScreenPoint = thisScreenPoint;
            }
            thisScreenPoint = Input.mousePosition;

            if (counter > 0 && (thisScreenPoint - lastScreenPoint).magnitude > difference)
            {   
                //x = Input.mousePosition.x;
                //y = Input.mousePosition.y;
                //z = Input.mousePosition.z;
                //var worldOffset = transform.rotation * localOffset;
                //lastScreenPoint.Scale(new Vector3(0.01f, 0.01f, 0.01f));
                //thisScreenPoint.Scale(new Vector3(0.01f, 0.01f, 0.01f));
                //DrawLine(lastScreenPoint + transform.position + worldOffset, thisScreenPoint + transform.position + worldOffset, Color.red);
                //counter++;
            }
            counter++;
            
        }
        else
        {
            
        }
    }
 }
