using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    AudioSource _teleportSound;
    private void Awake()
    {
        _teleportSound = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            _teleportSound.Play();
            TransitionController._transitionInstance.LoadNextScene();
        }
    }
}
