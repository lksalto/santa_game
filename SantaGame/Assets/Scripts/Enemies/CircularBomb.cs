using System.Collections;
using UnityEngine;

public class CircularBomb : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int bulletQtty;
    [SerializeField] int bulletSpeed;
    [SerializeField] int bulletDmg;
    [SerializeField] float bombCd;
    [SerializeField] float radius = 0;
    [SerializeField] float fadeDuration = 1f; // Time for fading

    private SpriteRenderer spriteRenderer;
    private float fadeTimer = 0f;
    private bool isFading = false;
    private Color originalColor;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            originalColor = Color.white; // Start with white color
            spriteRenderer.color = originalColor;
        }
        else
        {
            Debug.LogError("SpriteRenderer not found!");
        }

        StartCoroutine(BombCycle());
    }

    private void Update()
    {
        if (isFading && spriteRenderer != null)
        {
            fadeTimer += Time.deltaTime;

            // Calculate the interpolation factor
            float t = fadeTimer / fadeDuration;

            // Gradually change color from white to red (fade green and blue to 0)
            float greenAndBlue = Mathf.Lerp(1f, 0f, 0.5f); // Fade green and blue to 0
            spriteRenderer.color = new Color(1f, greenAndBlue, greenAndBlue, originalColor.a);

            // Stop fading once the sprite is fully red
            if (fadeTimer >= fadeDuration)
            {
                isFading = false;
                fadeTimer = 0f;

                // Trigger explosion
                Explode();
            }
        }
    }

    private IEnumerator BombCycle()
    {
        while (true)
        {
            StartFade();
            yield return new WaitForSeconds(fadeDuration + bombCd); // Wait for fade and cooldown duration
        }
    }

    private void StartFade()
    {
        if (spriteRenderer != null)
        {
            isFading = true;
            fadeTimer = 0f;
        }
    }

    private void Explode()
    {
        float angleStep = 360f / bulletQtty; // The angle between each bullet
        float angle = 0f;

        for (int i = 0; i < bulletQtty; i++)
        {
            // Calculate the position for each bullet
            float bulletDirX = transform.position.x + Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            float bulletDirY = transform.position.y + Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

            Vector3 bulletPosition = new Vector3(bulletDirX, bulletDirY, transform.position.z);
            Quaternion bulletRotation = Quaternion.Euler(0, 0, angle);

            GameObject bullet = Instantiate(bulletPrefab, bulletPosition, bulletRotation);

            // Apply damage to the bullet if necessary (assuming a Bullet script exists)
            if (bullet.TryGetComponent<Bullet>(out var bulletScript))
            {
                bulletScript.SetSpeed(bulletSpeed);
                bulletScript.SetDmg(bulletDmg);
            }

            // Increment the angle for the next bullet
            angle += angleStep;
        }
    }
}
