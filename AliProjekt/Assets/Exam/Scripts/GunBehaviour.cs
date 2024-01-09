using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunBehaviour : MonoBehaviour
{
    public float timeBeforeDraw = 3.0f; // Set the desired duration in seconds in THE INSPECTOR

    public bool readyToShoot = false;
    public float hitTimer = 0.0f;

    //public AudioClip ShootingAudio;

    public InputActionProperty trigger;

    public Transform bulletOrigin;

    public ParticleSystem gunShotParticles;

    public float coolDownTimer = 10;

    public int bulletsLeft = 6;

    public bool gunGrabbed = false;
    public bool countdownStarted = false;


    RaycastHit hit;

    InGameUI IGUI;
    AreaCollisionCheck areaCollisionCheck;

    private void Start()
    {
        IGUI = FindObjectOfType<InGameUI>(); //Gains access to the InGameUI script
        areaCollisionCheck = FindObjectOfType<AreaCollisionCheck>();
    }


    // Update is called once per frame
    void Update()
    {
        HasWaitedOnDraw();

        if (readyToShoot)
        {
            Shoot();
        }        
    }

    public void HasWaitedOnDraw() 
    {
        //If raycast from gun hits anything within 300f distance, and gun is grabbed, and area is not exited
        if (Physics.Raycast(bulletOrigin.transform.position, bulletOrigin.transform.forward, out hit, 300f) && gunGrabbed == true && areaCollisionCheck.stillExited == false)
        {
            // Check if the hit object is the collider on the ground named "Trigger"
            if (hit.collider.CompareTag("Trigger"))
            {             
                // Increment the timer
                hitTimer += Time.deltaTime;
                countdownStarted = true;

                // Check if the timer has exceeded the desired duration
                if (hitTimer+1 >= timeBeforeDraw) //+1 to match countdown timer from InGameUI
                {                    
                    // Perform actions when the target has been hit for the specified duration
                    readyToShoot = true;
                }
            }
            else
            {
                countdownStarted = false;
                hitTimer = 0f; //Timer reset
            }
        }
    }

    public void GunGrabbedTrue() //Method assigned to interactable event on gun and runs when gun is grabbed
    {
        gunGrabbed = true;        
    }

    public void GunGrabbedFalse() //Method assigned to interactable event on gun and runs when gun is ungrabbed
    {
        gunGrabbed = false;
    }

    public void Shoot()
    {
        trigger.action.started += ctx => //When the trigger button is pressed, run the following
        {
            if (gunGrabbed == true && bulletsLeft > 0)
            {
                GetComponent<AudioSource>().Play(); //PlayOneShot(ShootingAudio);

                //If something is hit within 300f distance
                if (Physics.Raycast(bulletOrigin.transform.position, bulletOrigin.transform.forward, out hit, 300f))
                {
                    if (hit.collider.CompareTag("Enemy")) 
                    {
                        IGUI.RemoveHealthEnemy();
                    }                        
                }
                bulletsLeft--;
                IGUI.RemoveBullet();

                gunShotParticles.Play();

                
            }
            else if (bulletsLeft == 0)
            {
                StartCoroutine(CoolDownTimer());
            }
        };
    }

   
    public IEnumerator CoolDownTimer()
    {   
        //Waits x amount of seconds before the following lines are run
        yield return new WaitForSeconds(coolDownTimer);

        bulletsLeft = 6;
        IGUI.RemoveBullet();
    }
}
