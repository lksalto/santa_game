using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeExplosion : MonoBehaviour
{
    
    [SerializeField] List<AudioClip> sounds;

    SoundManager soundManager;
    AudioSource mySource;
    void Start()
    {
        
        mySource = GetComponent<AudioSource>();
        soundManager = FindObjectOfType<SoundManager>();
        if (soundManager != null)
        {
            mySource.volume = soundManager.vol;
        }
        int sorteio = Random.Range(0, sounds.Count);
        mySource?.PlayOneShot(sounds[sorteio]);
        Destroy(gameObject, sounds[sorteio].length + 0.15f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
