using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpeedrunClock : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI clockText; // Reference to a UI Text element to display the time
    private float elapsedTime = 0f; // Tracks the elapsed time
    private bool isRunning = false; // Indicates whether the clock is active
    private bool passedTutorial = false; // Checks if the player has passed the "Tutorial" scene
    public bool isSpeedRunning = true;
    // Singleton instance
    public static SpeedrunClock Instance;
    public int pLife = 3;
    void Awake()
    {
        // Singleton setup to persist across scenes
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

        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // Unsubscribe from the sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        ResetClock(); // Optionally reset the clock at the start
    }

    void Update()
    {
        if (isRunning)
        {
   // Increment elapsed time using unscaledDeltaTime
            elapsedTime += Time.unscaledDeltaTime;
            UpdateClockDisplay();


        }

    }

    public void StartClock()
    {

        if (passedTutorial) // Only start if tutorial is passed
        {
            isRunning = true;
        }


    }

    public void StopClock()
    {
        isRunning = false;
    }

    public void ResetClock()
    {
        elapsedTime = 0f;
        UpdateClockDisplay();

    }

    private void UpdateClockDisplay()
    {
        
        // Format time as MM:SS.ms
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        int milliseconds = Mathf.FloorToInt((elapsedTime % 1) * 1000);

        clockText.text = string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        // Check if the current scene is the "Tutorial" scene
        if (scene.name == "Tutorial")
        {
            passedTutorial = true;
            ResetClock(); // Optionally reset the clock when starting the run
            StartClock(); // Automatically start the clock after passing the tutorial
        }
        if(scene.name == "MenuInicial")
        {
            StopClock();
            ResetClock();
            StopClock();
        }
        SetPlayerLife(pLife);
    }
    public void SetClock(bool b)
    {
        isSpeedRunning = b;
    }

    public void SetPlayerLife(int x = 3)
    {
        PlayerLife player = FindObjectOfType<PlayerLife>();
        if(player != null)
        {
            player.life = x;
        }

    }

    public void SetPLife(int v)
    {
        pLife = v;
    }
}
