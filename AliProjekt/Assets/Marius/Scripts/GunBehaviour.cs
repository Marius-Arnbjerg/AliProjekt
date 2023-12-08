using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunBehaviour : MonoBehaviour
{
    public float timeBeforeDraw = 3.0f; // Set the desired duration in seconds
    public bool readyToShoot = false;
    public float hitTimer = 0.0f;


    public AudioClip ShootingAudio;
    public AudioClip avSound;

    public InputActionProperty trigger;
    public GameObject gun;

    public Transform bulletOrigin;

    public ParticleSystem gunShotParticles;

    public float coolDownTimer = 10;

    public int bulletsLeft = 6;

    private bool gunGrabbed = false;
    public bool countdownStarted = false;

    public LayerMask enemyMask;
    public LayerMask triggerMask;

    RaycastHit hit;

    InGameUI IGUI;
    AreaCollisionCheck areaCollisionCheck;

    private void Start()
    {
        IGUI = FindObjectOfType<InGameUI>();
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
        if (Physics.Raycast(bulletOrigin.transform.position, bulletOrigin.transform.forward, out hit, 300f) && gunGrabbed == true && areaCollisionCheck.stillExited == false)
        {
            // Check if the hit object is the target you're interested in
            if (hit.collider.CompareTag("Trigger"))
            {             
                // Increment the timer
                hitTimer += Time.deltaTime;
                countdownStarted = true;

                // Check if the timer has exceeded the desired duration
                if (hitTimer >= timeBeforeDraw)
                {                    
                    // Perform actions when the target has been hit for the specified duration
                    readyToShoot = true;
                }
            }
            else
            {
                countdownStarted = false;
                hitTimer = 0f;
            }
        }
    }

    public void GunGrabbedTrue()
    {
        gunGrabbed = true;        
    }

    public void GunGrabbedFalse()
    {
        gunGrabbed = false;
    }

    public void Shoot()
    {
        trigger.action.started += ctx =>
        {
            if (gunGrabbed == true && bulletsLeft > 0)
            {
                GetComponent<AudioSource>().PlayOneShot(ShootingAudio);
                if (Physics.Raycast(bulletOrigin.transform.position, bulletOrigin.transform.forward, out hit, 300f))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        GetComponent<AudioSource>().PlayOneShot(avSound);
                        IGUI.RemoveHealthEnemy();
                    }                        
                }
                bulletsLeft--;

                gunShotParticles.Play();

                if(bulletsLeft == 0)
                {
                    StartCoroutine(CoolDownTimer());
                }
            }
        };            
    }

    public IEnumerator CoolDownTimer()
    {   
        yield return new WaitForSeconds(coolDownTimer);
        //reloadSound.Play();
        bulletsLeft = 6;
    }
}
