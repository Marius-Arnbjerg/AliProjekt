using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public GameObject gun;
    RaycastHit hit;
    public LayerMask layers;

    public enum GameState
    {
        initialState,
        countDownState,
        ShootingState,
        EndState,
    }
   

    void Start()
    {
        
    }


    
    // Update is called once per frame
    void Update()
    {

    }

    public void SwitchGameState(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.initialState:

                break;
        }
    }

    public bool InitialState()
    {
        if(Physics.Raycast(gun.transform.position, gun.transform.forward, out hit, layers))
        {
            return true;
        }
        else
            return false;
    }

}
