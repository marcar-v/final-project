using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionController : MonoBehaviour
{
    private Animator _transitionAnimator;
    [SerializeField] float _transitionTime;

    public static TransitionController _transitionInstance;

    private void Awake()
    {
        if (_transitionInstance == null)
        {
            _transitionInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _transitionAnimator = GetComponentInChildren<Animator>();
    }
    public void LoadNextScene()
    {
        int _nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(SceneLoad(_nextSceneIndex));
    }

    public void RestartLevel1()
    {
        int _firstScene = SceneManager.GetActiveScene().buildIndex - 2;
        StartCoroutine(SceneLoad(1));
    }

    public IEnumerator SceneLoad(int sceneIndex)
    {
        //Trigger para reproducir efecto fade in
        _transitionAnimator.SetTrigger("Start");
        //Esperar un segundo
        yield return new WaitForSeconds(_transitionTime);
        //Cargar escena
        SceneManager.LoadScene(sceneIndex);
        _transitionAnimator.SetTrigger("End");
    }

    public void ReloadCurrentScene()
    {
        int _nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(ReloadScene());
    }

    public IEnumerator ReloadScene()
    {
        //Trigger para reproducir efecto fade in
        _transitionAnimator.SetTrigger("Start");
        //Esperar un segundo
        yield return new WaitForSeconds(_transitionTime);
        //Cargar escena
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        _transitionAnimator.SetTrigger("End");
    }
}
