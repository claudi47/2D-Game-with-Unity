using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Text hudText;
    int startTimer;
    public static int timeLeft;
    public int total;

    void Start()
    {
        total = 60 * 2;
        startTimer = (int)Time.time;
    }

    void Update()
    {
        if (!Player.isPlay)
        {
            startTimer = (int)Time.time + timeLeft - total;
        }
        int a = (int)Time.time - startTimer;

        int remaining = total - a;
        timeLeft = remaining;

        if (remaining < 0) remaining = 0;

        int min = remaining / 60;
        int sec = remaining % 60;

        string s;
        if (sec >= 10) s = "0" + min.ToString() + ":" + sec.ToString();
        else s = "0" + min.ToString() + ":0" + sec.ToString();
        hudText.text = s.ToString();

        if (remaining<=0) SceneManager.LoadScene(0);
    }

}