using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{

    public static void LoadLevel(int level)
    {
        //string levelName = "Level" + level.ToString();
        //SceneManager.LoadScene(levelName);
        SceneManager.LoadScene("CarScene");
    }
    public static void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
