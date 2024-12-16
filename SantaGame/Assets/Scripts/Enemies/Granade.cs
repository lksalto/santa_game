using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Granade : MonoBehaviour
{
    public Vector3 targetGround;
    public Vector3 initialPosition;
    public float dmg;
    public float radius = 1.0f;
    public GameObject target;
    public float speed;
    public int dmgType = 0;
    [SerializeField] GameObject indicatorPrefab;
    SpriteRenderer indicatorSR;
    GameObject indicator;
    private float travelTime;

    public float maxHeight = 1.0f; // Maximum height of the cannonball's motion (optional)
    public float currentHeight = 0.0f; // Current height of the cannonball (optional)
    public float ascendSpeed = 1.0f; // Speed at which the cannonball ascends (optional)
    public float descendSpeed = 1.0f; // Speed at which the cannonball descends (optional)
    [SerializeField] Transform cannonballTransform;

    public float initialDistance;
    private bool hasReachedTarget = false;
    [SerializeField] GameObject explosionPrefab;

    private void Start()
    {
        targetGround = FindObjectOfType<PlayerLife>().gameObject.transform.position;
        target = FindObjectOfType<PlayerLife>().gameObject;
        initialPosition = target.transform.position + new Vector3(0, maxHeight, 0);

        
        transform.position = new Vector3(targetGround.x, targetGround.y + maxHeight, targetGround.z);


        // Create the indicator at the target position, 10 units above
        indicator = Instantiate(indicatorPrefab, new Vector3(targetGround.x, targetGround.y, 2), Quaternion.identity);
        indicator.transform.localScale = new Vector3(radius * 2f, radius * 2f, 1);

        // Get the SpriteRenderer component and set its initial alpha to 0 (invisible)
        indicatorSR = indicator.GetComponent<SpriteRenderer>();
        Color color = indicatorSR.color;
        color.a = 0;
        indicatorSR.color = color;
        indicator.transform.parent = null;

        // Calculate the travel time based on the initial distance
        float distance = Vector3.Distance(transform.position, targetGround);
        travelTime = distance / speed;

        // Start the coroutine to fade in the indicator
        StartCoroutine(FadeInIndicator(travelTime));

        initialDistance = Vector3.Distance(transform.position, targetGround);
        
    }

    void Update()
    {
        if (!hasReachedTarget)
        {
            // Update the cannonball's position (stationary) and the shadow size if needed
            UpdateCannonballMotion();
        }
    }

    private void UpdateCannonballMotion()
    {
        // Optional: You could animate the indicator or change its position here

        transform.position = Vector3.MoveTowards(transform.position, targetGround, Time.deltaTime * descendSpeed);
        if(transform.position.y == targetGround.y)
        {
            DealDamage(dmg);
        }
            

    }

    private void DealDamage(float dmg)
    {
        Vector2 position = transform.position;
        int layerMask = LayerMask.GetMask("Player");
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(position, radius, layerMask);

        foreach (Collider2D hitCollider in hitColliders)
        {
            PlayerLife playerLife = hitCollider.GetComponent<PlayerLife>();
            if (playerLife != null)
            {
                playerLife.TakeHit((int)dmg);
            }
        }

        GameObject exp = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(exp, 0.5f);

        Destroy(indicator,0.3f); // Destroy the indicator shortly after explosion
        
        Destroy(gameObject); // Destroy the cannonball object (it remains stationary)
    }

    private IEnumerator FadeInIndicator(float duration)
    {
        float targetAlpha = 0.6f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(0, targetAlpha, elapsed / duration);
            Color color = indicatorSR.color;
            color.a = alpha;
            indicatorSR.color = color;
            yield return null;
        }

        // Ensure the alpha is set to the target value at the end
        Color finalColor = indicatorSR.color;
        finalColor.a = targetAlpha;
        indicatorSR.color = finalColor;


    }
}

