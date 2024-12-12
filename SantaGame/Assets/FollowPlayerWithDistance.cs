using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerWithDistance : MonoBehaviour
{
    public Transform player;         // Reference to the player's transform
    public float followDistance = 5f; // Minimum distance to maintain from the player
    public float moveSpeed = 3f;      // Speed of movement
    public bool canMove = true;
    private void Start()
    {
        player = FindObjectOfType<PlayerLife>().transform;
    }

    private void Update()
    {
        if(canMove)
        {
            if (player == null)
            {
                Debug.LogWarning("Player reference is missing!");
                return;
            }

            // Calculate the direction and distance between the follower and the player
            Vector3 directionToPlayer = player.position - transform.position;
            float distanceToPlayer = directionToPlayer.magnitude;

            // Check if the follower is closer than the desired follow distance
            if (distanceToPlayer > followDistance)
            {
                // Normalize the direction vector to maintain a constant speed
                Vector3 moveDirection = directionToPlayer.normalized;

                // Move the follower closer to the player

                transform.position += moveDirection * moveSpeed * Time.deltaTime;
            }
        }

    }
}
