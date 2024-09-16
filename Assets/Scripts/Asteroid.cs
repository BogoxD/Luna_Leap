using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float force;
    public float ImpactForce;
    private Rigidbody2D rb2d;
    private Vector2 forceDirection;

    private bool isFalling = true;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
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
        if (collision.gameObject.CompareTag("Planet"))
        {
            //try to get the rigidbody of the planet
            if (collision.gameObject.TryGetComponent(out Rigidbody2D rigidbody))
            {
                //add force to planet when hitting it
                rigidbody.AddForce(forceDirection * ImpactForce, ForceMode2D.Impulse);

                //make asteroid fall downwards and bounce around
                isFalling = false;
                rb2d.gravityScale = 1f;

                //one in three chance for object to get disabled
                int chance = Random.Range(0, 3);
                if (chance == 1)
                    gameObject.SetActive(false);
            }
        }
    }
}
