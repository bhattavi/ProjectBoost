using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
    [Range(0,1)][SerializeField]float movementFactor;
    // this value will be between 0 and 1
    [SerializeField] float period = 2f;

    Vector3 startingPos;
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (period <= Mathf.Epsilon) return;
        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2;

        float rawSinWave = Mathf.Sin(cycles * tau);

        movementFactor = rawSinWave / 2f + 0.5f;
       
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
