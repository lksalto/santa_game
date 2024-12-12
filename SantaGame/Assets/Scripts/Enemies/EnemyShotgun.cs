using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotgun : MonoBehaviour
{
    Transform target; // The target to face
    [SerializeField] SpriteRenderer sr;
    [SerializeField] float aimSpeed = 90f; // Speed of aiming in degrees per second

    private void Awake()
    {
        target = FindObjectOfType<PlayerLife>().gameObject.transform;
    }

    void Update()
    {
        if (target == null) return;

        // Calculate the direction to the target
        Vector2 direction = target.position - transform.position;

        // Calculate the angle (in degrees) from the direction
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Flip the sprite based on the angle
        if (targetAngle > 90 || targetAngle < -90)
        {
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }

        // Get the current rotation angle
        float currentAngle = transform.eulerAngles.z;

        // Smoothly interpolate between the current angle and the target angle at a constant speed
        float smoothedAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle - 90, aimSpeed * Time.deltaTime);

        // Apply the smoothed rotation to the object
        transform.rotation = Quaternion.Euler(0, 0, smoothedAngle);
    }
}
