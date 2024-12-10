using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMono : MonoBehaviour
{

    [SerializeField] int dmg;
    [SerializeField] int bulletSpeed;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float atkCd = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootProjectile());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShootProjectile()
    {
        GameObject bullet = Instantiate(projectilePrefab, transform.position, transform.rotation);
        bullet.GetComponent<Bullet>().SetDmg(dmg);
        bullet.GetComponent<Bullet>().SetSpeed(bulletSpeed);
        bullet.transform.parent = null;
        yield return new WaitForSeconds(atkCd);
        StartCoroutine(ShootProjectile());

    }
}
