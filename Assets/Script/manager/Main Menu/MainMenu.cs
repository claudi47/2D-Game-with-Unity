using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public InputField input;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        if (input.text == "") Player.name = "Unknown";
        else Player.name = input.text;
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}