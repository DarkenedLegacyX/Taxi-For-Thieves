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
    public bool playerIsDisguised;
    public bool timeRestrictionOn;
    public bool crimModel;
    public int totalNumberOfCrims = 9;
    public int goalNuberOfPoints;
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
    public bool isOnMud = false;

    [Header("RADAR")]
    public Transform radarRotate;



    [Header("OTHER")]
    int playerPoints;
    bool gamePaused, gameExitPaused;

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
        gameExitPaused = false;
        SpawnACrim();
        GameUI_CS.instance.SetCrimSliderAt(0);
        GameUI_CS.instance.UpdatePoints(goalNuberOfPoints);
        GetCops();
        StartCoroutine("HoldAlltheTraffic", 7);
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            GameUI_CS.instance.HideShowSureExit();
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
            //if (gamePaused)
            //    ResumeGame();
            //else
            //    PauseGame();
            GameUI_CS.instance.HideShowSureExit();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            StartCoroutine("GameEnd");
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
                StartCoroutine("GameEnd");
        }
        else
        {
            Instantiate(crim, spawns[currentCrimIndex]);
            crimModel = !crimModel;

            PlayerController.instance.indicatorTarget = spawns[currentCrimIndex].transform.position;
            print("Crim location arrow.");
        }
        crimsRemaining--;
    }

    public GameObject GetDropOff()
    {
        currentDropOff = spawns[currentCrimIndex].transform.GetChild(0).gameObject;
        return currentDropOff;
    }

    public void CrimPickedUp()
    {
        SoundManager_CS.instance.PlayCrimPickupSound();
        currentCrimIndex++;
        playerhasCrim = true;
        GameUI_CS.instance.haveCrim = true;
        GameUI_CS.instance.StartTimer(Random.Range(timerMinPickupSec, timerMaxSecPickupSec));
        PlayerController.instance.indicatorTarget = currentDropOff.transform.position;
        GameUI_CS.instance.SetCrimSliderAt(currentCrimIndex);
        SoundManager_CS.instance.PlayPoliceSirensSound(true);
        GameUI_CS.instance.StartCoroutine("ShowObjective");
    }
    public void CrimDroppedOff()
    {
        SoundManager_CS.instance.PlayPoliceSirensSound(false);
        SoundManager_CS.instance.PlayDropOffSound();
        crimsDroppedOff++;
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
        SoundManager_CS.instance.PlayPoliceSirensSound(false);
        playerhasCrim = false;
        GameUI_CS.instance.StopTimer();
        PlayerController.instance.PoofPoliceGotUs();
        SliderScriptAnim.instance.PlayPrisonerAnim(currentCrimIndex - 1);
        //PlayerController.instance.ResetPosition();
        //PlayerController.instance.ActivateIndicator(false);
        //cam.ForceCameraPosition(cameraStartPosition.position, Quaternion.Euler(new Vector3(cameraStartPosition.rotation.eulerAngles.x, 0, 0)));
        //StartCoroutine("GameHoldFor", 3);
        
        PlayerController.instance.StartCoroutine("HoldPlayer", 3);
        SendCopsToSpawnsPos();
        GameUI_CS.instance.ShowErrorMsg();
        GameUI_CS.instance.SetCrimSliderAt(0);
        //GameUI_CS.instance.SetIconToRed(currentCrimIndex - 1);
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
    void StopNPCs(bool stop)
    {
        GameObject NPCs = GameObject.Find("CarTraffic");
        int allNPCsCount = NPCs.transform.childCount;
        print(allNPCsCount);
        for (int i = 0; i < allNPCsCount; i++)
        {
            NPCs.transform.GetChild(i).gameObject.SendMessage("StopNPC", stop);
        }

    }

    void SendCopsToSpawnsPos()
    {
        foreach(Transform cop in cops)
        {
            cop.gameObject.SendMessage("GoBackToSpawn");
        }
    }

    IEnumerator GameHoldFor(int sec)
    {
        yield return new WaitForSecondsRealtime(sec);
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
    public void SetAllPoliceChaseSpeed(int newSpeed)
    {
        foreach (Transform cop in cops)
        {
            cop.gameObject.SendMessage("SetChaseSpeed", newSpeed);
        }
    }
    public void StopAllPolice(int seconds)
    {
        foreach (Transform cop in cops)
        {
            cop.gameObject.SendMessage("StopTheCop", seconds);
        }
    }
    public IEnumerator HoldAlltheTraffic(int seconds)
    {
        StopAllPolice(seconds);
        StopNPCs(false);
        PlayerController.instance.StartCoroutine("HoldPlayer", seconds);
        yield return new WaitForSecondsRealtime(seconds);
        StopNPCs(true);
    }
    IEnumerator GameEnd()
    {
        yield return new WaitForSecondsRealtime(2f);
        StartCoroutine("HoldAlltheTraffic", 7);
        GameUI_CS.instance.StartEndGamePanel();
        yield return new WaitForSecondsRealtime(4);
        GameUI_CS.instance.SetEndGameButtons();
        Time.timeScale = 0;
    }
}