using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCollisionCheck : MonoBehaviour
{
    public bool stillExited = false;
    InGameUI IGUI;


    [Range(0, 50)]
    public int segments = 50;

    private float xradius = 5;

    private float yradius = 5;
    LineRenderer line;

    SphereCollider sphereCollider;
    // Start is called before the first frame update
    void Start()
    {
        IGUI = FindObjectOfType<InGameUI>();

        line = gameObject.GetComponent<LineRenderer>();

        sphereCollider = gameObject.GetComponent<SphereCollider>();

        xradius = sphereCollider.radius;
        yradius = sphereCollider.radius;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (stillExited == true)
        {
            IGUI.OutsideAreaCountdown();
        }

        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        CreatePoints();
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("exit");
            IGUI.ShowCountdownTimer();
            stillExited = true;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("enter");
            IGUI.NoCountdownTimer();
            stillExited = false;
        }
    }



    void CreatePoints()
    {
        float x;
        float y;
        float z;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

            line.SetPosition(i, new Vector3(x, y, 0));

            angle += (360f / segments);
        }
    }
}