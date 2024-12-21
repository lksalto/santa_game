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
    [SerializeField] TextMeshProUGUI diffText;
    public int pLife;
    private void Start()
    {
        pLife = FindObjectOfType<SpeedrunClock>().pLife;
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
        int d;
        SpeedrunClock clock = FindObjectOfType<SpeedrunClock>();
        
        menuEnd.SetActive(true);
        if(clock)
        {
            FindObjectOfType<SpeedrunClock>().StopClock();
            chronometerText.text = clock.clockText.text;

            if(pLife==1)
            {
                diffText.text = diffText.text + "HARD";
            }
            else if(pLife == 2)
            {
                diffText.text = diffText.text + "MEDIUM";
            }
            else if(pLife == 3)
            {
                diffText.text = diffText.text + "EASY";
            }
            else
            {
                diffText.text = diffText.text + "????";
            }
        }
        else
        {
            chronometerText.text = "---";
            diffText.text = "Lvl Selection";
        }

        player.gameObject.SetActive(false);

    }
}
