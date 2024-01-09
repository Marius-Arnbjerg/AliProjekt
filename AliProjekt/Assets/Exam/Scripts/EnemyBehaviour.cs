using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyBehaviour : MonoBehaviour
{
	public AudioClip ShootingAudio;

	public Transform bulletOrigin;
	public Transform player;

	GunBehaviour gb;

	Animator shootAnimation;

	public ParticleSystem gunShotParticles;

	private float nextFireTime;
	public float fireRate = 2; //Increase for longer duration between shots
	public float rotationSpeed = 1; //Increase to make him rotate faster
	public float drawSpeed = 1f; //Increase this to make enemy draw faster

	public LayerMask layers;
	
	RaycastHit hit;

	InGameUI IGUI;
	

    private void Awake()
    {
		nextFireTime = Time.time;

		IGUI = FindObjectOfType<InGameUI>();
		gb = FindObjectOfType<GunBehaviour>();

		shootAnimation = GetComponent<Animator>();
	}

    void Start()
	{
		shootAnimation.speed = 0;
	}

	// Update is called once per frame
	void Update()
	{
		if (gb.readyToShoot)
        {
			shootAnimation.speed = drawSpeed;
			CheckIfTimeToFire();
			StartCoroutine(RotateTowardsPlayer());
			
		}
	}

	private IEnumerator RotateTowardsPlayer()
    {
		Quaternion lookRotation = Quaternion.LookRotation(player.position - transform.position);

		float time = 0;

        while (time < 1)
        {
			transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, time);

			time += Time.deltaTime * rotationSpeed;

			yield return null;
        }
    }

	void CheckIfTimeToFire()
	{
        if (shootAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime > 1) //Shoot first shot when animation is done ("GetCurrentAnimatorStateInfo(0)" refers to baselayer in the animator.
		{
			if (Time.time > nextFireTime) //If the time since awake is larger than nextFireTime (This will always be true the first time the enemy shoots)
			{
				GetComponent<AudioSource>().Play(); //PlayOneShot(ShootingAudio); //Play shoot audio
				gunShotParticles.Play(); //Play particles

				if (Physics.Raycast(bulletOrigin.transform.position, bulletOrigin.transform.forward, out hit, 300f, layers)) //If the raycast from bulletorigin hits the "playerLayer" assigned to the layers variable in the inspector
				{
					IGUI.RemoveHealthPlayer(); //Remove player health
					hit.transform.GetComponent<GetHit>().GettingHit(); //Accesses the GetHit script component on the player, and calls the GettingHit() method.
				}

				nextFireTime = Time.time + fireRate; //nextFireTime is set to a value that is x (fireRate) greater than the current time. 
			}
		}
	}
}
