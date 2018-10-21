using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    #region Variables
    private Rigidbody rigidBody;
    AudioSource audioSource;

    #endregion

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        ProcessInput();


    }

    private void ProcessInput()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            //            rigidBody.AddRelativeForce(Vector3.left);
            //           rigidBody.AddRelativeTorque(Vector3.up);
            transform.Rotate(Vector3.forward);
            Debug.Log("Going to the left");
        }

        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            //            rigidBody.AddRelativeForce(Vector3.right);
            //            rigidBody.AddRelativeTorque(Vector3.down);
            transform.Rotate(Vector3.back);
            Debug.Log("Going to the right");
        }

        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            audioSource.Stop();
        }
    }

} // main class
