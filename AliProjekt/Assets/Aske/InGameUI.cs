using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                 
using UnityEngine.SceneManagement;     
using TMPro;

public class InGameUI : MonoBehaviour
{
    public float playerHealth = 100;
    public float removeHealth = 1;
    private float fullHealth = 100;
    public float countdownFloat = 60;
    //public float refillHealth = 25;

    public TMP_Text countdownNumberText;
    public TMP_Text countdownText;
    public TMP_Text healthNumberText;

    /*
    public Slider playerOneFuelSlider, playerTwoFuelSlider; 
    public Image playerOneFuelIcon, playerTwoFuelIcon;     
    public Image playerOneFill, playerTwoFill;              
    public TMP_Text trashCounterText;                     
    public TMP_Text playerOneFuelText, playerTwoFuelText;
    */

    public void RemoveHealthPlayer()                             
    {
        playerHealth = playerHealth - removeHealth * Time.deltaTime;
        healthNumberText.text = playerHealth.ToString("F0");

        if (playerHealth <= 0)                                        
            EmptyPlayerHealth();                                      
    }

    public void EmptyPlayerHealth()                                  
    {
        Debug.Log("ded");
        //gameover                                  
    }

    public void Countdown()
    {
        if (countdownFloat >= 0)
        {
            countdownFloat -= Time.deltaTime;
            countdownNumberText.text = countdownFloat.ToString("F0");
        }
        else
        {
            Debug.Log("stop");
            // countdown over action
        }
    }

    public void ShowCountdownTimer()
    {
        countdownText.gameObject.SetActive(true);
        countdownNumberText.gameObject.SetActive(true);
    }

    public void NoCountdownTimer()
    {
        countdownText.gameObject.SetActive(false);
        countdownNumberText.gameObject.SetActive(false);
        countdownFloat = 60;
    }
}