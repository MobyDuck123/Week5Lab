using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string playerTag = "Player"; // Tag of the player object
    public float moveSpeed = 5f; // Movement speed
    public float rotationSpeed = 5f; // Rotation speed

    private Transform player;

    void Start()
    {
        // Find the player using the tag
        player = GameObject.FindGameObjectWithTag(playerTag)?.transform;

        if (player == null)
        {
            Debug.LogError("Player not found. Make sure the player has the specified tag.");
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Calculate the direction to the player
            Vector3 directionToPlayer = player.position - transform.position;

            // Rotate to look at the player
            Quaternion rotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

            // Move towards the player
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }
}
