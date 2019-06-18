using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class force_on_ball_002 : MonoBehaviour {


    public Rigidbody rb2;
    public Rigidbody planet;
    public float gain2;
    public float distance2;
    public float startForce;
    Vector3 down = new Vector3(0, -1, 0);



    // Use this for initialization
    void Start()
    {
        rb2 = GetComponent<Rigidbody>();
        rb2.AddForce(startForce, 0, 0);
    }

    // Update is called once per frame



    void Update()
    {
        distance2 = Vector3.Distance(rb2.transform.position, planet.transform.position);
        Vector3 toCenter = (-rb2.transform.position + planet.transform.position);
        rb2.AddForce(gain2 / (distance2 * distance2) * toCenter.normalized);
    }
}


/*
 *      public Rigidbody rb2;
        public Rigidbody planet;
        public float gain2;
        public float distance2;
        public float startForce;
        Vector3 down = new Vector3(0, -1, 0);

        Vector3 cross(Vector3 a, Vector3 b)
        {
            return (new Vector3(a[1] * b[2] - b[1] * a[2], -a[0] * b[2] + b[0] * a[2], a[0] * b[1] - b[0] * a[1]));
        }

        // Use this for initialization
        void Start()
        {
            rb2 = GetComponent<Rigidbody>();
            rb2.AddForce(startForce * cross( down, (-rb2.transform.position + planet.transform.position)));
        }

        // Update is called once per frame



        void Update()
        {
            distance2 = Vector3.Distance(rb2.transform.position, planet.transform.position);
            Vector3 toCenter = (-rb2.transform.position + planet.transform.position);
            rb2.AddForce(gain2 / (distance2 * distance2) * toCenter.normalized);
        }
 * */
