using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionCheck : MonoBehaviour
{
    public LayerMask layers;
    RaycastHit hit;
    public GameObject Player;

    InGameUI IGUI;

    // Start is called before the first frame update
    void Start()
    {
        IGUI = FindObjectOfType<InGameUI>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, 300f, layers))
        {
            Debug.Log(hit.transform.name);
            IGUI.RemoveHealthPlayer();
        }
    }
}
