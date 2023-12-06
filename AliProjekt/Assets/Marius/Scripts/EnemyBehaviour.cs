using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyBehaviour : MonoBehaviour
{
	public Transform bulletOrigin;
	public Transform player;

	private Coroutine LookCoroutine;

	public ParticleSystem gunShot;

	private float nextFire;
	public float fireRate = 2;
	public float rotationSpeed = 1;

	public LayerMask layers;
	
	RaycastHit hit;

	InGameUI IGUI;

	void Start()
	{
		nextFire = Time.time;

		IGUI = FindObjectOfType<InGameUI>();
	}

	// Update is called once per frame
	void Update()
	{
		CheckIfTimeToFire();
	}

    private void FixedUpdate()
    {
		StartCoroutine(RotateTowardsPlayer());
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
		if (Time.time > nextFire)
		{
			if (Physics.Raycast(bulletOrigin.transform.position, bulletOrigin.transform.forward, out hit, 300f, layers))
			{
				IGUI.RemoveHealthPlayer();
			}

			gunShot.Play();

			nextFire = Time.time + fireRate;
		}
	}
}
