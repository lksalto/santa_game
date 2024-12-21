using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CutsceneGenius : MonoBehaviour
{

    [SerializeField] List<Transform> positions;
    [SerializeField] GameObject boss;
    [SerializeField] GameObject seguimores;
    [SerializeField] GameObject player;
    [SerializeField] Camera cam;

    [SerializeField] Transform keyTransform;

    float speed = 1f;

    private void Start()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<PlayerDash>().enabled = false;
        boss.GetComponent<BossStateMachine>().enabled = false;
        boss.GetComponentInChildren<GeniusAim>().enabled = false;
        boss.GetComponentInChildren<EnemyMono>().enabled = false;
        seguimores.GetComponentInChildren<LaserCannon>().enabled = false;
        seguimores.GetComponentInChildren<EnemyShotgun>().enabled = false;
    }

    private void Update()
    {
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 6, speed * Time.deltaTime);
        cam.transform.position = Vector3.MoveTowards(cam.transform.position, new Vector3(0,0,-10), speed * Time.deltaTime);
        boss.transform.position = Vector3.MoveTowards(boss.transform.position, keyTransform.position + new Vector3(0,0,-2), speed * Time.deltaTime);
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(3f);
        Destroy(keyTransform.gameObject);
        Destroy(gameObject);
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<PlayerDash>().enabled = true;
        boss.GetComponent<BossStateMachine>().enabled = true;
        boss.GetComponentInChildren<GeniusAim>().enabled = true;
        boss.GetComponentInChildren<EnemyMono>().enabled = true;
        seguimores.GetComponentInChildren<LaserCannon>().enabled = true;
        seguimores.GetComponentInChildren<EnemyShotgun>().enabled = true;
    }

}
