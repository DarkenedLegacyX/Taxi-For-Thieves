using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    static public SceneLoader instance;

    void Awake()
    { 
        instance = this; 
    }

    public void LoadLevel(int level)
    {
        string levelName = "Level" + level.ToString();
        SceneManager.LoadScene(levelName);
        //SceneManager.LoadScene("CarScene");
    }
    public void LoadLevelAsync(int level, GameObject loadingPanel, Slider loadingSlider)
    {
        string levelName = "Level" + level.ToString();
        instance.StartCoroutine(LoadSceneAsync(levelName, loadingPanel, loadingSlider));
        //instance.StartCoroutine(LoadSceneAsync("CarScene", loadingPanel, loadingSlider));
        //SceneManager.LoadScene("CarScene");
    }
    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    IEnumerator LoadSceneAsync(string levelName, GameObject loadingPanel, Slider loadingSlider)
    {
        loadingPanel.SetActive(true);

        AsyncOperation op = SceneManager.LoadSceneAsync(levelName);

        while (!op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress / .9f);
            loadingSlider.value = progress;
            //loadingText.text = progress * 100f + "%";

            yield return null;
        }
    }
}
