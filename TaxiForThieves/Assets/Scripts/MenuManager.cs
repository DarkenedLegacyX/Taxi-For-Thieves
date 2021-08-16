using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
    
{
    public static MenuManager instance = null;

    public Button playButton, howToButton, exitButton, resetScores;
    public Button lvl1Button, lvl2Button, lvl3Button, backlvlButton;
    public Text highScoreLvl1, highScoreLvl2, highScoreLvl3;
    Animator mainButtonsAnimator, levelSelectAnimator;
    public bool unlockAllLevels;
    public int scoreToUnlockLvl2, scoreToUnlockLvl3;
    public GameObject lvl2ButtonCage, lvl3ButtonCage;
    public GameObject mainButtons, levelSelectButtons;
    public GameObject loadingPanel;
    public Slider loadingSlider;

    private void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //}
        //else if (instance != this)
        //{
        //    Destroy(gameObject);
        //}

        //DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        mainButtons.gameObject.SetActive(true);
        levelSelectButtons.gameObject.SetActive(true);
        mainButtonsAnimator = mainButtons.transform.GetComponent<Animator>();
        levelSelectAnimator = levelSelectButtons.transform.GetComponent<Animator>();
        playButton.onClick.AddListener(PlayButtonClick);
        //howToButton.onClick.AddListener(HowToPlayOpen);
        //closeHowTo.onClick.AddListener(HowToPlayClose);
        exitButton.onClick.AddListener(ExitGame);
        backlvlButton.onClick.AddListener(BackButtonClick);
        loadingPanel.SetActive(false);
        UnlockLevels();
        //MaxScores.SaveMaxScoreLvl(2000, 1);
        //MaxScores.SaveMaxScoreLvl(3000,2);
        lvl1Button.onClick.AddListener(delegate { SceneLoader.instance.LoadLevelAsync(1, loadingPanel, loadingSlider); });
        lvl2Button.onClick.AddListener(delegate { SceneLoader.instance.LoadLevelAsync(2, loadingPanel, loadingSlider); });
        lvl3Button.onClick.AddListener(delegate { SceneLoader.instance.LoadLevelAsync(3, loadingPanel, loadingSlider); });
        resetScores.onClick.AddListener(ResetButtonClick);
    }

    void PlayButtonClick()
    {
        mainButtonsAnimator.SetTrigger("slideOut");
        levelSelectAnimator.SetTrigger("slideIn");
        UnlockLevels();
        RefreshHighScores();
    }
    void BackButtonClick()
    {
        levelSelectAnimator.SetTrigger("slideOut");
        mainButtonsAnimator.SetTrigger("slideIn");
        
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
