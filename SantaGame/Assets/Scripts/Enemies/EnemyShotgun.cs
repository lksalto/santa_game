using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemyShotgun : MonoBehaviour
{
    Transform target; // The target to face
    [SerializeField] SpriteRenderer sr;
    
    private void Awake()
    {
        target = FindObjectOfType<PlayerLife>().gameObject.transform;
        //sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (target == null) return;

        // Calculate the direction to the target
        Vector2 direction = target.position - transform.position;

        // Calculate the angle (in degrees) from the direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if(angle > 90 || angle < -90)
        {
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }
        // Rotate the object so that its up direction faces the target
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }
}
