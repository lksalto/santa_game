using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubberBallSpawner : MonoBehaviour
{
    [SerializeField] GameObject rubberBall;
    [SerializeField] float ballSpeed;
    bool isAtk1Over = false;
    public float atkTime = 1f;
    public float cd = 0.02f;
    [SerializeField] float angle;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AtkCd());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator AtkCd()
    {
        StartCoroutine(Atk1());
        yield return new WaitForSeconds(atkTime*40);
        //isAtk1Over=true;

    }

    IEnumerator Atk1()
    {
        // Position to spawn the ball
        Vector3 spawnPosition = transform.position;

        // Instantiate the ball
        GameObject ball = Instantiate(rubberBall, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();

        // Set the angle of the ball

        ball.transform.rotation = Quaternion.Euler(0, 0, angle);

        // Calculate the direction based on the rotation
        Vector2 direction = (Quaternion.Euler(0, 0, angle) * Vector2.right).normalized;

        // Apply force in the calculated direction
        rb.AddForce(direction * ballSpeed, ForceMode2D.Impulse);

        // Wait before repeating the attack
        yield return new WaitForSeconds(cd);

        if (!isAtk1Over)
        {
            StartCoroutine(Atk1());
        }
    }




}
