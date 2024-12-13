using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class BossKey : MonoBehaviour
{
    GameManager gm;
    void Start()
    {
        gm = FindAnyObjectByType<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gm = FindAnyObjectByType<GameManager>();
    }

    private void OnDestroy()
    {
        gm = FindAnyObjectByType<GameManager>();
        gm.EndGame();

    }
}
