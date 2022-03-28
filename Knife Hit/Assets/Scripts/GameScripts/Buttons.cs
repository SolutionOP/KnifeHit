using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    /// <summary>
    /// Restart first scene
    /// </summary>
    public void RestartGameScene()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Go to game scene from menu
    /// </summary>
    public void GoToGameScene()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Go to main menu
    /// </summary>
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
