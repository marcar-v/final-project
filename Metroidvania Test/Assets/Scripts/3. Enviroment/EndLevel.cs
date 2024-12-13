using System.Collections;
using System.Collections.Generic;
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

            int _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            if(_currentSceneIndex == 3)
            {
                TransitionController._transitionInstance.RestartLevel1();
            }

            else
            {
                TransitionController._transitionInstance.LoadNextScene();
            }
        }
    }
}
