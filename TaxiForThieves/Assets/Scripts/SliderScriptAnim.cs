using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderScriptAnim : MonoBehaviour
{
    public static SliderScriptAnim instance = null;
    public Animation[] prisonerIcon;
    private void Awake()
    {
        instance = this;
    }

    public void PlayPrisonerAnim(int iconIndex)
    {
        prisonerIcon[iconIndex].Play();
    }
}
