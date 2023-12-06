using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float moveSpeed = 50f;

	Rigidbody rb;

	GameObject target;
	
	Vector3 moveDirection;
	public ParticleSystem hitParticles;

	// Use this for initialization
	void Start()
	{
		/*rb = GetComponent<Rigidbody>();

		target = GameObject.FindWithTag("Player");
		moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;

		Debug.Log("Move Direction: " + moveDirection);

		rb.velocity = new Vector3(moveDirection.x, moveDirection.y, moveDirection.z);

		rb.AddForce(rb.transform.forward * moveSpeed);*/

		Destroy(gameObject, 3f);
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.name.Equals("Player"))
		{
			Debug.Log("Hit!");
			hitParticles.Play();
			Destroy(gameObject);

		}
	}
}
