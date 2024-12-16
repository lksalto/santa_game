using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] GameObject seta;
    SoundManager soundManager;
    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Detector"))
        {

            FindObjectOfType<NextLevel>().Activate();
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        soundManager?.PlaySound(2);
        if(seta)
        {
            seta.SetActive(true);
        }
    }

}
