using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	//[SerializeField]
	//GameObject bullet;

	
	//public float bulletSpeed = 10;


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
		//gunShot.Stop();

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
			//Instantiate(bullet, bulletOrigin.transform.position, bullet.transform.rotation);

			/*GameObject tempBullet = Instantiate(bullet, bulletOrigin.transform.position, bulletOrigin.transform.rotation);
			gunShot.Play();
			Rigidbody tempBulletRig = tempBullet.GetComponent<Rigidbody>();

			tempBulletRig.AddForce(tempBulletRig.transform.forward * bulletSpeed);*/

			if (Physics.Raycast(bulletOrigin.transform.position, bulletOrigin.transform.forward, out hit, 300f, layers))
			{
				IGUI.RemoveHealthPlayer();
				Debug.Log("hit");
			}

			gunShot.Play();

			nextFire = Time.time + fireRate;

			//Destroy(tempBulletRig, 3f);
		}

	}
}
