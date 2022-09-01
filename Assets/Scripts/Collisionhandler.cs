using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collisionhandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip death;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;


    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;



    void Start()
    {
        audioSource = GetComponent<AudioSource>();      
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;  //toggle collision
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(isTransitioning || collisionDisabled) { return; }

        
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friednly");
                break;
            case "Finish":
                StartSuccessSequence();
                break ;
            default:
                StartCrashSequence();
                break;

        }
    }

    void StartSuccessSequence()
    {
        isTransitioning = true; 
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        deathParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel",levelLoadDelay);

    }

    void LoadNextLevel()
    {
        GetComponent<Movement>().enabled = false;

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
