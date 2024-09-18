using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float jumpForce = 10;
    public float moveForce = 2;
    public float maxSpeed = 5f;

    [Header("Rotation")]
    public float playerMaxRotation = 20f;


    private Rigidbody2D rb2d;
    public bool isGrounded;
    public bool isDead = false;
    public bool isMoving = false;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        OnMove();
        OnJump();

        //animation stuff below
        animator.SetFloat("yVelocity", rb2d.velocity.y); // Vertical velocity for jumping/falling
        animator.SetFloat("Magnitude", Mathf.Abs(rb2d.velocity.x)); // Horizontal movement speed for running/walking
        animator.SetBool("isGrounded", isGrounded); // Grounded check for jump/land animations
        animator.SetBool("isDead", isDead); // Trigger death animation if necessary
    }

    private void OnMove()
    {
        float horizontal = Input.GetAxis("Horizontal");

        rb2d.AddForce(horizontal * moveForce * Vector2.right, ForceMode2D.Force);

        //sets a max speed so the player doesn't accelerate to infinity
        LimitVelocity();

        //clamp rotation
        //ClampRotation();

        if (Mathf.Abs(horizontal) > 0 && isGrounded)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        
    }
    private void OnJump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded == true)
        {
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;

            animator.SetTrigger("Jump");
        }
    }
    private void ClampRotation()
    {
        Vector3 playerEulerAngles = transform.localEulerAngles;

        playerEulerAngles.z = Mathf.Clamp(playerEulerAngles.z, -playerMaxRotation, playerMaxRotation);

        transform.rotation = Quaternion.Euler(0, 0, playerEulerAngles.z);
    }
    private void LimitVelocity()
    {
        rb2d.velocity = new Vector2(Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed), rb2d.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Planet"))
            isGrounded = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pickup"))
        {
            Destroy(other.gameObject);
        }
    }
}