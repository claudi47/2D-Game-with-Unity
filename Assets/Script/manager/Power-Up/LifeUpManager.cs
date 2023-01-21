using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUpManager : MonoBehaviour {

    public Text Hud;
    public static LifeUpManager instance;
    public static int life = 1;

    private void Awake()
    {
        instance = this;
        life = 2;
    }

    public void AddLife(int lifeToAdd)
    {
        life += lifeToAdd;
        Hud.text = "x   " + life.ToString();
    }

    public static LifeUpManager getInstance()
    {
        return instance;
    }

}
