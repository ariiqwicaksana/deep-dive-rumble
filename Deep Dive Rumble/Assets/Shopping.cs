using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shopping : MonoBehaviour
{
    public GameManager gameManager;
    public Move player;
    public TextMeshProUGUI indicator;
    public float indicatorTime;

    void Start()
    {
        indicator.enabled = false;
    }

    public void addOxygen()
    {
        if(gameManager.coinCount >= 10)
        {
            gameManager.coinCount -= 10;
            player.isAddOxygen = true;
            indicator.enabled = true;
            indicator.text = "Adding 10 Oxygen";
            Debug.Log("Membeli oksigen");
        } else
        {
            indicator.enabled = true;
            indicator.text = "Insufficient coins";
            Debug.Log("Koin ga cukup");
        }
        StartCoroutine(blinkText());
    }

    public void ghostMode()
    {
        if(gameManager.pieceCount >= 20)
        {
            gameManager.pieceCount -= 20;
            player.isInvincible = true;
            indicator.enabled = true;
            indicator.text = "Ghost mode activated";

            Debug.Log("Membeli ghost");
        } else
        {
            indicator.enabled = true;
            indicator.text = "Insufficient Scattered Pieces";
            Debug.Log("tidak cukup pecahan");
        }
        StartCoroutine(blinkText());
    }

    public void shooting()
    {
        if(gameManager.scrollCount >= 40)
        {
            gameManager.scrollCount -= 40;
            player.isBubbleAttack = true;
            indicator.enabled = true;
            indicator.text = "Bubble Power enabled";
            Debug.Log("Membeli shoot");
        } else
        {
            indicator.enabled = true;
            indicator.text = "Insufficient Scrolls";
            Debug.Log("tidak cukup gulungan");
        }
        StartCoroutine(blinkText());
    }

    IEnumerator blinkText()
    {
        yield return new WaitForSeconds(indicatorTime);
        indicator.enabled = false;
    }
}
