using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI_CS : MonoBehaviour
{
    public static GameUI_CS instance = null;
    public Text crimText;
    public bool haveCrim = false;
    public Text errorText, livesText, gameOverText;
    

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


    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
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

    public void UpdateLives(int lives)
    {
        livesText.text = lives.ToString();
    }

    public void ShowErrorMsg()
    {
        StartCoroutine(ShowErrorText());
    }
    public void ShowGameOver()
    {
        gameOverText.gameObject.SetActive(true);
    }
}
