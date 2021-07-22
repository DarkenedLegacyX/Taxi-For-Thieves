using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrimHUD : MonoBehaviour
{
    public static CrimHUD instance = null;

    public GameObject[] crimsIcons;
    public int maxCrims;
    public Sprite iconDroppedOff, iconChased, iconLost;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        maxCrims = crimsIcons.Length;
    }

    public void ChangeCrimIconToChased(int crimNo)
    {
        crimsIcons[crimNo].GetComponent<Image>().sprite = iconChased;
    }
    public void ChangeCrimIconToDroppedOff(int crimNo)
    {
        crimsIcons[crimNo].GetComponent<Image>().sprite = iconDroppedOff;
    }
    public void ChangeCrimIconToDroppedLost(int crimNo)
    {
        crimsIcons[crimNo].GetComponent<Image>().sprite = iconLost;
    }

}
