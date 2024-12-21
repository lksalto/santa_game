using System.Collections;
using UnityEngine;

public class BallReflect : MonoBehaviour
{
    public float reflectRadius = 5f;      // Radius of the circle
    public LayerMask ballLayer;          // Layer for rubber balls
    public float reflectForce = 10f;     // Force applied to redirect the balls
    public GameObject circleVisualPrefab; // Prefab for the circle visual
    public float cooldown = 1f;          // Cooldown duration in seconds
    public float circleDuration = 0.5f;  // How long the circle lasts

    private bool canUseAbility = true;   // Tracks if the ability is ready to use
    private Camera mainCamera;
    private GameObject activeCircle;     // Reference to the active circle visual
    private bool isCircleActive = false; // Tracks if the circle is currently active

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Check for right mouse button press and ability cooldown
        if (Input.GetMouseButtonDown(1) && canUseAbility)
        {
            canUseAbility = false;
            StartCoroutine(UseReflectAbility());
        }

        // Continuously reflect balls while the circle is active
        if (isCircleActive)
        {
            ReflectBalls();
        }
    }

    IEnumerator UseReflectAbility()
    {
        // Activate the ability
        isCircleActive = true;
        ShowCircle();

        // Wait for the circle's active duration
        yield return new WaitForSeconds(circleDuration);

        // Deactivate the circle
        isCircleActive = false;
        if (activeCircle != null)
        {
            Destroy(activeCircle);
        }

        // Set cooldown
        canUseAbility = false;

        // Wait for cooldown duration
        yield return new WaitForSeconds(cooldown);

        // Reactivate ability
        canUseAbility = true;
    }

    void ReflectBalls()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure it's on the same plane as the game

        // Find all rubber balls within the reflect radius
        Collider2D[] balls = Physics2D.OverlapCircleAll(transform.position, reflectRadius, ballLayer);

        foreach (Collider2D ball in balls)
        {
            // Calculate the direction from the ball to the mouse
            Vector2 direction = (mousePosition - ball.transform.position).normalized;

            // Apply force to redirect the ball
            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero; // Stop current movement
                rb.AddForce(direction * reflectForce, ForceMode2D.Impulse); // Redirect
                ball.GetComponent<RubberBall>().Activate();
            }
            
        }
    }

    void ShowCircle()
    {
        // Spawn the circle visual
        if (circleVisualPrefab != null)
        {
            activeCircle = Instantiate(circleVisualPrefab, transform.position, Quaternion.identity);
            Destroy(activeCircle, circleDuration);
            activeCircle.transform.localScale = new Vector3(reflectRadius * 2, reflectRadius * 2, 1); // Scale the circle

            // Start coroutine to follow the player while the circle is active
            StartCoroutine(FollowPlayer());
        }
    }

    IEnumerator FollowPlayer()
    {
        while (isCircleActive)
        {
            // Update the circle's position to match the player
            if (activeCircle != null)
            {
                activeCircle.transform.position = transform.position;
            }
            yield return null;
        }
    }

    // Draw the reflect radius in the editor for debugging
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, reflectRadius);
    }
}
