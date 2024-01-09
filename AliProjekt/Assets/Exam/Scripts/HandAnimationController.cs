using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //Unity's new inputsystem

public class HandAnimationController : MonoBehaviour //This script is to be placed on each handModel
{
    public InputActionProperty grabAnimation; //Reference to the button that triggers the animation assigned in inspector

    public Animator handAnimation; //Reference to the animator component that should be triggered assigned in inspector

    void Update()
    {
        float grabValue = grabAnimation.action.ReadValue<float>(); //The float value representíng how much the button is pressed

        handAnimation.SetFloat("Grab", grabValue); //Blendtree float in "Grab" animation set according to the grabValue
    }
}
