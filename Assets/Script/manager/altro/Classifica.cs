using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Classifica : MonoBehaviour
{
    public Text classifica;

    void Start()
    {

    }

    void Update()
    {
        int len = Player.scores.Count + 1;
        string[] nomi = new string[len];
        int[] point = new int[len];

        string s = "Rank:\n";
        int i = 1;
        point[0] = Punteggio.score1;
        foreach (int a in Player.scores)
        {
            point[i++] = a;
        }
        i = 1;
        nomi[0] = Player.name;
        foreach (string a in Player.names)
        {
            nomi[i++] = a;
        }

        for (i = 0; i < len && i < 4; i++)
        {
            int max = 0;
            for (int j = 1; j < len; j++)
            {
                if (point[j] > point[max]) max = j;
            }
            if (point[max] == Punteggio.score1) s = s + "*";
            s = s + nomi[max] + " " + point[max] + "\n";
            point[max] = 0;
        }

        classifica.text = s;
    }
}