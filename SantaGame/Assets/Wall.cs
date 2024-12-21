using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{



    
     
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Detector"))
        {
            collision.gameObject.GetComponentInParent<PlayerLife>().HitWall();
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("COLISAO");
        if (collision.gameObject.CompareTag("RubberBall"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

}
