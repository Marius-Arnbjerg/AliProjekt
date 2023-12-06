using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject player;

    public Transform bulletOrigin;

    public float bulletSpeed = 10f;

    [SerializeField] private float fireInterval = 2;

    public void ShootPlayer()
    {
        GameObject tempBullet = Instantiate(bulletPrefab, bulletOrigin.transform.position, Quaternion.identity);

        Rigidbody bulletRigidbody = tempBullet.GetComponent<Rigidbody>();

        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = (player.transform.position - tempBullet.transform.position).normalized * bulletSpeed;
        }
    }


    private void FixedUpdate()
    {
        ShootPlayerWithInterval();
    }

    private IEnumerator ShootPlayerWithInterval()
    {
            ShootPlayer();
            yield return new WaitForSeconds(fireInterval);
    
    }

}
