using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float force;
    private Rigidbody2D rb2d;
    private Vector2 forceDirection;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        forceDirection = -Vector2.one * Random.Range(0.1f, 1f);
        rb2d.AddForce((forceDirection) * force, ForceMode2D.Force);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Planet"))
        {
            if(collision.gameObject.TryGetComponent(out Rigidbody2D rigidbody))
            {
                //add force to planet when hitting it
                rigidbody.AddForce(forceDirection * 2f, ForceMode2D.Impulse);
            }
        }
    }
}
