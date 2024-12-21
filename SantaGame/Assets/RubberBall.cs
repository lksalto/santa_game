using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubberBall : MonoBehaviour
{
    public bool active = false;
    private void OnCollisionStay2D(Collision2D collision)
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !active)
        {
            collision.gameObject.GetComponent<PlayerLife>().TakeHit(1);
            //ADD PARTICULA

            Destroy(gameObject);

        }
    }

    public void Activate()
    {
        active = true;
        GetComponentInChildren<SpriteRenderer>().color = Color.red;
    }
}
