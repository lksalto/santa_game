using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerLife>().TakeHit(10);
        }
        else if(collision.gameObject.CompareTag("Genius"))
        {
            //Debug.Log("HIT BOSS");
            if(collision.GetComponent<BossStateMachine>().canAttack)
            {
                //collision.GetComponent<CircleCollider2D>().enabled = false;
                collision.GetComponent<BossStateMachine>().TakeDamage(13);
            }
            
        }
        else if(collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);

        }

    }
}
