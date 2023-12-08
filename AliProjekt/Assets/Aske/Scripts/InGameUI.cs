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
    private float countdownFloat;
    public float startCountdownFloat = 10;
    public int bulletCount = 6;

    private float drawCountdown;

    //public float refillHealth = 25;
    //private float fullHealth = 100;

    public float enemyHealth = 100;

    public TMP_Text countdownNumberText;
    public TMP_Text countdownText;
    public TMP_Text healthNumberText;
    public TMP_Text bulletNumberText;
    public TMP_Text drawCountdownText;

    public TMP_Text enemyHealthNumberText;

    public Text winHeaderText;
    public GameObject winMenu;
    public GameObject gameOverMenu;
    public GameObject pauseMenu;
    private bool pausedGame = false;

    GunBehaviour GB;
    AreaCollisionCheck AreaColCheck;
    /*
    public Slider playerOneFuelSlider, playerTwoFuelSlider; 
    public Image playerOneFuelIcon, playerTwoFuelIcon;     
    public Image playerOneFill, playerTwoFill;              
    public TMP_Text trashCounterText;                     
    public TMP_Text playerOneFuelText, playerTwoFuelText;
    */

    private void Awake()
    {
        Time.timeScale = 1;
        GB = FindObjectOfType<GunBehaviour>();
        AreaColCheck = FindObjectOfType<AreaCollisionCheck>();

        bulletNumberText.text = bulletCount.ToString();
        enemyHealthNumberText.text = enemyHealth.ToString();
        countdownText.gameObject.SetActive(false);
        countdownNumberText.gameObject.SetActive(false);
        countdownNumberText.text = countdownFloat.ToString("F0");
        healthNumberText.text = playerHealth.ToString();
    }

    private void Start()
    {
        countdownFloat = startCountdownFloat;
        drawCountdown = GB.timeBeforeDraw;
    }

    private void Update()
    {
        TimeBeforeDrawCountdown();
    }

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

    public void GameOverMenu()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void WinMenu()
    {
        winHeaderText.text = "Level " + SceneManager.GetActiveScene().buildIndex.ToString() + " complete!";
        winMenu.SetActive(true);
        Time.timeScale = 0;
    }
        
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        GameOverMenu();                        
    }

    public void TimeBeforeDrawCountdown()
    {
        if(GB.countdownStarted && drawCountdown > 0f && GB.readyToShoot == false && GB.gunGrabbed && !AreaColCheck.stillExited)
        {
            drawCountdownText.gameObject.SetActive(true);
            drawCountdown -= Time.deltaTime;
            drawCountdownText.text = drawCountdown.ToString("F0");
        }
        else
        {
            drawCountdown = GB.timeBeforeDraw;
            drawCountdownText.gameObject.SetActive(false);
        }
        /*
        else if(!GB.countdownStarted && GB.readyToShoot == false)
        {
            drawCountdown = GB.timeBeforeDraw;
            drawCountdownText.gameObject.SetActive(false);
        }
        else if(GB.readyToShoot == true)
        {
            drawCountdownText.gameObject.SetActive(false);
        }*/
    }

    public void OutsideAreaCountdown()
    {
        if (countdownFloat >= 0)
        {
            countdownFloat -= Time.deltaTime;
            countdownNumberText.text = countdownFloat.ToString("F0");
        }
        else
        {
            GameOverMenu();
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
        countdownFloat = startCountdownFloat;
    }
    public void RemoveHealthEnemy()
    {
        enemyHealth = enemyHealth - removeHealth;
        enemyHealthNumberText.text = enemyHealth.ToString();

        if (enemyHealth <= 0)
            EmptyEnemyHealth();
    }
    public void EmptyEnemyHealth()
    {
        WinMenu();
    }
    public void RemoveBullet()
    {
        bulletNumberText.text = GB.bulletsLeft.ToString();
    }
}