using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("wall");
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Detector"))
        {
            collision.gameObject.GetComponentInParent<PlayerLife>().HitWall();
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);

        }
    }
}
