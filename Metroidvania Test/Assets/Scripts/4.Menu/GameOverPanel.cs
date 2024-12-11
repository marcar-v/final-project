using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{


    public void RestartGame()
    {
        TransitionController._transitionInstance.ReloadCurrentScene();
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
