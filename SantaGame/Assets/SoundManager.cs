using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] List<AudioClip> deathSounds;
    [SerializeField] List<AudioClip> hitSounds;
    [SerializeField] AudioClip keySound;
    [SerializeField] List<AudioClip> dashSounds;
    [SerializeField] List<AudioClip> geniusSound;

    AudioSource source;
    public float vol;

    //---------------------- Os sons do canhão ficam no próprio canhão ------------------------//


    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public void PlaySound(int i)
    {
        //death
        if(i == 0)
        {
            source.PlayOneShot(deathSounds[Random.Range(0, deathSounds.Count)]);
        }
        //hit
        else if(i == 1)
        {
            source.PlayOneShot(hitSounds[Random.Range(0, hitSounds.Count)]);
        }
        //chave
        else if (i == 2)
        {
            source.PlayOneShot(keySound);
        }
        //dash
        else if (i == 3)
        {
            source.PlayOneShot(dashSounds[Random.Range(0, dashSounds.Count)]);
        }
        //bossHit
        else if (i == 4)
        {
            source.PlayOneShot(geniusSound[Random.Range(0, geniusSound.Count)]);
        }




    }
}
