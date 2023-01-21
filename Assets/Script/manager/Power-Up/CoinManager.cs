﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour {
    public Text hudText;
    private static CoinManager instance;
    public static int coins;

    public void Awake() {
        instance = this;
    }

    public void AddCoin(int coinsToAdd)
    {
        coins += coinsToAdd;
        hudText.text = "x   " + coins.ToString();
    }

    public static CoinManager getInstance()
    {
        return instance;
    }
}
