using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    // Start is called before the first frame update
    AudioSource audioSource;
    [SerializeField] float rcsThrust = 300f;
    [SerializeField] float mainThrust = 2000f;
    [SerializeField] float levelLoadDelay = 2f;

    [SerializeField] AudioClip engineSound;
    [SerializeField] AudioClip deadSound;
    [SerializeField] AudioClip winSound;

    [SerializeField] ParticleSystem engineParticles;
    [SerializeField] ParticleSystem deadParticles;
    [SerializeField] ParticleSystem winParticles;



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
            RespondToThrustInput();
            RespondToRotateInput();

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
                StartWinSeq();
                break;
            default:
                StartDeathSeq();
                break;
        }
    }

    private void StartDeathSeq()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(deadSound);
        deadParticles.Play();
        Invoke("LoadFirstScene", levelLoadDelay);
    }

    private void StartWinSeq()
    {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(winSound);
        winParticles.Play();
        Invoke("LoadNewScene", levelLoadDelay);
    }

    private void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNewScene()
    {
        SceneManager.LoadScene(1);
    }

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
            engineParticles.Stop();
        }
    }

    private void ApplyThrust()
    {
        rigidbody.AddRelativeForce(Vector3.up * mainThrust*Time.deltaTime);
        if (!audioSource.isPlaying)
        {


            audioSource.PlayOneShot(engineSound);
        }
        engineParticles.Play();
    }

    private void RespondToRotateInput()
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
