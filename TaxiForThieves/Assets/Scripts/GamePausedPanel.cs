using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePausedPanel : MonoBehaviour
{
    public Button exitButton, resumeButton;
    void Start()
    {
        exitButton.onClick.AddListener(SceneLoader.LoadMainMenu);
        resumeButton.onClick.AddListener(GameUI_CS.instance.HideShowSureExit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
