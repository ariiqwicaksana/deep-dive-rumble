using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class shop : MonoBehaviour
{
    public TextMeshProUGUI indicators;
   void OnTriggerStay2D(Collider2D other)
   {
        if(other.gameObject.CompareTag("Player"))
        {
            indicators.enabled = true;
        }
   }
   void OnTriggerExit2D(Collider2D other)
   {
        if(other.gameObject.CompareTag("Player"))
        {
            indicators.enabled = false;
        }
   }
}
