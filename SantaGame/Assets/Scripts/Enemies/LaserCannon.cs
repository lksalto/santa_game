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

    [SerializeField] List<AudioClip> chargeSounds;
    [SerializeField] List<AudioClip> beamSounds;

    SoundManager soundManager;
    AudioSource mySource;
    

    // Flag to track when the laser is ready to fire
    private bool isLaserReady = false;

    private void Start()
    {
        mySource = GetComponent<AudioSource>();
        soundManager = FindObjectOfType<SoundManager>();
        if(soundManager != null )
        {
            mySource.volume = soundManager.vol;
        }
        

        rechargeCd = 0;
        isLaserReady = true; // Start ready to fire
    }

    void FixedUpdate()
    {
        rechargeCd -= Time.deltaTime;

        if (rechargeCd < 0 && !isLaserReady)
        {
            // Reset laser readiness
            isLaserReady = true;
            aim.gameObject.SetActive(true);
        }
    }

    IEnumerator ShootLaser()
    {
        // Instantiate aim effect
        int sorteio = Random.Range(0, chargeSounds.Count);
        mySource?.PlayOneShot(chargeSounds[sorteio]);
        GameObject aimPrefab = Instantiate(prefabs[0], spawnPoint.position + new Vector3(0, 0, -4), transform.rotation);
        aimPrefab.transform.parent = spawnPoint.transform;
        aimPrefab.GetComponentInChildren<FadeInFadeOut>().fadeDuration = cd;
        yield return new WaitForSeconds(chargeSounds[sorteio].length);

        // Destroy aim effect
        Destroy(aimPrefab);

        // Instantiate laser effect
        GameObject laserPrefab = Instantiate(prefabs[1], spawnPoint.position + new Vector3(0, 0, -4), transform.rotation);
        sorteio = Random.Range(0, beamSounds.Count);
        mySource?.PlayOneShot(beamSounds[sorteio]);
        laserPrefab.transform.parent = spawnPoint.transform;

        // Destroy laser after its duration
        Destroy(laserPrefab, beamSounds[sorteio].length);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isLaserReady && collision.gameObject.CompareTag("Detector"))
        {
            // Reset cooldown and shoot laser
            rechargeCd = rechargeTime;
            isLaserReady = false;
            StartCoroutine(ShootLaser());
            aim.gameObject.SetActive(false);
        }
    }
}
