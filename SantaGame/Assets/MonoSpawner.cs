using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSpawner : MonoBehaviour
{
    [SerializeField] float destroyTime = 10f;
    [SerializeField] float monoSpeed;
    [SerializeField] GameObject monoPrefab;
    [SerializeField] float cd = 1;
    [SerializeField] float monoAtkCd = 0;
    float cdTimer;
    // Start is called before the first frame update
    void Start()
    {
        cdTimer = cd;
    }

    // Update is called once per frame
    void Update()
    {
        cdTimer-=Time.deltaTime;
        if(cdTimer < 0)
        {
            cdTimer = cd;
            GameObject mono = Instantiate(monoPrefab, transform.position, Quaternion.identity);
            mono.GetComponent<EnemyMovement>().AutoDestroy(destroyTime);
            if(transform.position.x > 0)
            {
                mono.GetComponent<EnemyMovement>().xMoveSpeed = -monoSpeed;
            }
            else
            {
                mono.GetComponent<EnemyMovement>().xMoveSpeed = monoSpeed;
            }

            if(monoAtkCd > 0)
            {
                mono.GetComponent<EnemyMono>().atkCd = monoAtkCd;
                mono.GetComponent<EnemyMono>().bulletSpeed = 5 / monoAtkCd;
            }
            
        }
    }
}
