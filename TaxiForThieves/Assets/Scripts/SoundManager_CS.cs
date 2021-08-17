using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager_CS : MonoBehaviour
{
    public static SoundManager_CS instance = null;
    public AudioSource backgroundMusic, backgroundMenuMusic;
    public AudioSource policeSirensSound;
    public AudioSource jailSound;
    public AudioSource endGameSound1, endGameSound2;
    public AudioSource dropOffSound1, dropOffSound2;
    public AudioSource crimPickUpSound1, crimPickUpSound2;
    public AudioSource pickupSound;
    public AudioSource mudSound, disguiseSound;
    public AudioSource speedBoostSound;
    public AudioSource policeGotUsSound;
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
    }
    public void StartMusic()
    {
        backgroundMusic.loop = true;
        backgroundMusic.Play();
    }
    public void StartMenuMusic()
    {
        backgroundMenuMusic.loop = true;
        backgroundMenuMusic.Play();
    }
    public void PlayPoliceSirensSound(bool play)
    {
        if(play)
        {
            backgroundMusic.volume = 0.5f;
            StartCoroutine("PlayPoliceLoop");
        }
        else
        {
            backgroundMusic.volume = 1f;
            StopCoroutine("PlayPoliceLoop");
            policeSirensSound.Stop();
        }

    }
    IEnumerator PlayPoliceLoop()
    {
        while(true)
        {
            policeSirensSound.Play();
            yield return new WaitForSecondsRealtime(19);
            policeSirensSound.Play();
            yield return new WaitForSecondsRealtime(19);
        }

    }
    public void PlayJailSound()
    {
        jailSound.time = 1;
        jailSound.Play();
    }
    public void PlayEndGameSound()
    {
        StartCoroutine("PlayEndGame");
    }
    IEnumerator PlayEndGame()
    {
        backgroundMusic.Stop();
        endGameSound1.Play();
        yield return new WaitForSecondsRealtime(3);
        endGameSound2.Play();
    }
    public void PlayDropOffSound()
    {
        StartCoroutine("PlayDropOff");
    }
    IEnumerator PlayDropOff()
    {
        backgroundMusic.volume = 0.5f;
        dropOffSound1.Play();
        yield return new WaitForSecondsRealtime(1);
        dropOffSound2.Play();
        yield return new WaitForSecondsRealtime(1);
        backgroundMusic.volume = 1f;
    }
    IEnumerator PlayPickup()
    {
        int r = Random.Range(1, 3);
        backgroundMusic.volume = 0.5f;
        if(r ==1)
            crimPickUpSound1.Play();
        else
            crimPickUpSound2.Play();
        yield return new WaitForSecondsRealtime(2);
        backgroundMusic.volume = 1f;
    }
    public void PlayPickupSound()
    {
        pickupSound.Play();
    }
    public void PlayCrimPickupSound()
    {
        StartCoroutine("PlayPickup");
    }
    public void PlayMudSound()
    {
        mudSound.Play();
    }
    public void SpeedBoostSound()
    {
        speedBoostSound.Play();
    }
    public void PlayDisguiseSound()
    {
        disguiseSound.Play();
    }

    public void PlayPoliceGotUsSound()
    {
        policeGotUsSound.Play();
    }
}
