using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    AudioSource source;
    [SerializeField] Slider songSlider;
    private void Awake()
    {
        // Ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            source = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject); // Make this object persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
        
    }

    private void Start()
    {

    }

    public void PlayMusic(AudioClip clip)
    {
        
        source.Stop();
        source.loop = true;
        source.clip = clip;
        Debug.Log(clip.name);
        source.PlayOneShot(clip);
    }

    /// <summary>
    /// Play a sound effect.
    /// </summary>
    /// <param name="clip">The sound effect clip to play.</param>
    public void PlaySFX(AudioClip clip)
    {
        //sfxSource.PlayOneShot(clip);
    }

    /// <summary>
    /// Adjust the volume of music.
    /// </summary>
    /// <param name="volume">Volume level (0.0 to 1.0).</param>
    public void SetMusicVolume(float volume, int idx)
    {
        source.volume = Mathf.Clamp01(volume);
    }

    private void Update()
    {
        if(songSlider)
        {
            source.volume = songSlider.value;
        }
        
    }

}
