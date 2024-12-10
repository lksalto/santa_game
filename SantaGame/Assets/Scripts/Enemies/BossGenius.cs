using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGenius : MonoBehaviour
{
    [SerializeField] int dmg;
    [SerializeField] float bulletSpeed;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float atkCd = 0.5f;
    [SerializeField] Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootProjectile());
    }

    IEnumerator ShootProjectile()
    {
        GameObject bullet = Instantiate(projectilePrefab, spawnPoint.position, transform.rotation);
        bullet.GetComponent<Bullet>().SetDmg(dmg);
        bullet.GetComponent<Bullet>().SetSpeed(bulletSpeed);
        bullet.transform.parent = null;
        yield return new WaitForSeconds(atkCd);
        StartCoroutine(ShootProjectile());

    }
}
