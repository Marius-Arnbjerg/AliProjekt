using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyBehaviour : MonoBehaviour
{
	public Transform bulletOrigin;
	public Transform player;

	public ParticleSystem gunShot;

	private float nextFire;
	public float fireRate = 2;

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

		transform.LookAt(player);
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
