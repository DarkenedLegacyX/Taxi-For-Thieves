using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI_CS : MonoBehaviour
{
    public static GameUI_CS instance = null;
    public Text crimText;
    public bool haveCrim = false;

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
}
