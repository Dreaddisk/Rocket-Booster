using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    #region Variables

    [Header("Rocket Stats")]
    [SerializeField] float rcsThrust = 10f;
    [SerializeField] float mainThrust = 10f;

    [Header("SoundClips")]
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip rocketeath;
    [SerializeField] AudioClip levelLoad;

    [Header("FVX")]
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;

    private Rigidbody rigidBody;
    AudioSource audioSource;

    enum State
    {
        DYING,
        ALIVE,
        TRANSCENDING
    }

    State state = State.ALIVE;

    #endregion

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }
    private void Update()
    {
        if(state == State.ALIVE)
        {
            RespondToRotateInput();
            RespondToThrustInput();
        }
    }

    private void RespondToRotateInput()
    {
        RocketRotation();
    }

    private void RocketRotation()
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

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void ApplyThrust()
    {
        float thrustPower = mainThrust * Time.deltaTime;
        rigidBody.AddRelativeForce(Vector3.up * thrustPower);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        mainEngineParticles.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.ALIVE)   return;


        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You are in a freindly position");
                break;

            case "Deadly":
                StartDeathSequence();
                deathParticles.Play();
                break;

            case "Finish":
                StartSuccessSequence();

                break;
            default:
                Debug.Log("You are not standing in any platform");
                break;
        }
    }

    private void StartSuccessSequence()
    {
        audioSource.PlayOneShot(levelLoad);
        state = State.TRANSCENDING;
        successParticles.Play();
        Invoke("LoadNextScene", 1f);
    }

    private void StartDeathSequence()
    {
        audioSource.PlayOneShot(rocketeath);
        state = State.DYING;
        deathParticles.Play();
        Invoke("LoadFirstLevel", 2f);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("Scene_Game_2"); // todo allow for more scenes
    }

    void LoadFirstLevel()
    {
        SceneManager.LoadScene("Scene_Game_1");
    }
} // main class
