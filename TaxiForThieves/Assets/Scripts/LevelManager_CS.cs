using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager_CS : MonoBehaviour
{
    public static LevelManager_CS instance = null;
    [Header("CRIM")]
    public GameObject crim;
    GameObject currentDropOff;
    public Transform[] spawns;
    public bool playerhasCrim;
    public bool timeRestrictionOn;
    public bool crimModel;
    public int totalNumberOfCrims = 9;
    public int goalNuberOfCrims;
    int currentCrimIndex;
    public int crimsRemaining;
    int crimsDroppedOff;
    public int timerMinPickupSec, timerMaxSecPickupSec;

    [Header("COP")]
    Transform[] copsSpawnPoints;
    Transform[] cops;

    [Header("CAM")]
    public Transform cameraStartPosition;
    public CinemachineVirtualCamera cam;

    [Header("RADAR")]
    public Transform radarRotate;



    [Header("OTHER")]
    int playerPoints;
    bool gamePaused;

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
        instance = this;
    }

    void Start()
    {
        if (totalNumberOfCrims > 9)
            totalNumberOfCrims = 9;
        crimsRemaining = totalNumberOfCrims;
        currentCrimIndex = 0;
        crimsDroppedOff = 0;
        playerPoints = 0;
        gamePaused = false;
        GameUI_CS.instance.UpdateCrimsCounter(crimsDroppedOff, goalNuberOfCrims);
        SpawnACrim();
        GameUI_CS.instance.SetCrimSliderAt(0);
        GetCops();
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            SceneLoader.LoadMainMenu();
        }

        if (Input.GetKey(KeyCode.I) && !gamePaused)
        {
            PlayerController.instance.ResetPosition();
            cam.ForceCameraPosition(cameraStartPosition.position, Quaternion.Euler(new Vector3(cameraStartPosition.rotation.eulerAngles.x, 0, 0)));
        }
        if (Input.GetKeyDown(KeyCode.O) && !gamePaused)
        {
            AddPoints(100);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gamePaused)
                ResumeGame();
            else
                PauseGame();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            StartCoroutine("GameHoldFor", 3);
        }
    }
    void PauseGame()
    {
        GameUI_CS.instance.FreezeTimer(true);
        GameUI_CS.instance.PauseUi(true);
        gamePaused = true;
        Time.timeScale = 0;
    }
    void ResumeGame()
    {
        GameUI_CS.instance.FreezeTimer(false);
        GameUI_CS.instance.PauseUi(false);
        gamePaused = false;
        Time.timeScale = 1;
    }
    void ReStartLevel()
    {
        if (totalNumberOfCrims > 9)
            totalNumberOfCrims = 9;
        crimsRemaining = totalNumberOfCrims;
        currentCrimIndex = 0;
        crimsDroppedOff = 0;
        playerPoints = 0;
        GameUI_CS.instance.UpdateCrimsCounter(crimsDroppedOff, goalNuberOfCrims);
        SpawnACrim();
        GameUI_CS.instance.SetCrimSliderAt(0);
        GameUI_CS.instance.ResetIconsColor();
        PlayerController.instance.ResetPosition();
        cam.ForceCameraPosition(cameraStartPosition.position, Quaternion.Euler(new Vector3(cameraStartPosition.rotation.eulerAngles.x, 0, 0)));
        ResetCopsStartPoints();
    }

    public void SpawnACrim()
    {
        if (crimsRemaining == 0)
        {
            if (crimsDroppedOff >= goalNuberOfCrims)
                StartCoroutine("GameWin");
            else
                StartCoroutine("GameOver");
        }
        crimsRemaining--;

        Instantiate(crim, spawns[currentCrimIndex]);
        crimModel = !crimModel;

        PlayerController.instance.indicatorTarget = spawns[currentCrimIndex].transform.position;
        print("Crim location arrow.");
    }

    public GameObject GetDropOff()
    {
        currentDropOff = spawns[currentCrimIndex].transform.GetChild(0).gameObject;
        return currentDropOff;
    }

    public void CrimPickedUp()
    {
        currentCrimIndex++;
        playerhasCrim = true;
        GameUI_CS.instance.haveCrim = true;
        GameUI_CS.instance.StartTimer(Random.Range(timerMinPickupSec, timerMaxSecPickupSec));
        PlayerController.instance.indicatorTarget = currentDropOff.transform.position;
        GameUI_CS.instance.SetCrimSliderAt(currentCrimIndex);
    }
    public void CrimDroppedOff()
    {
        crimsDroppedOff++;
        GameUI_CS.instance.UpdateCrimsCounter(crimsDroppedOff, goalNuberOfCrims);
        playerhasCrim = false;
        SpawnACrim();
        GameUI_CS.instance.haveCrim = false;
        GameUI_CS.instance.SetCrimSliderAt(0);
        int remainingTime = GameUI_CS.instance.StopTimer();
        AddPoints(100 + remainingTime * 2);
        GameUI_CS.instance.SetIconToGreen(currentCrimIndex - 1);
    }

    public void ResetPlayerLost()
    {
        playerhasCrim = false;
        GameUI_CS.instance.StopTimer();
        //PlayerController.instance.ResetPosition();
        //PlayerController.instance.ActivateIndicator(false);
        //cam.ForceCameraPosition(cameraStartPosition.position, Quaternion.Euler(new Vector3(cameraStartPosition.rotation.eulerAngles.x, 0, 0)));
        StartCoroutine("GameHoldFor", 3);
        GameUI_CS.instance.ShowErrorMsg();
        GameUI_CS.instance.SetCrimSliderAt(0);
        GameUI_CS.instance.SetIconToRed(currentCrimIndex - 1);
        currentDropOff.SendMessage("Deactivate");
        SpawnACrim();
    }
    void AddPoints(int pointsToAdd)
    {
        playerPoints += pointsToAdd;
        PlayerController.instance.DropSomeLoot(7);
        StartCoroutine(GameUI_CS.instance.UpdatePointsCounter(playerPoints, 2.0f));
    }

    void ResetCopsStartPoints()
    {
        if (cops.Length > copsSpawnPoints.Length)
            print("Not enough cops spawn points");
        else
        {
            for (int i = 0; i < cops.Length; i++)
            {
                cops[i].position = copsSpawnPoints[i].position;
                cops[i].rotation = copsSpawnPoints[i].rotation;
            }
        }
    }
    void GetCops()
    {
        GameObject allCops = GameObject.Find("Cops");
        cops = new Transform[allCops.transform.childCount];
        for (int i = 0; i < allCops.transform.childCount; i++)
        {
            cops[i] = allCops.transform.GetChild(i);
        }

        allCops = GameObject.Find("CopsSpawnPoints");
        copsSpawnPoints = new Transform[allCops.transform.childCount];
        for (int i = 0; i < allCops.transform.childCount; i++)
        {
            copsSpawnPoints[i] = allCops.transform.GetChild(i);
        }
    }

    void HoldAllCops()
    {
        foreach(Transform cop in cops)
        {
            cop.gameObject.SendMessage("HoldCop");
        }
    }

    IEnumerator GameOver()
    {
        GameUI_CS.instance.ShowGameOver();
        yield return new WaitForSecondsRealtime(5);
        SceneLoader.LoadMainMenu();
    }

    IEnumerator GameWin()
    {
        GameUI_CS.instance.ShowGameWin();
        yield return new WaitForSecondsRealtime(5);
        SceneLoader.LoadMainMenu();
    }

    IEnumerator GameHoldFor(int sec)
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(sec);
        Time.timeScale = 1;
    }
    public void TimeOver()
    {
        if (timeRestrictionOn)
        {
            GameUI_CS.instance.ShowErrorMsg();
            GameUI_CS.instance.SetCrimSliderAt(0);
            GameUI_CS.instance.SetIconToRed(currentCrimIndex - 1);
            currentDropOff.SendMessage("Deactivate");
            SpawnACrim();
        }
    }
}