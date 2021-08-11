using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPickedUp_CS : MonoBehaviour
{
    public static PowerPickedUp_CS instance = null;
    public GameObject pickupVFX;
    public float speed = 100;
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
        pickupVFX.SetActive(false);
    }

    private void Update()
    {
        transform.Rotate(0, Time.deltaTime * speed, 0, Space.Self);
    }

    IEnumerator PlayEffects()
    {
        print("SPARKLES!");
        pickupVFX.SetActive(true);
        yield return new WaitForSeconds(30f);
        pickupVFX.SetActive(false);
    }
}
