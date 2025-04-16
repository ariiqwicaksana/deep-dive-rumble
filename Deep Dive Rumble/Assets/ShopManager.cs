using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject shopCanvas;

    void shopDisabled()
    {
        shopCanvas.SetActive(false);
    }
}
