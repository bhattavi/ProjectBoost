using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    //public float thrust = 100f;
    //public float rotate = 20f;
    // Start is called before the first frame update
    AudioSource audioSource;

    //AudioClip audioClip;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
        
    }
    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            print("THRUST");
            rigidbody.AddRelativeForce(Vector3.up);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void Rotate()
    {
        rigidbody.freezeRotation = true; // freeze rotation when we take control.
        
        if (Input.GetKey(KeyCode.A))
        {
            print("ROTATE LEFT");
            transform.Rotate(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            print("ROTATE RIGHT");
            transform.Rotate(-Vector3.forward);
        }
        rigidbody.freezeRotation = false; // resume physics control of rotation after we are done controlling it.
    }

   
}
