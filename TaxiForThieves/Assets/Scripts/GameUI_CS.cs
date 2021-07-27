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
    public Sprite iconCrimBw, iconCrimYellow;
    public GameObject[] crimIcons;
    public Slider crimSlider;
    public GameObject timer;
    

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
        UpdateUI();
        crimSlider.value = 0;
    }

    public void UpdateUI()
    {

        if(haveCrim == false)
        {
            crimText.text = "You Have not picked up a Criminal";
        }
        else
        {
            crimText.text = "You picked up a Criminal!";
        }

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
    public void StartTimer(int seconds)
    {
        secondsTimerTxt.text = seconds.ToString();
        timer.SetActive(true);
        StartCoroutine("TimerCountDown", seconds);
    }
    public void StopTimer()
    {
        timer.SetActive(false);
        StopCoroutine("TimerCountDown");
    }

    IEnumerator TimerCountDown(int seconds)
    {
        while(seconds > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            seconds--;
            secondsTimerTxt.text = seconds.ToString();
        }
        yield return new WaitForSecondsRealtime(1);
        timer.SetActive(false);
    }
}
