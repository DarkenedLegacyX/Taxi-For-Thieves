using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI_CS : MonoBehaviour
{
    public static GameUI_CS instance = null;
    public Text crimText;
    public bool haveCrim = false;
    public Text errorText, goalText, droppedOffTxt, gameOverText;
    public Text minutesTimerTxt, secondsTimerTxt;
    public Text pointsTxt;
    public Text disguiseTimer;
    public GameObject[] crimIcons;
    public Slider crimSlider;
    public GameObject timer;
    public GameObject pausePanel;
    int playerPoints;
    

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
        crimSlider.value = 0;
    }

    void FixedUpdate()
    {

    }

    IEnumerator ShowErrorText()
    {
        errorText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(3);
        errorText.gameObject.SetActive(false);
    }

    public void UpdateCrimsCounter(int crimsDropedOff, int crimsTarget)
    {
        if (crimsDropedOff > crimsTarget)
            return;
        droppedOffTxt.text = crimsDropedOff.ToString();
        goalText.text = crimsTarget.ToString();
    }
    public IEnumerator UpdatePointsCounter(int pointsFinal, float duration)
    {
        float time = 0;
        int startValue = playerPoints;

        while (time < duration)
        {
            playerPoints = (int)Mathf.Lerp(startValue, pointsFinal, time/ duration);
            pointsTxt.text = playerPoints.ToString();
            time += Time.deltaTime;
            yield return null;
        }
        playerPoints = pointsFinal;
        pointsTxt.text = playerPoints.ToString();
    }

    public void ShowErrorMsg()
    {
        StartCoroutine(ShowErrorText());
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

        if(crim == 0)
        {
            crimSlider.transform.GetChild(2).gameObject.SetActive(false);
            foreach(GameObject crimIcon in crimIcons)
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
            crimIcons[iconIndex].GetComponent<Image>().color = new Color32(0, 255,16, 255);
    }

    public void SetIconToRed(int iconIndex)
    {
        if (iconIndex < crimIcons.Length)
            crimIcons[iconIndex].GetComponent<Image>().color = new Color32(255, 0, 0, 255);
    }
    public void ResetIconsColor()
    {
        foreach(GameObject icon in crimIcons)
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
        while(seconds > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            seconds--;
            if(seconds < 10)
                secondsTimerTxt.text = "0" + seconds.ToString();
            else
                secondsTimerTxt.text = seconds.ToString();
        }
        yield return new WaitForSecondsRealtime(1);
        secondsTimerTxt.text = "00";
    }
    public void FreezeTimer(bool frozen)
    {
        if(frozen)
            StopCoroutine("TimerCountDown");
        else
            StartCoroutine("TimerCountDown", Convert.ToInt32(secondsTimerTxt.text));
    }
    public void PauseUi(bool paused)
    {
        pausePanel.SetActive(paused);
    }
}
