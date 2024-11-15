using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public void RetryGame()
    {
        SceneManager.LoadScene("Game"); // Replace with your actual Game Scene name
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu"); // Replace with your Menu Scene name
    }
}
