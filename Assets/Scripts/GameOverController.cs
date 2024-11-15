using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Required for TextMeshPro

public class GameOverController : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText; // Reference to the TextMeshPro element for displaying the score

    void Start()
    {
        // Retrieve the final score from PlayerPrefs
        float finalScore = PlayerPrefs.GetFloat("FinalScore", 0);

        // Display the final score
        if (finalScoreText != null)
        {
            finalScoreText.text = "Score: " + Mathf.FloorToInt(finalScore).ToString();
        }
        else
        {
            Debug.LogWarning("FinalScoreText is not assigned in the Inspector!");
        }
    }

    public void RetryGame()
    {
        SceneManager.LoadScene("Game"); // Replace with your actual Game Scene name
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu"); // Replace with your Menu Scene name
    }
}
