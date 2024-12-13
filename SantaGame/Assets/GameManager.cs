using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    PlayerLife player;
    [SerializeField] GameObject menuEnd;
    [SerializeField] TextMeshProUGUI chronometerText;
    private void Start()
    {
        
        player = FindObjectOfType<PlayerLife>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            Restart();
        }
    }

    public static void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EndGame()
    {
        SpeedrunClock clock = FindObjectOfType<SpeedrunClock>();
        Destroy(player.gameObject);
        menuEnd.SetActive(true);
        if(clock)
        {
            FindObjectOfType<SpeedrunClock>().StopClock();
            chronometerText.text = clock.clockText.text;
        }
        else
        {
            chronometerText.text = "--:--:----";
        }

        

    }
}
