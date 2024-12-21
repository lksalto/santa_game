using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneSecond : MonoBehaviour
{

    [SerializeField] List<Transform> positions;
    [SerializeField] GameObject boss;

    [SerializeField] GameObject player;
    [SerializeField] Camera cam;
    [SerializeField] Transform keyTransform;

    float speed = 1f;

    private void Start()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<PlayerDash>().enabled = false;
        boss.GetComponent<BossSecond>().enabled = false;
        boss.GetComponentInChildren<EnemyShotgun>().enabled = false;
        //boss.GetComponentInChildren<EnemyMono>().enabled = false;

    }

    private void Update()
    {
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 5, speed * Time.deltaTime);
        cam.transform.position = Vector3.MoveTowards(cam.transform.position, new Vector3(0, 0, -10), speed * 2.5f * Time.deltaTime);
        boss.transform.position = Vector3.MoveTowards(boss.transform.position, keyTransform.position, speed * Time.deltaTime);
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(3f);
        Destroy(keyTransform.gameObject);
        Destroy(gameObject);
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<PlayerDash>().enabled = true;
        boss.GetComponent<BossSecond>().enabled = true;
        boss.GetComponentInChildren<EnemyShotgun>().enabled = true;
        //boss.GetComponentInChildren<EnemyMono>().enabled = true;

    }
}
