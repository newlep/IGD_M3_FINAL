using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float laneDistance = 3f; // Distance between lanes
    private int targetLane = 1; // 0 = Left, 1 = Middle, 2 = Right
    public float forwardSpeed = 5f; // Initial speed
    public float speedIncreaseAmount = 1f; // Amount to increase speed
    public float speedIncreaseInterval = 10f; // Time in seconds to increase speed
    private float speedIncreaseTimer = 0f; // Timer to track speed increase intervals

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

        // Increase speed at regular intervals
        speedIncreaseTimer += Time.deltaTime;
        if (speedIncreaseTimer >= speedIncreaseInterval)
        {
            forwardSpeed += speedIncreaseAmount;
            speedIncreaseTimer = 0f; // Reset the timer
        }
    }

     void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over!");
            SceneManager.LoadScene("Lose"); // Replace with your Game Over Scene name
        }
    }
}