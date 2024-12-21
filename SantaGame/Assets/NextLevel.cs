using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] List<Sprite> sprites;
    SpriteRenderer sr;
    [SerializeField] bool active = false;
    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        sr.sprite = sprites[0];
        
    }
    public void Activate()
    {

        active = true;
        sr.sprite = sprites[1];

        GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && active)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
    }
}
