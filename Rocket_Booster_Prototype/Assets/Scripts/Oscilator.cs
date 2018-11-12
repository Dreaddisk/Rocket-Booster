﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscilator : MonoBehaviour
{
    #region Variables
    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
    [SerializeField] float period = 2.0f;

    // todo remove from insepctor later
    [Range(0,1)] [SerializeField] float movementFactor;

    Vector3 startingPos;
    #endregion

    private void Start()
    {
        startingPos = transform.position;
    }

    private void Update()
    {
        if(period <= Mathf.Epsilon) return;


        // set movement factor
        float cycles = Time.time / period;  // grows continually from 0.

        const float tau = Mathf.PI * 2; // about 6.28
        float rawSinWave = Mathf.Sin(cycles * tau); // goes from -1 to +1

        movementFactor = rawSinWave / 2f + 0.5f;

        Vector3 offset = movementFactor * movementVector;
        transform.position = startingPos + offset;
    }

}
