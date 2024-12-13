using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] public int life = 3;
    [SerializeField] public int coins = 0;
    [SerializeField] public bool canTakeDamage = true;
    [SerializeField] float invulnerabilityCd = 0.8f;

    [SerializeField] List<Sprite> sprites;

    [SerializeField] SpriteRenderer sr;
    [SerializeField] float flashDuration=0.1f;
    Sprite originalSprite;
    Sprite dmgSprite;

    [SerializeField] Resources resources;
    Menu menu;
    SlowMotion slowMotion;
    Camera cam;
    SoundManager soundManager;
    // Start is called before the first frame update
    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        slowMotion = GetComponent<SlowMotion>();
        menu = FindObjectOfType<Menu>();
        cam = Camera.main;
        originalSprite= sprites[0];
        dmgSprite = sprites[1];
        resources = FindObjectOfType<Resources>();
        resources.atualizaVida(life);
        
    }

    public void Flash()
    {
        StartCoroutine(FlashCoroutine());
    }

    public void TakeHit(int dmg)
    {
        if(canTakeDamage)
        {
            soundManager.PlaySound(0);
            life -= dmg;
            Flash();
            cam.GetComponent<CameraShake>().StartShake();
            StartCoroutine(Invulnerability(invulnerabilityCd));
            resources.atualizaVida(life);
            if(life <= 0)
            {
                
                StartCoroutine(Die());
                resources.atualizaVida(life);
            }
            else
            {
                slowMotion.SlowMo();
            }
        }

        
    }
    
    IEnumerator Invulnerability(float invulnerabilityCd)
    {
        soundManager.PlaySound(1);
        canTakeDamage = false;
        sr.sprite = dmgSprite;
        yield return new WaitForSeconds(invulnerabilityCd);
        canTakeDamage = true;
        sr.sprite = originalSprite;
    }

    IEnumerator FlashCoroutine()
    {
        
        yield return new WaitForSeconds(invulnerabilityCd);
        

    }

    IEnumerator Die()
    {
        soundManager.PlaySound(0);
        sr.enabled = false;
        GetComponent<PlayerDash>().showDash = false;
        GetComponent<LineRenderer>().enabled = false;
        GetComponent<PlayerMovement>().speed = 0;

        //GetComponent<PlayerDash>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(1);
        menu.ShowRestart();
        Destroy(gameObject);

    }

}
