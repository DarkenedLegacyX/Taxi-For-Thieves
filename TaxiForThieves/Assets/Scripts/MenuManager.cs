using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button playButton, howToButton, exitButton, resetScores;
    public Button lvl1Button, lvl2Button, lvl3Button, backlvlButton;
    public Text highScoreLvl1, highScoreLvl2, highScoreLvl3;
    public Animator mainButtonsAnimator, levelSelectAnimator;
    public bool unlockAllLevels;
    public int scoreToUnlockLvl2, scoreToUnlockLvl3;
    public GameObject lvl2ButtonCage, lvl3ButtonCage;
    void Start()
    {
        playButton.onClick.AddListener(PlayButtonClick);
        //howToButton.onClick.AddListener(HowToPlayOpen);
        //closeHowTo.onClick.AddListener(HowToPlayClose);
        exitButton.onClick.AddListener(ExitGame);
        backlvlButton.onClick.AddListener(BackButtonClick);
        UnlockLevels();
        //MaxScores.SaveMaxScoreLvl(2000, 1);
        //MaxScores.SaveMaxScoreLvl(3000,2);
        lvl1Button.onClick.AddListener(delegate { SceneLoader.LoadLevel(1); });
        lvl2Button.onClick.AddListener(delegate { SceneLoader.LoadLevel(2); });
        lvl3Button.onClick.AddListener(delegate { SceneLoader.LoadLevel(3); });
        resetScores.onClick.AddListener(ResetButtonClick);
    }

    void PlayButtonClick()
    {
        mainButtonsAnimator.Play("MainMenuAnimationSlideout");
        levelSelectAnimator.Play("LevelSelectButtonsAnimSlideIn");
        UnlockLevels();
        RefreshHighScores();
    }
    void BackButtonClick()
    {
        mainButtonsAnimator.Play("MainMenuAnimationSlidein");
        levelSelectAnimator.Play("LevelSelectButtonsAnimSlideOut");
    }
    void ResetButtonClick()
    {
        MaxScores.ResetScores();
        UnlockLevels();
        RefreshHighScores();
    }

    void HowToPlayOpen()
    {

    }

    void HowToPlayClose()
    {

    }

    void UnlockLevels()
    {
        if(unlockAllLevels)
        {
            lvl2Button.interactable = true;
            lvl2ButtonCage.SetActive(false);
            lvl3Button.interactable = true;
            lvl3ButtonCage.SetActive(false);
        }
        else
        {
            if(MaxScores.GetMaxScoreLvl(1) >= scoreToUnlockLvl2)
            {
                lvl2Button.interactable = true;
                lvl2ButtonCage.SetActive(false);
            }
            else
            {
                lvl2Button.interactable = false;
                lvl2ButtonCage.SetActive(true);
                lvl3Button.interactable = false;
                lvl3ButtonCage.SetActive(true);
            }

            if (MaxScores.GetMaxScoreLvl(2) >= scoreToUnlockLvl3)
            {
                lvl3Button.interactable = true;
                lvl3ButtonCage.SetActive(false);
            }
        }
    }

    void RefreshHighScores()
    {
        if (MaxScores.GetMaxScoreLvl(1) > 0)
            highScoreLvl1.text = "High: " + MaxScores.GetMaxScoreLvl(1).ToString();
        else
            highScoreLvl1.text = "";

        if (MaxScores.GetMaxScoreLvl(2) > 0)
            highScoreLvl2.text = "High: " + MaxScores.GetMaxScoreLvl(2).ToString();
        else
            highScoreLvl2.text = "";

        if (MaxScores.GetMaxScoreLvl(3) > 0)
            highScoreLvl3.text = "High: " + MaxScores.GetMaxScoreLvl(3).ToString();
        else
            highScoreLvl3.text = "";
    }
    void ExitGame()
    {
        Application.Quit();
    }
}
