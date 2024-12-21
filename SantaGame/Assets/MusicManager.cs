using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    // Singleton instance to persist the music manager across scenes
    public static MusicManager Instance;

    // AudioSource to play music
    private AudioSource audioSource;

    // AudioClips for normal and boss scenes
    public AudioClip normalSong;
    public AudioClip bossSong1;
    public AudioClip bossSong2;
    public Slider slider;
    SoundManager soundManager;
    // Current scene name
    private string currentSceneName;

    void Awake()
    {
        soundManager = GetComponentInChildren<SoundManager>();
        // Ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Get or add the AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Subscribe to scene load event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        if(slider)
        {
            audioSource.volume = slider.value;
            soundManager.GetComponent<AudioSource>().volume = slider.value;
            soundManager.vol = slider.value;
        }
        
    }

    void OnDestroy()
    {
        // Unsubscribe from the scene load event to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string newSceneName = scene.name;
        
        if (newSceneName != currentSceneName)
        {
            currentSceneName = newSceneName;
            UpdateMusicForScene(newSceneName);
        }
        GameObject s = GameObject.FindGameObjectWithTag("VolumeSlider");
        if (s != null)
        {
            slider = s.GetComponent<Slider>();
        }

    }

    void UpdateMusicForScene(string sceneName)
    {
        if (IsBossScene(sceneName))
        {
            // Play one of the boss songs randomly
            AudioClip bossSong = Random.Range(0, 2) == 0 ? bossSong1 : bossSong2;
            PlayMusic(bossSong);
        }
        else
        {
            // For normal scenes, play the normal song
            PlayMusic(normalSong);
        }
    }

    bool IsBossScene(string sceneName)
    {
        // Define your boss scene names here
        return sceneName == "Boss_Genius" || sceneName == "Boss2";
    }

    void PlayMusic(AudioClip clip)
    {
        if (audioSource.isPlaying && audioSource.clip == clip)
        {
            // Do nothing if the correct clip is already playing
            return;
        }

        // Stop the current music and play the new clip
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }
}