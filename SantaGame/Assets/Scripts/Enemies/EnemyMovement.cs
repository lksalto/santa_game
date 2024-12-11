using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] public float xMoveSpeed = 1f;
    [SerializeField] public float yMoveSpeed = 1f;


    private void Start()
    {

    }

    private void Update()
    {
        MoveEnemy();
    }

    public void MoveEnemy()
    {
        transform.position = new Vector3(transform.position.x + xMoveSpeed * Time.deltaTime, transform.position.y + yMoveSpeed * Time.deltaTime, transform.position.z);
    }

    public void AutoDestroy(float time)
    {

        Destroy(gameObject, time);


    }
}
