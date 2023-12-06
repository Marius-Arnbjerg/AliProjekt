using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunBehaviour : MonoBehaviour
{
    public AudioClip ShootingAudio;

    public InputActionProperty trigger;
    public GameObject gun;

    public Transform bulletOrigin;

    public ParticleSystem gunShotParticles;

    public float coolDownTimer = 10;

    public int bulletsLeft = 6;

    private bool gunGrabbed = false;

    public LayerMask layers;

    RaycastHit hit;

    InGameUI IGUI;
    private void Start()
    {
        IGUI = FindObjectOfType<InGameUI>();
    }


    // Update is called once per frame
    void Update()
    {
        Shoot();
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
                if (Physics.Raycast(bulletOrigin.transform.position, bulletOrigin.transform.forward, out hit, 300f, layers))
                {
                    IGUI.RemoveHealthEnemy();
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
        bulletsLeft = 6;
    }
}
