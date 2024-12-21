using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Duration of the shake
    public float shakeDuration = 0.5f;

    // Intensity of the shake
    public float shakeMagnitude = 0.1f;

    // Whether the shake is currently active
    private bool isShaking = false;

    // Original position of the camera
    private Vector3 originalPosition;

    private void Start()
    {
        // Store the initial position of the camera
        originalPosition = new Vector3(0,0,-10);
    }



    public void StartShake()
    {
        if (!isShaking)
        {
            StartCoroutine(Shake());
        }
    }

    private IEnumerator Shake()
    {
        isShaking = true;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            // Generate random offsets for X and Y axes
            float offsetX = Random.Range(-1f, 1f) * shakeMagnitude;
            float offsetY = Random.Range(-1f, 1f) * shakeMagnitude;

            // Apply the offset to the camera's position
            transform.localPosition = new Vector3(
                originalPosition.x + offsetX,
                originalPosition.y + offsetY,
                originalPosition.z
            );

            elapsed += Time.deltaTime;
            yield return null; // Wait until the next frame
        }

        // Reset the camera's position
        transform.localPosition = originalPosition;
        isShaking = false;
    }
}
