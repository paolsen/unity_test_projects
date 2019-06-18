using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class force_on_ball_001 : MonoBehaviour {

    public Rigidbody rb;
    public Rigidbody forceSource;
    public float gain = 1;
    public float distance;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        //forceSource = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        distance = Vector3.Distance(rb.transform.position, forceSource.transform.position);
        rb.AddForce(0, 1/(distance *distance)*gain,0);
        Vector3 toCenter = (- rb.transform.position + forceSource.transform.position);
        rb.AddForce(distance * toCenter.normalized);
	}
}
