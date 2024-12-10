using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInFadeOut : MonoBehaviour
{
    public bool reverse;
    public float fadeDuration = 0f; // Duration of the fade in seconds
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private float fadeTimer = 0f;
    private bool isFading = false;

    void Start()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color; // Store the original color
        }
        else
        {
            Debug.LogError("No SpriteRenderer found on this GameObject!");
        }
        StartFade();
    }

    void Update()
    {
        if (!reverse)
        {
            if (isFading && spriteRenderer != null)
            {
                fadeTimer += Time.deltaTime;
                float alpha = Mathf.Lerp(originalColor.a, 0, fadeTimer / fadeDuration);
                spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

                // Stop fading once the sprite is fully transparent
                if (fadeTimer >= fadeDuration)
                {
                    isFading = false;
                    fadeTimer = 0f;
                }
            }
        }
        else
        {
            if (isFading && spriteRenderer != null)
            {
                fadeTimer += Time.deltaTime;
                float alpha = Mathf.Lerp(0, originalColor.a, fadeTimer / fadeDuration);
                spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

                // Stop fading once the sprite is fully transparent
                if (fadeTimer >= fadeDuration)
                {
                    isFading = false;
                    fadeTimer = 0f;
                }
            }
        }

    }

    // Call this method to start the fade-out process
    public void StartFade()
    {
        if (spriteRenderer != null)
        {
            isFading = true;
            fadeTimer = 0f;
        }
    }
}
