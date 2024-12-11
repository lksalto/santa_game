using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resources : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI lifeText;
    [SerializeField] Slider lifeSlider;

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
        lifeText.text = vida.ToString();
        lifeSlider.value = vida;
    }

    public void atualizaCoin(int coin)
    {
        coinText.text = coin.ToString();
    }
}
