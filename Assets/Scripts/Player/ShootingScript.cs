
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab for the bullet
    public Transform bulletSpawnPoint; // Position from where the bullet will be instantiated
    public float bulletSpeed = 20f; // Speed of the bullet
    public Animator animator; // Reference to the Animator

    [SerializeField] AudioClip sayHelloClip;
    [SerializeField] AudioClip gunShotClip;


    private bool hasShot = false; // Track if the shoot animation has already been played

    private void Update()
    {
        // Check if the player presses the space key, can shoot, and hasn't shot already
        if (Input.GetKeyDown(KeyCode.Space) && !hasShot)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        //play soundbites
        
        int chanceToSay = Random.Range(1, 5);
        if (chanceToSay < 3 )
        {
            SoundFXManager.Instance.PlaySoundFXClip(sayHelloClip, transform, 1f);
        }

       
        SoundFXManager.Instance.PlaySoundFXClip(gunShotClip, transform, 0.1f);

        // Set the hasShot flag to true to prevent the animation from playing again
        hasShot = true;

        // Trigger the shooting animation by setting isShooting bool to true
        animator.SetBool("isShooting", true);

        // Wait for 1 second to match the animation or delay
        yield return new WaitForSeconds(0.3f);

        // Instantiate the bullet at the bullet spawn point
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // Get the Rigidbody2D component from the bullet
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        // Determine the direction based on the player's facing direction (localScale.x)
        Vector2 shootDirection = transform.localScale.x > 0 ? transform.right : -transform.right;

        // Set the bullet velocity based on the player's facing direction
        bulletRb.velocity = shootDirection * bulletSpeed;

        // Destroy the bullet after 5 seconds to avoid clutter
        Destroy(bullet, 5f);

        // After shooting, set the isShooting bool to false to stop the animation
        animator.SetBool("isShooting", false);

        // Optional: if you want to re-enable shooting after some time or a specific condition
        yield return new WaitForSeconds(1f);

        hasShot = false;
    }
}