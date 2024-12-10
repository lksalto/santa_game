using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    public float getSpeed() { return speed; }


    float xVelocity, yVelocity;
    private void Awake()
    {
        
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if(input.magnitude > 1)
        {
            input.Normalize();
        }
        
        transform.position = new Vector3(transform.position.x + input.x * speed * Time.deltaTime, transform.position.y + input.y * speed * Time.deltaTime, transform.position.z);

    }
}
