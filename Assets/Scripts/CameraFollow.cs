using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Drag the Player GameObject here in the Inspector
    public Vector3 offset;    // Offset to maintain a fixed position behind the player
    public float smoothSpeed = 0.125f; // Adjust this for smoothness

    void LateUpdate()
    {
        // Target position based on player's position and offset
        Vector3 desiredPosition = player.position + offset;

        // Smoothly interpolate to the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera position
        transform.position = smoothedPosition;

        // Keep the camera looking at the player
        transform.LookAt(player);
    }
}
