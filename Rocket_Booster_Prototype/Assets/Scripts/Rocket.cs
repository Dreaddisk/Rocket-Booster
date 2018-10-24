using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    #region Variables

    [SerializeField] float rcsThrust = 10f;
    [SerializeField] float mainThrust = 10f;
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
        Rotate();
        Thrust();

    }

    private void Rotate()
    {

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            float rotationLeft = rcsThrust * Time.deltaTime;
            transform.Rotate(Vector3.forward * rotationLeft);
 //           Debug.Log("Going to the left");
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            float rotationRight = rcsThrust * Time.deltaTime;
            transform.Rotate(Vector3.back * rotationRight);
 //           Debug.Log("Going to the right");
        }
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            float thrustPower = mainThrust * Time.deltaTime;
            rigidBody.AddRelativeForce(Vector3.up * thrustPower);
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

    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You are in a freindly position");
                break;

            case "Deadly":
                Debug.Log("You are in the DANGER ZONE");
                break;
            default:
                Debug.Log("You are not standing in any platform");
                break;
        }
    }


} // main class
