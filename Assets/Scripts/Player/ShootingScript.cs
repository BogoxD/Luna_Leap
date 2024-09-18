using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab for the bullet
    public Transform bulletSpawnPoint; // Position from where the bullet will be instantiated
    public float bulletSpeed = 20f; // Speed of the bullet
    public Animator animator; // Reference to the Animator

    private bool canShoot = true; 
    private void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.S) && canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        

       
        animator.SetTrigger("Shoot");

        yield return new WaitForSeconds(1f);

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = bulletSpawnPoint.right * bulletSpeed;

      
        Destroy(bullet, 5f);

       
        yield return new WaitForSeconds(2f);

       
        animator.SetTrigger("Shoot");
    }

    
}
