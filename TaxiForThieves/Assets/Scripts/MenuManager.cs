using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button PlayButton, ExitButton;
    void Start()
    {
        PlayButton.onClick.AddListener(StartGame);
        ExitButton.onClick.AddListener(ExitGame);
    }

    void StartGame()
    {
        SceneLoader.LoadLevel(1);
    }

    void ExitGame()
    {
        Application.Quit();

    }
}
