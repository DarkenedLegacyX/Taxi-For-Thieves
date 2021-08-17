using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI_CS : MonoBehaviour
{
    [Header("UI")]
    public int levelNo;
    public static GameUI_CS instance = null;
    public Text crimText;
    public bool haveCrim = false;
    public Text errorText, goalText, droppedOffTxt;
    public Text minutesTimerTxt, secondsTimerTxt;
    public Text pointsTxt, pointsRequiredTxt;
    public Text disguiseTimer;
    public GameObject[] crimIcons;
    public Slider crimSlider;
    public GameObject timer, lostCrimTxt;
    public GameObject pausePanel, quitEndPanel;
    public GameObject disguiseIMG;
    public GameObject speedIMG;
    public GameObject mudIMG;

    int playerPoints, pointsRequiredUI;
    bool gamePaused;

    [Header("STARTGAME")]
    public GameObject startPanel;
    public Text startLevelText, pstartPointsRequiredText;

    [Header("ENDGAME")]
    public GameObject endGamePanel;
    public Button endGameButtonPlayAgain, endGameButtonExit;
    public Text gameOverText, pointsEndGameText, pointsMinEndText, noPointsRequiredEndText;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        disguiseIMG.SetActive(false);
        speedIMG.SetActive(false);
        mudIMG.SetActive(false);

        startPanel.SetActive(true);
        disguiseTimer.enabled = false;
        crimSlider.value = 0;
        HideEndGamePanel();
        gamePaused = false;
        startLevelText.text = "Level " + levelNo.ToString();
        pstartPointsRequiredText.text = LevelManager_CS.instance.goalNuberOfPoints.ToString();
        SoundManager_CS.instance.StartMusic();
    }

    void FixedUpdate()
    {

    }

    IEnumerator ShowLostCrimText()
    {
        SoundManager_CS.instance.PlayJailSound();
        lostCrimTxt.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(3);
        lostCrimTxt.gameObject.SetActive(false);
    }

    //public void UpdateCrimsCounter(int crimsDropedOff, int crimsTarget)
    //{
    //    if (crimsDropedOff > crimsTarget)
    //        return;
    //    droppedOffTxt.text = crimsDropedOff.ToString();
    //    goalText.text = crimsTarget.ToString();
    //}
    public IEnumerator UpdatePointsCounter(int pointsFinal, float duration)
    {
        float time = 0;
        int startValue = playerPoints;

        while (time < duration)
        {
            playerPoints = (int)Mathf.Lerp(startValue, pointsFinal, time / duration);
            pointsTxt.text = playerPoints.ToString();
            time += Time.deltaTime;
            yield return null;
        }
        playerPoints = pointsFinal;
        pointsTxt.text = playerPoints.ToString();
    }

    public void UpdatePoints(int pointsRequired)
    {
        pointsRequiredUI = pointsRequired;
        pointsTxt.text = playerPoints.ToString();
        pointsRequiredTxt.text = "/ " + pointsRequired.ToString();
    }

    public void ShowErrorMsg()
    {
        StartCoroutine(ShowLostCrimText());
    }
    public void ShowGameOver()
    {
        gameOverText.gameObject.SetActive(true);
    }
    public void ShowGameWin()
    {
        gameOverText.text = "Win!";
        gameOverText.gameObject.SetActive(true);
    }

    public void SetCrimSliderAt(int crim)
    {
        if (crim > crimIcons.Length + 1)
            crim = crimIcons.Length + 1;

        if (crim == 0)
        {
            crimSlider.transform.GetChild(2).gameObject.SetActive(false);
            foreach (GameObject crimIcon in crimIcons)
            {
                crimIcon.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
        else
        {
            crimSlider.transform.GetChild(2).gameObject.SetActive(true);
            crimSlider.value = crim - 1;
            crimIcons[crim - 1].transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }

    }

    public void SetIconToGreen(int iconIndex)
    {
        if (iconIndex < crimIcons.Length)
            crimIcons[iconIndex].GetComponent<Image>().color = new Color32(0, 255, 16, 255);
    }

    public void SetIconToRed(int iconIndex)
    {
        if (iconIndex < crimIcons.Length)
            crimIcons[iconIndex].GetComponent<Image>().color = new Color32(255, 0, 0, 255);
    }
    public void ResetIconsColor()
    {
        foreach (GameObject icon in crimIcons)
        {
            icon.GetComponent<Image>().color = new Color32(0, 0, 0, 255);
        }
    }
    public void StartTimer(int seconds)
    {
        secondsTimerTxt.text = seconds.ToString();
        StartCoroutine("TimerCountDown", seconds);
    }
    public int StopTimer()
    {
        int remaining;
        StopCoroutine("TimerCountDown");
        remaining = Convert.ToInt32(secondsTimerTxt.text);
        secondsTimerTxt.text = "00";
        return remaining;
    }

    IEnumerator TimerCountDown(int seconds)
    {
        while (seconds > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            seconds--;
            if (seconds < 10)
                secondsTimerTxt.text = "0" + seconds.ToString();
            else
                secondsTimerTxt.text = seconds.ToString();
        }
        yield return new WaitForSecondsRealtime(1);
        secondsTimerTxt.text = "00";
    }
    public void FreezeTimer(bool frozen)
    {
        if (frozen)
            StopCoroutine("TimerCountDown");
        else
            StartCoroutine("TimerCountDown", Convert.ToInt32(secondsTimerTxt.text));
    }
    public void PauseUi(bool paused)
    {
        pausePanel.SetActive(paused);
    }

    void HideEndGamePanel()
    {
        endGamePanel.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 10000, 0);
    }

    public void StartEndGamePanel()
    {
        pointsEndGameText.text = playerPoints.ToString();
        noPointsRequiredEndText.text = pointsRequiredUI.ToString();
        SoundManager_CS.instance.PlayEndGameSound();

        if (playerPoints >= pointsRequiredUI)
        {
            gameOverText.text = "The End";
            pointsMinEndText.text = "The best score was: ";
            noPointsRequiredEndText.text = MaxScores.GetMaxScoreLvl(levelNo).ToString();
            MaxScores.SaveMaxScoreLvl(playerPoints, levelNo);
        }
        RectTransform rt = endGamePanel.GetComponent<RectTransform>();
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, rt.rect.height);
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, rt.rect.width);
        endGamePanel.GetComponent<Animator>().Play("EndGameAnim");
    }
    public void SetEndGameButtons()
    {
        endGameButtonPlayAgain.gameObject.SetActive(true);
        endGameButtonExit.gameObject.SetActive(true);

        //endGameButtonPlayAgain.onClick.AddListener();
        endGameButtonExit.onClick.AddListener(SceneLoader.instance.LoadMainMenu);
    }
    public void HideShowSureExit()
    {
        if (!gamePaused)
        {
            quitEndPanel.SetActive(true);
            gamePaused = true;
            Time.timeScale = 0;
        }
        else
        {
            quitEndPanel.SetActive(false);
            gamePaused = false;
            Time.timeScale = 1;
        }
    }
}
