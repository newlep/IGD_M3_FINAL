using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Required for TextMeshPro

public class PlayerMovement : MonoBehaviour
{
    public float laneDistance = 3f; // Distance between lanes
    private int targetLane = 1; // 0 = Left, 1 = Middle, 2 = Right
    public float forwardSpeed = 5f; // Normal base speed
    public float speedIncreaseAmount = 1f; // Amount to increase speed every 5 seconds
    public float speedIncreaseInterval = 5f; // Time in seconds for base speed increase
    public float speedBoostMultiplier = 2f; // Multiplier for speed boost
    public float speedBoostDuration = 1f; // Duration of the boost in seconds
    private float speedIncreaseTimer = 0f; // Timer to track speed increase intervals
    private bool isBoosting = false; // Tracks if the player is currently boosted

    public Camera mainCamera; // Reference to the main camera
    public float normalFOV = 60f; // Normal field of view
    public float boostFOV = 80f; // Field of view during speed boost
    public float fovTransitionSpeed = 5f; // Speed of FOV transition

    public TextMeshProUGUI scoreText; // Reference to the ScoreText
    private float startZ; // Starting Z position of the player

    public AudioClip boostSound; // Sound effect for boost
    private AudioSource audioSource; // AudioSource for playing sounds

    void Start()
    {
        // Initialize camera and starting position
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        mainCamera.fieldOfView = normalFOV;

        // Record the starting Z position
        startZ = transform.position.z;

        // Ensure the scoreText is assigned
        if (scoreText == null)
        {
            Debug.LogError("ScoreText is not assigned in the Inspector!");
        }

        // Setup AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Move the player forward automatically
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        // Handle lane switching
        if (Input.GetKeyDown(KeyCode.LeftArrow) && targetLane > 0)
            targetLane--;
        if (Input.GetKeyDown(KeyCode.RightArrow) && targetLane < 2)
            targetLane++;

        // Smoothly move to the target lane
        Vector3 targetPosition = new Vector3((targetLane - 1) * laneDistance, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10);

        // Handle base speed increase
        speedIncreaseTimer += Time.deltaTime;
        if (speedIncreaseTimer >= speedIncreaseInterval)
        {
            forwardSpeed += speedIncreaseAmount; // Increase the base speed
            speedIncreaseTimer = 0f; // Reset the timer
        }

        // Check for speed boost input
        if (Input.GetKeyDown(KeyCode.Space) && !isBoosting)
        {
            StartCoroutine(SpeedBoost());
        }

        // Smoothly transition the camera FOV
        float targetFOV = isBoosting ? boostFOV : normalFOV;
        mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFOV, Time.deltaTime * fovTransitionSpeed);

        // Update the score
        UpdateScore();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Save the score before loading the Game Over Scene
            float finalScore = transform.position.z - startZ;
            PlayerPrefs.SetFloat("FinalScore", finalScore);

            // Transition to the Game Over Scene
            SceneManager.LoadScene("Lose");
            forwardSpeed = 0; // Stop the player
        }
    }

   IEnumerator SpeedBoost()
{
    isBoosting = true; // Set boosting flag
    forwardSpeed *= speedBoostMultiplier; // Temporarily increase speed

    // Play the boost sound effect
    if (boostSound != null && audioSource != null)
    {
        audioSource.clip = boostSound;
        audioSource.loop = false; // Ensure it doesn't loop
        audioSource.Play();
    }

    yield return new WaitForSeconds(speedBoostDuration); // Wait for the boost duration

    // Stop the sound when the boost is over
    if (audioSource.isPlaying && audioSource.clip == boostSound)
    {
        audioSource.Stop();
    }

    forwardSpeed /= speedBoostMultiplier; // Reset speed to normal
    isBoosting = false; // Clear boosting flag
}

    void UpdateScore()
    {
        // Calculate distance traveled
        float distance = transform.position.z - startZ;

        // Update the score text
        scoreText.text = "Score: " + Mathf.FloorToInt(distance).ToString();
    }
}
