using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab; // Prefab of the obstacle
    public float spawnInterval = 2f;  // Time between spawns
    public float minDistanceFromPlayer = 10f; // Minimum distance from player
    public float maxDistanceFromPlayer = 50f; // Maximum distance from player
    public Transform player;          // Reference to the player
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnObstacleNearPlayer();
            timer = 0f;
        }
    }

    void SpawnObstacleNearPlayer()
    {
        // Randomly choose a lane (-3 for left, 0 for middle, 3 for right)
        float randomLaneX = Random.Range(-1, 2) * 3f;

        // Choose a Z position relative to the player's current position
        float randomZ = Random.Range(player.position.z + minDistanceFromPlayer, player.position.z + maxDistanceFromPlayer);

        // Set spawn position
        Vector3 spawnPosition = new Vector3(randomLaneX, 1, randomZ);

        // Instantiate the obstacle
        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }
}
