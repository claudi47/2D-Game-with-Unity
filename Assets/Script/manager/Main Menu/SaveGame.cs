using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveGame
{
    public static void SaveMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MUSIC", volume);
        PlayerPrefs.Save();
    }

}