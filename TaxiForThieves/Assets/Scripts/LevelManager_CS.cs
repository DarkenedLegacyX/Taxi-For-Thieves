using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager_CS : MonoBehaviour
{
    public static LevelManager_CS instance = null;

    public GameObject crim;
    public bool playerhasCrim;

    public GameObject[] dropOffPoints;
    public Transform[] spawns;

    public Transform cameraStartPosition;
    public CinemachineVirtualCamera cam;

    public Transform radarRotate;

    public int totalNumberOfCrims;
    public int playerLife;
    int activeDropOffId;

    int currentCrimIndex;


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

        DontDestroyOnLoad(gameObject);

    }

    void Start()
    {
        currentCrimIndex = 0;
        SpawnACrim();
        GameUI_CS.instance.SetCrimSliderAt(0);
    }

    void Update()
    {
        if(Input.GetKey("escape"))
        {
            SceneLoader.LoadMainMenu();
        }

        if (Input.GetKey(KeyCode.P))
        {
            PlayerController.instance.ResetPosition();
            cam.ForceCameraPosition(cameraStartPosition.position, Quaternion.Euler(new Vector3(cameraStartPosition.rotation.eulerAngles.x, 0, 0)));
        }
        if (Input.GetKey(KeyCode.O))
        {
            //ResetPlayerLost();
        }

        //radarRotate.Rotate(Vector3.one * 4 * Time.deltaTime);
    }

    public void SpawnACrim()
    {
        int rand = Random.Range(0, (spawns.Length - 1));
        Instantiate(crim, spawns[rand]);

        PlayerController.instance.indicatorTarget = spawns[rand].transform.position;
        print("Crim location arrow.");
        //crim.transform.position = new Vector3(spawns[rand].y, 0, 0);
    }

    public GameObject GetRandomDropOff()
    {
        activeDropOffId = Random.Range(0, (dropOffPoints.Length - 1));
        PlayerController.instance.indicatorTarget = dropOffPoints[activeDropOffId].transform.position;
        return dropOffPoints[activeDropOffId];
    }

    public void CrimPickedUp()
    {
        currentCrimIndex++;
        playerhasCrim = true;
        GameUI_CS.instance.haveCrim = true;
        GameUI_CS.instance.UpdateUI();
        GameUI_CS.instance.SetCrimSliderAt(currentCrimIndex);
    }
    public void CrimDroppedOff()
    {
        playerhasCrim = false;
        SpawnACrim();
        GameUI_CS.instance.haveCrim = false;
        GameUI_CS.instance.UpdateUI();
        GameUI_CS.instance.SetCrimSliderAt(0);
        GameUI_CS.instance.SetIconToGreen(currentCrimIndex - 1);
    }

    public void ResetPlayerLost()
    {
        playerLife--;
        if (playerLife == 0)
            StartCoroutine(GameOver());
        else
        {
            PlayerController.instance.ResetPosition();
            PlayerController.instance.ActivateIndicator(false);
            cam.ForceCameraPosition(cameraStartPosition.position, Quaternion.Euler(new Vector3(cameraStartPosition.rotation.eulerAngles.x, 0, 0)));
            GameUI_CS.instance.ShowErrorMsg();
            //GameUI_CS.instance.UpdateLives(playerLife);
            GameUI_CS.instance.SetCrimSliderAt(0);
            GameUI_CS.instance.SetIconToRed(currentCrimIndex - 1);
            dropOffPoints[activeDropOffId].SendMessage("Deactivate");
            SpawnACrim();
        }
    }

    IEnumerator GameOver()
    {
        GameUI_CS.instance.ShowGameOver();
        yield return new WaitForSecondsRealtime(5);
        //SceneLoader.LoadMainMenu();
    }
}
