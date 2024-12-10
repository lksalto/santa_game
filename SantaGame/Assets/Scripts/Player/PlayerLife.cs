using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] int life = 3;
    [SerializeField] bool canTakeDamage = true;
    [SerializeField] float invulnerabilityCd = 0.8f;

    [SerializeField] List<Sprite> sprites;

    [SerializeField] SpriteRenderer sr;
    [SerializeField] float flashDuration=0.1f;
    Sprite originalSprite;
    Sprite dmgSprite;

    // Start is called before the first frame update
    void Start()
    {
        
        originalSprite= sprites[0];
        dmgSprite = sprites[1];
        
    }

    public void Flash()
    {
        StartCoroutine(FlashCoroutine());
    }

    public void TakeHit(int dmg)
    {
        if(canTakeDamage)
        {
            
            life -= dmg;
            StartCoroutine(Invulnerability(invulnerabilityCd));
        }

        
    }
    
    IEnumerator Invulnerability(float invulnerabilityCd)
    {
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

}
