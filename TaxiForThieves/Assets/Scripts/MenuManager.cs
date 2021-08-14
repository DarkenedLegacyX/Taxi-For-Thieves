using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button playButton, howToButton, exitButton;
    public Button lvl1Button, lvl2Button, lvl3Button, backlvlButton;
    public Animator mainButtonsAnimator, levelSelectAnimator;
    void Start()
    {
        playButton.onClick.AddListener(PlayButtonClick);
        //howToButton.onClick.AddListener(HowToPlayOpen);
        //closeHowTo.onClick.AddListener(HowToPlayClose);
        exitButton.onClick.AddListener(ExitGame);
        backlvlButton.onClick.AddListener(BackButtonClick);
    }

    void PlayButtonClick()
    {
        mainButtonsAnimator.Play("MainMenuAnimationSlideout");
        levelSelectAnimator.Play("LevelSelectButtonsAnimSlideIn");
        //SceneLoader.LoadLevel(1);
    }
    void BackButtonClick()
    {
        mainButtonsAnimator.Play("MainMenuAnimationSlidein");
        levelSelectAnimator.Play("LevelSelectButtonsAnimSlideOut");
        //SceneLoader.LoadLevel(1);
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
