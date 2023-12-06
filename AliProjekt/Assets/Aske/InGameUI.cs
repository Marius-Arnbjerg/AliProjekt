using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                 
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class InGameUI : MonoBehaviour
{
    public int playerHealth = 100;
    public int removeHealth = 1;
    public float countdownFloat = 60;
    public int bulletCount = 6;
    //public float refillHealth = 25;
    //private float fullHealth = 100;

    public float enemyHealth = 100;

    public TMP_Text countdownNumberText;
    public TMP_Text countdownText;
    public TMP_Text healthNumberText;
    public TMP_Text bulletNumberText;

    public TMP_Text enemyHealthNumberText;

    public GameObject pauseMenu;
    private bool pausedGame = false;

    /*
    public Slider playerOneFuelSlider, playerTwoFuelSlider; 
    public Image playerOneFuelIcon, playerTwoFuelIcon;     
    public Image playerOneFill, playerTwoFill;              
    public TMP_Text trashCounterText;                     
    public TMP_Text playerOneFuelText, playerTwoFuelText;
    */

    public void PauseButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PauseMenu();
        }
    }

    public void PauseMenu()
    {
        if (pausedGame == false)
        {
            pauseMenu.SetActive(true);
            pausedGame = true;
            Time.timeScale = 0;
        }
        else if (pausedGame == true)
        {
            pauseMenu.SetActive(false);
            pausedGame = false;
            Time.timeScale = 1;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RemoveHealthPlayer()                             
    {
        playerHealth = playerHealth - removeHealth;
        healthNumberText.text = playerHealth.ToString();

        if (playerHealth <= 0)                                        
            EmptyPlayerHealth();                                      
    }

    public void EmptyPlayerHealth()                                  
    {
        Debug.Log("player ded");
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
    public void RemoveHealthEnemy()
    {
        enemyHealth = enemyHealth - removeHealth;
        enemyHealthNumberText.text = enemyHealth.ToString();

        if (enemyHealth <= 0)
            EmptyPlayerHealth();
    }
    public void EmptyEnemyHealth()
    {
        Debug.Log("enemy ded");
        //game won
    }
    public void RemoveBullet()
    {
        bulletCount = bulletCount - removeHealth;
        bulletNumberText.text = bulletCount.ToString();

        if (bulletCount <= 0)
        {
            //empty bullets
        }
    }
}