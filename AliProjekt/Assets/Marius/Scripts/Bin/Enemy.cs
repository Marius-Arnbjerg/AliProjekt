using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public Transform bulletOrigin;
	public ParticleSystem gunShot;
	public Transform player;
	float nextFire;
	public float fireRate = 2;

	public LayerMask layers;
	RaycastHit hit;

	InGameUI IGUI;

	// Use this for initialization
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
				Debug.Log("hit");
			}

			gunShot.Play();

			nextFire = Time.time + fireRate;
		}

	}
}
