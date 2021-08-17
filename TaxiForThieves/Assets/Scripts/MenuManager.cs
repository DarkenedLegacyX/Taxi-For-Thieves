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
    public Button closeHowTo, nextPageButton, prevPageButton;
    public Text highScoreLvl1, highScoreLvl2, highScoreLvl3;
    Animator mainButtonsAnimator, levelSelectAnimator;
    public bool unlockAllLevels;
    public int scoreToUnlockLvl2, scoreToUnlockLvl3;
    public GameObject lvl2ButtonCage, lvl3ButtonCage;
    public GameObject mainButtons, levelSelectButtons;
    public GameObject loadingPanel;
    public GameObject howToPanel;
    public GameObject[] howToPage;
    public GameObject logo;
    public Slider loadingSlider;
    public bool page0, page1, page2, page3;

    int currentPage = 1;
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
        logo.SetActive(true);
        mainButtonsAnimator = mainButtons.transform.GetComponent<Animator>();
        levelSelectAnimator = levelSelectButtons.transform.GetComponent<Animator>();
        playButton.onClick.AddListener(PlayButtonClick);
        howToButton.onClick.AddListener(HowToPlayOpen);
        nextPageButton.onClick.AddListener(nextPage);
        closeHowTo.onClick.AddListener(HowToPlayClose);
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
        logo.SetActive(false);
        page0 = true;
        howToPage[0].SetActive(true);
        howToPage[1].SetActive(false);
        howToPage[2].SetActive(false);
        howToPage[3].SetActive(false);
        mainButtons.SetActive(false);
        howToPanel.SetActive(true);
    }

    void HowToPlayClose()
    {
        logo.SetActive(true);
        page0 = false;
        page1 = false;
        page2 = false;
        page3 = false;
        howToPage[0].SetActive(false);
        howToPage[1].SetActive(false);
        howToPage[2].SetActive(false);
        howToPage[3].SetActive(false);
        mainButtons.SetActive(true);
        howToPanel.SetActive(false);
    }

    void nextPage()
    {

        if (page0 == true)
        {
            page0 = false;
            page1 = true;
            howToPage[0].SetActive(false);
            howToPage[1].SetActive(true);

        }
        else if (page1 == true)
        {
            page1 = false;
            page2 = true;
            howToPage[1].SetActive(false);
            howToPage[2].SetActive(true);
        }
        else if (page2 == true)
        {
            page2 = false;
            page3 = true;
            howToPage[2].SetActive(false);
            howToPage[3].SetActive(true);
        }
        else if (page3 == true)
        {
            page0 = true;
            page3 = false;
            howToPage[3].SetActive(false);
            howToPage[0].SetActive(true);
        }

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
