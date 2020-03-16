using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    public float thrust = 100f;
    public float rotate = 20f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            print("THRUST");
            rigidbody.AddRelativeForce(new Vector3(0, thrust * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.A))
        {
            print("ROTATE LEFT");
            transform.Rotate(new Vector3(0, 0, rotate * Time.deltaTime));
        }
        else if (Input.GetKey(KeyCode.D))
        {
            print("ROTATE RIGHT");
            transform.Rotate(new Vector3(0, 0, -rotate * Time.deltaTime));
        }
    }
}
