using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MaxScores
{
    public static string levelName = "Level";

    public static int GetMaxScoreLvl(int lvlNo)
    {
        return PlayerPrefs.GetInt(levelName + lvlNo.ToString());
    }

    public static bool SaveMaxScoreLvl(int score, int lvlNo)
    {
        if(score > PlayerPrefs.GetInt(levelName + lvlNo.ToString()))
        {
            PlayerPrefs.SetInt(levelName + lvlNo.ToString(), score);
            return true;
        }
        else
        {
            return false;
        }
        
    }
}
