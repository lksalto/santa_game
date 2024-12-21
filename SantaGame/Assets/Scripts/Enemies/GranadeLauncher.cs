using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeLauncher : MonoBehaviour
{


    [SerializeField] GameObject granadePrefab;
    [SerializeField] float minCd = 1;
    [SerializeField] float maxCd = 3f;

    float cdTimer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {

        yield return new WaitForSeconds(Random.Range(minCd, maxCd));

        GameObject granade = Instantiate(granadePrefab, transform.position, Quaternion.identity);
        granade.transform.parent = null;
        StartCoroutine(Attack());
    }


}
