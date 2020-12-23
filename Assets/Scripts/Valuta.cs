using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valuta : MonoBehaviour
{
    public static int Coin;

    private void Awake()
    {
        Coin = PlayerPrefs.GetInt("Coin");
    }
}
