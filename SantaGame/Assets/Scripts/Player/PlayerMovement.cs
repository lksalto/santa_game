using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    public float getSpeed() { return speed; }
    // Define the movement bounds (replace these values with your desired range)
    public float minY = -3.7f; // Minimum x boundary
    public float maxY = 3.7f;  // Maximum x boundary
    public float minX = -7.2f; // Minimum y boundary
    public float maxX = 7.2f;  // Maximum y boundary

    float xVelocity, yVelocity;
    private void Awake()
    {
        
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        // Get input from the player
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Normalize the input vector if its magnitude is greater than 1
        if (input.magnitude > 1)
        {
            input.Normalize();
        }

        // Calculate the new position
        Vector3 newPosition = new Vector3(
            transform.position.x + input.x * speed * Time.deltaTime,
            transform.position.y + input.y * speed * Time.deltaTime,
            transform.position.z
        );



        // Clamp the new position to the defined bounds
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // Apply the clamped position to the player
        transform.position = newPosition;
    }
}
