using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField]
	GameObject bullet;

	public float fireRate = 2;
	public float bulletSpeed = 10;
	float nextFire;
	public Transform player;
	public Transform bulletOrigin;
	public ParticleSystem gunShot;


	// Use this for initialization
	void Start()
	{
		nextFire = Time.time;
		//gunShot.Stop();
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

			GameObject tempBullet = Instantiate(bullet, bulletOrigin.transform.position, bulletOrigin.transform.rotation);
			gunShot.Play();
			Rigidbody tempBulletRig = tempBullet.GetComponent<Rigidbody>();

			tempBulletRig.AddForce(tempBulletRig.transform.forward * bulletSpeed);

			nextFire = Time.time + fireRate;

			//Destroy(tempBulletRig, 3f);
		}

	}
}
