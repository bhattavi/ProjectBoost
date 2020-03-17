using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    //public float thrust = 100f;
    //public float rotate = 20f;
    // Start is called before the first frame update
    AudioSource audioSource;
    [SerializeField] float rcsThrust = 250f;
    [SerializeField] float mainThrust = 50f;
    enum State { Alive, Dying , Transcending};
    State state = State.Alive;
    
    //AudioClip audioClip;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            Thrust();
            Rotate();

        }
        else
        {
            audioSource.Stop();
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) return;
        switch (collision.gameObject.tag)
        {
            case "Friendly":
              
                break;
            case "Finish":
                state = State.Transcending; 

                Invoke("LoadNewScene", 1f);
                break;
            default:
                state = State.Dying;
                Invoke("LoadFirstScene", 10f);
                break;
        }
    }
    private void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNewScene()
    {
        SceneManager.LoadScene(1);
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //print("THRUST");
            rigidbody.AddRelativeForce(Vector3.up * mainThrust);
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
        float rotationThisFrame = rcsThrust * Time.deltaTime;
        
        if (Input.GetKey(KeyCode.A))
        {
            //print("ROTATE LEFT");
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //print("ROTATE RIGHT");
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
        rigidbody.freezeRotation = false; // resume physics control of rotation after we are done controlling it.
    }

   
}
