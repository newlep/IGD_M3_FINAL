using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Function to load the Game Scene
    public void PlayGame()
    {
        SceneManager.LoadScene("Game"); // Replace with the exact name of your game scene
    }

    // Function to quit the application
    public void ExitGame()
    {
        Debug.Log("Exit Game"); // This will log in the editor
        Application.Quit();    // This will close the application in a build
    }
}
