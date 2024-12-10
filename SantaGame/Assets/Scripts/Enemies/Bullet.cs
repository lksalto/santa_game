using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    int dmg;
    float speed;
    float lifeSpan = 5f;

    private void Start()
    {
        Destroy(gameObject,lifeSpan);
    }

    private void Update()
    {
        MoveBullet();
    }

    public void MoveBullet()
    {
        transform.position += -transform.up  * speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, -4);
    }

    public void SetDmg(int dmg)
    {
        this.dmg = dmg;
    }
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerLife>().TakeHit(dmg);
            //ADD PARTICULA

            Destroy(gameObject);

        }
    }

}
