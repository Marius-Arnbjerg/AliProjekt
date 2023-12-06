using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshAgent : MonoBehaviour
{
    private NavMeshAgent enemyNavMeshAgent;

    //public GameObject enemy;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyNavMeshAgent.target = target;
    }
}
