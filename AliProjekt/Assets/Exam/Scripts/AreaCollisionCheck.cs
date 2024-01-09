using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCollisionCheck : MonoBehaviour
{
    public bool stillExited = false;
    InGameUI IGUI;


    [Range(0, 50)]
    public int segments = 50;

    public float lineRadius = 1; //Assigned in inspector

    LineRenderer line;

    BoxCollider boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        IGUI = FindObjectOfType<InGameUI>(); //Acceses the InGameUI script

        line = gameObject.GetComponent<LineRenderer>(); //Accesses the lineRenderer
        boxCollider = gameObject.GetComponent<BoxCollider>();

        boxCollider.size = new Vector3(lineRadius, 0.3f, lineRadius);

        //Try placing in Start
        line.positionCount = segments + 1; //LineRenderer parameter deciding the amount of points in the lineRenderer
        line.useWorldSpace = false;

        CreatePoints();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (stillExited == true)
        {
            IGUI.OutsideAreaCountdown();
        }
    }

    //If leaving the area (the collider of this gameobject), show the countdowntimer and set Still exited true 
    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IGUI.ShowCountdownTimer();
            stillExited = true;
        }
    }

    //If entering the area (the collider of this gameobject), hide the countdowntimer and set Still exited false
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IGUI.NoCountdownTimer();
            stillExited = false;
        }
    }


    //Draws a circle with a lineRenderer, source: https://gamedev.stackexchange.com/questions/126427/draw-circle-around-gameobject-to-indicate-radius
    void CreatePoints()
    {
        float x;
        float y;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * lineRadius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * lineRadius;

            line.SetPosition(i, new Vector3(x, 0, y));

            angle += (360f / segments);
        }
    }
}