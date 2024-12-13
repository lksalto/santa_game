using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    public bool showDash = true;
    private CapsuleCollider2D cc2d;
    private bool isDashing = false;
    private float lastDashTime;
    [SerializeField] private List<Sprite> sprites;
    private SpriteRenderer sr;

    private PlayerLife playerLife;

    private LineRenderer lineRenderer; // LineRenderer component
    SoundManager soundManager;
    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
        sr = GetComponentInChildren<SpriteRenderer>();
        sr.sprite = sprites[0];
        cc2d = GetComponent<CapsuleCollider2D>();
        playerLife = GetComponent<PlayerLife>();
        lineRenderer = GetComponent<LineRenderer>();

        // Initialize LineRenderer
        if (lineRenderer != null)
        {
            lineRenderer.enabled = false; // Start with the line hidden
        }
        lastDashTime = -dashCooldown;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= lastDashTime + dashCooldown)
        {
            soundManager.PlaySound(3);
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = transform.position.z;

            Vector3 direction = (mousePosition - transform.position).normalized;
            float distanceToMouse = Vector3.Distance(transform.position, mousePosition);

            Vector3 targetPosition = distanceToMouse > dashDistance
                ? transform.position + direction * dashDistance
                : mousePosition;

            StartCoroutine(Dash(targetPosition));
            lastDashTime = Time.time;
        }
        if(showDash)
        {
            if (Time.time >= lastDashTime + dashCooldown)
            {
                UpdateTrajectoryLine();
            }
            else
            {
                HideTrajectoryLine();
            }
        }
        else
        {
            HideTrajectoryLine();
        }



    }

    private IEnumerator Dash(Vector3 targetPosition)
    {
        sr.sprite = sprites[1];
        isDashing = true;
        playerLife.canTakeDamage = false;
        cc2d.enabled = false;

        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / dashDuration);
            yield return null;
        }

        playerLife.canTakeDamage = true;
        transform.position = targetPosition;
        isDashing = false;
        cc2d.enabled = true;
        sr.sprite = sprites[0];
    }

    private void UpdateTrajectoryLine()
    {
        if (lineRenderer == null) return;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z;

        Vector3 direction = (mousePosition - transform.position).normalized;
        float distanceToMouse = Vector3.Distance(transform.position, mousePosition);

        Vector3 targetPosition = distanceToMouse > dashDistance
            ? transform.position + direction * dashDistance
            : mousePosition;

        lineRenderer.enabled = true;
        lineRenderer.positionCount = 2; // A line has 2 points
        lineRenderer.SetPosition(0, transform.position + new Vector3(0,0,3)); // Start at player position
        lineRenderer.SetPosition(1, targetPosition); // End at target position
    }
    private void HideTrajectoryLine()
    {
        if (lineRenderer != null)
        {
            lineRenderer.enabled = false;
        }
    }

}
