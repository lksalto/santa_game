using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMono : MonoBehaviour
{

    [SerializeField] int dmg;
    [SerializeField] float bulletSpeed;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float atkCd = 0.5f;
    [SerializeField] Transform spawnPoint;
    public bool canShoot = true;
    // Start is called before the first frame update
    void Start()
    {   if(canShoot)
        {
            StartCoroutine(ShootProjectile());
        }
        
    }

    public void StartShooting()
    {
        canShoot = true;
        StartCoroutine(ShootProjectile());
    }

    IEnumerator ShootProjectile()
    {
        if(canShoot)
        {

            yield return new WaitForSeconds(atkCd);
            GameObject bullet = Instantiate(projectilePrefab, spawnPoint.position + new Vector3(0, 0, -4), transform.rotation);
            bullet.GetComponent<Bullet>().SetDmg(dmg);
            bullet.GetComponent<Bullet>().SetSpeed(bulletSpeed);
            bullet.transform.parent = null;
            StartCoroutine(ShootProjectile());
        }


    }
}
