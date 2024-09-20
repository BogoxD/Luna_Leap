using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Header("Asteroid Values")]
    public float force;
    public float ImpactForce;

    public float destroyTime = 2f;

    [Header("Animations")]
    [SerializeField] AnimationClip asteroidDeath;

    private Animator animator;
    private Rigidbody2D rb2d;
    private Vector2 forceDirection;

    private bool isFalling = true;

    private void Start()
    {
        //get components
        rb2d = GetComponent<Rigidbody2D>();
        //if asteroid has animator get component
        if (TryGetComponent(out Animator anim))
            animator = anim;

        Destroy(gameObject, destroyTime);
    }
    void FixedUpdate()
    {
        if (isFalling)
            Move();
        //else 
        //bounce around
    }
    private void Move()
    {
        //add random direction downwards for now
        forceDirection = -Vector2.one * Random.Range(0.1f, 1f);

        //apply direction and force to rigidbody
        rb2d.AddForce((forceDirection) * force, ForceMode2D.Force);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //when asteroid collides with planet object
        if (collision.gameObject.CompareTag("Planet") || collision.gameObject.CompareTag("Player"))
        {
            //try to get the rigidbody of the planet
            if (collision.gameObject.TryGetComponent(out Rigidbody2D rigidbody))
            {
                //add force to planet when hitting it
                rigidbody.AddForce(forceDirection * ImpactForce, ForceMode2D.Impulse);
            }

            isFalling = false;

            if (animator)
                animator.Play("Asteroid_Death");

            //set parameters
            rb2d.isKinematic = true;
            rb2d.velocity = Vector2.zero;
            rb2d.angularVelocity = 0f;

            //set collder to trigger
            if (TryGetComponent(out Collider collider))
                collider.isTrigger = true;

            //destroy game object
            Destroy(gameObject, asteroidDeath.length);
        }
    }
}
