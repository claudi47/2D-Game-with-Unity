using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Punteggio : MonoBehaviour
{
    public static int score1;
    public Text TOTPUNTI;
    int tmp;

    void Start()
    {
        score1 = 0;
        tmp = (int)CoinManager.coins;
    }

    void Update()
    {

        if (CoinManager.coins > tmp)
        {
            score1 += Timer.timeLeft;
            tmp++;
        }

        TOTPUNTI.text = "Points: " + score1.ToString();

        if (LifeUpManager.life <= 0)
        {
            score1 = 0;
        }
    }
}