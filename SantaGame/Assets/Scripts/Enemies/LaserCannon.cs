using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserCannon : MonoBehaviour
{
    [SerializeField] float rechargeTime = 5f;
    float rechargeCd = 0;
    [SerializeField] float cd = 1f;
    [SerializeField] List<GameObject> prefabs;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float laserDuration = 0.5f;
    [SerializeField] GameObject aim;
    private void Start()
    {
        rechargeCd = 0;
    }
    void Update()
    {
        rechargeCd -= Time.deltaTime;
        if(rechargeCd < 0)
        {
            aim.gameObject.SetActive(true);
        }
    }


    IEnumerator ShootLaser()
    {
        GameObject aimPrefab = Instantiate(prefabs[0], spawnPoint.position + new Vector3(0,0,-4), transform.rotation);
        aimPrefab.transform.parent = spawnPoint.transform;
        aimPrefab.GetComponentInChildren<FadeInFadeOut>().fadeDuration = cd;
        yield return new WaitForSeconds(cd);
        Destroy(aimPrefab);
        GameObject laserPrefab = Instantiate(prefabs[1], spawnPoint.position + new Vector3(0, 0, -4), transform.rotation);
        laserPrefab.transform.parent = spawnPoint.transform;
        Destroy(laserPrefab, laserDuration);


    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(rechargeCd < 0 && collision.gameObject.CompareTag("Detector"))
        {
            rechargeCd = rechargeTime;
            StartCoroutine(ShootLaser());
            aim.gameObject.SetActive(false);
        }
        
    }
}
