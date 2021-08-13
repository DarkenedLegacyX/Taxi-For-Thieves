using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button PlayButton, howToButton, closeHowTo, ExitButton;
    void Start()
    {
        PlayButton.onClick.AddListener(StartGame);
        howToButton.onClick.AddListener(HowToPlayOpen);
        closeHowTo.onClick.AddListener(HowToPlayClose);
        ExitButton.onClick.AddListener(ExitGame);
    }

    void StartGame()
    {
        SceneLoader.LoadLevel(1);
    }

    void HowToPlayOpen()
    {

    }

    void HowToPlayClose()
    {

    }

    void ExitGame()
    {
        Application.Quit();

    }
}
