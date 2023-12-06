using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCollisionCheck : MonoBehaviour
{
    public bool stillExited = false;
    InGameUI IGUI;

    // Start is called before the first frame update
    void Start()
    {
        IGUI = FindObjectOfType<InGameUI>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (stillExited == true)
        {
            IGUI.Countdown();
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("exit");
            IGUI.ShowCountdownTimer();
            stillExited = true;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("enter");
            IGUI.NoCountdownTimer();
            stillExited = false;
        }
    }
}