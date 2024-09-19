using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed;
    public float attackSpeed;
    public int health = 1;
    //   private Rigidbody2D enemyRb;
    private GameObject player;
    public int attackCooldown;
    // public int retreatTime;

    private bool readyToAttack;
    private bool hasPreparedAttack = true;
    private Animator animator;             // Reference to Animator
    private AudioSource audioSource;       // Reference to AudioSource

    private Rigidbody2D enemyRb;


    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
        readyToAttack = true;
        enemyRb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {


        if (readyToAttack == true)
        {
            hasPreparedAttack = true;
            Vector2 lookDirection = (player.transform.position - transform.position).normalized;

            //         transform.Translate(lookDirection * attackSpeed * Time.deltaTime, Space.World);
            enemyRb.AddForce(lookDirection * attackSpeed);

        }

        else if (readyToAttack == false)
        {
            if (hasPreparedAttack)
            {
                StartCoroutine("PrepareAttack");
            }
            Vector2 lookDirection = (transform.position - player.transform.position).normalized;

            transform.Translate(lookDirection * moveSpeed * Time.deltaTime);



        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Gotcha");

            readyToAttack = false;
        }

    }

    IEnumerator PrepareAttack()
    {
        Debug.Log("Need to recharge");
        hasPreparedAttack = false;
        yield return new WaitForSeconds(attackCooldown);

        readyToAttack = true;

        Debug.Log("Im coming for you");


    }

    //   private void OnTriggerEnter2D(Collider2D other)
    //   {
    //       if (other.gameObject.CompareTag("Player"))
    //           {
    //          StartCoroutine("PrepareAttack");
    //          }
    //   }

    //   private void Retreat()
    //   {

    //       {
    //           Vector2 lookDirection = (transform.position - player.transform.position).normalized;

    //           transform.Translate(lookDirection * moveSpeed * Time.deltaTime);
    //       }

    //   }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet")) // Make sure the bullet has the tag "Bullet"
        {

            TakeDamage(1);  // Assume each bullet does 1 damage

        }
    }

    // Handle taking damage
    void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    // Handle the death of the enemy
    void Die()
    {
        // Trigger the death animation
        animator.SetTrigger("Die");

        // Play the death sound if available
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }

        // Disable collider and movement logic, but allow animation to play
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

        // Start a coroutine to wait until the animation finishes before destroying the object
        StartCoroutine(WaitForDeathAnimation());
    }

    IEnumerator WaitForDeathAnimation()
    {
        // Get the length of the current animation state or wait a specific amount of time
        AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float animationDuration = animatorStateInfo.length;

        // Wait for the death animation to complete
        yield return new WaitForSeconds(animationDuration);

        // Destroy the enemy object after the animation has played
        if (audioSource != null && audioSource.clip != null)
        {
            Destroy(gameObject, audioSource.clip.length); // Wait for audio to finish if available
        }
        else
        {
            Destroy(gameObject); // Destroy immediately if no audio is playing
        }
    }
}
