using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resources : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI lifeText;
    [SerializeField] Slider lifeSlider;

    [SerializeField] List<GameObject> hearts;

    [SerializeField] Image coinImg;
    [SerializeField] TMPro.TextMeshProUGUI coinText;

    GameObject player;
    PlayerLife playerLife;

    private void Start()
    {
        player = FindObjectOfType<PlayerLife>().gameObject;
        playerLife = player.GetComponent<PlayerLife>();

        lifeText.text = playerLife.life.ToString();
        lifeSlider.maxValue = playerLife.life;
        lifeSlider.value = playerLife.life;

        coinText.text = playerLife.coins.ToString();

    }

    public void atualizaVida(int vida)
    {
        
        if(vida == 3)
        {
            
            hearts[0].SetActive(true);
            hearts[1].SetActive(true);
            hearts[2].SetActive(true);
        }
        if(vida == 2)
        {
            
            hearts[0].SetActive(true);
            hearts[1].SetActive(true);
            hearts[2].SetActive(false);
        }
        else if(vida == 1)
        {
            
            hearts[0].SetActive(true);
            hearts[1].SetActive(false);
            hearts[2].SetActive(false);
        }
        else if(vida == 0)
        {
            
            hearts[0].SetActive(false);
        }
    }

    public void atualizaCoin(int coin)
    {
        coinText.text = coin.ToString();
    }
}
