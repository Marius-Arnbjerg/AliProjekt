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

	private Coroutine LookCoroutine;

	public ParticleSystem gunShot;

	private float nextFire;
	public float fireRate = 2;
	public float rotationSpeed = 1;
	public float drawSpeed = 1f;

	public LayerMask layers;
	
	RaycastHit hit;

	InGameUI IGUI;
	

    private void Awake()
    {
		nextFire = Time.time;

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
        if (shootAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime > 1) //Shoot first shot when animation is done
		{
			if (Time.time > nextFire)
			{
				GetComponent<AudioSource>().PlayOneShot(ShootingAudio);
				
				if (Physics.Raycast(bulletOrigin.transform.position, bulletOrigin.transform.forward, out hit, 300f, layers))
				{
					IGUI.RemoveHealthPlayer();
					hit.transform.GetComponent<GetHit>().GettingHit();
					
				
				}

				gunShot.Play();

				nextFire = Time.time + fireRate;
			}
		}

	}
}
