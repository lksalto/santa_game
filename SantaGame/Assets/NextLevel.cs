using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] bool active = false;
    private void Start()
    {
        GetComponentInChildren<SpriteRenderer>().color = Color.red;
    }
    public void Activate()
    {
        GetComponentInChildren<SpriteRenderer>().color = Color.green;
        
        GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
    }
}
