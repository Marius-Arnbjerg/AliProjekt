using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHit : MonoBehaviour
{
    public GameObject hitScreen;

    public void GettingHit()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        hitScreen.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        hitScreen.SetActive(false);
    }
}
