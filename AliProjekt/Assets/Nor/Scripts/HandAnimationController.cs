using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimationController : MonoBehaviour
{

    public InputActionProperty grabAnimation;

    public Animator handAnimation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float grabValue = grabAnimation.action.ReadValue<float>();
        Debug.Log("Grab value" + grabValue);

        handAnimation.SetFloat("Grab", grabValue);

    }
}
