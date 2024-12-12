using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CircularBomb : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int bulletQtty;
    [SerializeField] int bulletSpeed;
    [SerializeField] int bulletDmg;
    [SerializeField] float bombCd = 1;
    [SerializeField] float radius = 0;
    int i = 0;
    [SerializeField] bool autoStart = false;
    

    private void Start()
    {

        if (autoStart)
        {
            StartCoroutine(BombCycle());
        }

    }


    private IEnumerator BombCycle()
    {
        while (true)
        {
            Explode();
            yield return new WaitForSeconds(bombCd);
        }
    }


    private void Explode()
    {
        float angleStep = 360f / bulletQtty; // The angle between each bullet
        float angle = 0f;

        for (int i = 0; i < bulletQtty; i++)
        {
            // Calculate the position for each bullet
            float bulletDirX = transform.position.x + Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            float bulletDirY = transform.position.y + Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

            Vector3 bulletPosition = new Vector3(bulletDirX, bulletDirY, transform.position.z);
            Quaternion bulletRotation = Quaternion.Euler(0, 0, angle);

            GameObject bullet = Instantiate(bulletPrefab, bulletPosition, bulletRotation);

            // Apply damage to the bullet if necessary (assuming a Bullet script exists)
            if (bullet.TryGetComponent<Bullet>(out var bulletScript))
            {
                bulletScript.SetSpeed(bulletSpeed);
                bulletScript.SetDmg(bulletDmg);
            }

            // Increment the angle for the next bullet
            angle += angleStep;
        }
    }
    public void StartExplosion()
    {
        StartCoroutine(BombCycle());
    }
}
