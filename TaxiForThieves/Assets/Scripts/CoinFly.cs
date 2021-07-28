using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFly : MonoBehaviour
{
    void Start()
    {
        transform.Rotate(90, 0, 0);
        //yield return new WaitForSeconds(Random.Range(0.2f, 0.8f));
        StartCoroutine("FlyToUI");
    }

    void FixedUpdate()
    {
        transform.Rotate(0, 0, Random.Range(1, 4));
    }

    IEnumerator FlyToUI()
    {
        float t = 0;

        Transform refCube = Camera.main.transform.Find("ColectCube");
        transform.parent = Camera.main.transform;
        Vector3 posA = transform.localPosition;
        Vector3 posB = refCube.localPosition;

        while (t < 1)
        {
            transform.localPosition = Vector3.Lerp(posA, posB, Mathf.SmoothStep(0, 1, t));
            t += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
