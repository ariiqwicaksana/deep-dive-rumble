using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class shoopManager : MonoBehaviour
{
    public TextMeshProUGUI indicators;
    public GameObject shopCanvas;
    public void dissabledCanvas()
    {
        indicators.enabled = false;
        shopCanvas.SetActive(false);
        Time.timeScale = 1;
    }
}
