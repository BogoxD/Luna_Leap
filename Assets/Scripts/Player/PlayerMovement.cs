using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float jumpForce = 10;
    public float moveForce = 2;
    public float maxSpeed = 5f;

    //Gravity 
    [SerializeField] private float baseGravity = 5f;
    [SerializeField] private float maxFallSpeed = 10f;
    [SerializeField] private float fallSpeedMultiplier = 5f;

    private Rigidbody2D rb2d;
    public bool isGrounded;

    public bool isMoving = false;
    public bool isJetpacking;
    private Transform respawnPoint;

    private bool isFacingRight = true;
    public Animator animator;

    //Sound Effects
    [SerializeField] AudioClip pickUpSoundClip;
    [SerializeField] AudioClip jumpClip;
    [SerializeField] AudioClip startClip;
    [SerializeField] AudioClip restartClip;
    [SerializeField] AudioClip fallingClip;
    private bool hasPlayedFallSound = false;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        // Automatically find the GameObject with tag "Respawn" in the scene
        FindRespawnPoint();

        SoundFXManager.Instance.PlaySoundFXClip(startClip, transform, 1f);
    }

    private void Update()
    {
        Gravity();
        OnJump();
    }
    private void FixedUpdate()
    {
        CheckFall();
        OnMove();
        

        //animation stuff below
        animator.SetFloat("yVelocity", rb2d.velocity.y); // Vertical velocity for jumping/falling
        animator.SetFloat("Magnitude", Mathf.Abs(rb2d.velocity.x)); // Horizontal movement speed for running/walking
        animator.SetBool("isGrounded", isGrounded); // Grounded check for jump/land animations

        if (isJetpacking)
        {
            rb2d.gravityScale = 0;
            JetPacking();
        }
        else
            rb2d.gravityScale = 1;
    }

    private void OnMove()
    {
        float horizontal = Input.GetAxis("Horizontal");

        rb2d.AddForce(horizontal * moveForce * Vector2.right, ForceMode2D.Force);

        // Flip the sprite based on direction
        FlipSprite(horizontal);

        //sets a max speed so the player doesn't accelerate to infinity
        LimitVelocity();
    }

    private void FlipSprite(float horizontal) {
        if (horizontal > 0 && !isFacingRight) {
            Flip(); 
        } else if (horizontal < 0 && isFacingRight) {
            Flip(); 
        }
    }

    private void Flip() {
        // Switch the way the player is labeled as facing
        isFacingRight = !isFacingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }

    private void OnJump()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && isGrounded == true) {
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;

            animator.SetTrigger("Jump");

            SoundFXManager.Instance.PlaySoundFXClip(jumpClip, transform, 1f);
        }
    }

    private void JetPacking()
    {
        float vertical = Input.GetAxis("Vertical");

        rb2d.AddForce(vertical * moveForce * Vector2.up, ForceMode2D.Force);
    }

    private void LimitVelocity()
    {
        rb2d.velocity = new Vector2(Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed), rb2d.velocity.y);
    }

    private void Gravity() {
        // Check if the player is falling (velocity.y < 0)
        if (rb2d.velocity.y < 0) {
            rb2d.gravityScale = baseGravity * fallSpeedMultiplier; //fall faster longer you fall

            // Check if the player is falling at or below the maximum fall speed
            if (rb2d.velocity.y <= -maxFallSpeed) {
                rb2d.velocity = new Vector2(rb2d.velocity.x, -maxFallSpeed); //cap at max fall speed

                // Only play the sound effect if it hasn't been played yet
                if (!hasPlayedFallSound) {
                    SoundFXManager.Instance.PlaySoundFXClip(fallingClip, transform, 1f);
                    hasPlayedFallSound = true; // Prevent sound from repeating
                }
            }
        } else {
            // Reset the flag when the player stops falling or starts moving upwards
            hasPlayedFallSound = false;
        }
    }

    private void CheckFall() {
        // Check if the player's Y pos is below -20
        if (transform.position.y < -20) {
            Respawn();
        }
    }

    private void Respawn() {
        // Set the player's position to the respawn point
        if (respawnPoint != null) {
            transform.position = respawnPoint.position;
            rb2d.velocity = Vector2.zero; // Reset velocity

            SoundFXManager.Instance.PlaySoundFXClip(restartClip, transform, 1f);
        }
    }

    private void FindRespawnPoint() {
        // Automatically find the respawn point
        GameObject respawnObject = GameObject.FindGameObjectWithTag("Respawn");
        if (respawnObject != null) {
            respawnPoint = respawnObject.transform;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Planet"))
        {
            isGrounded = true;
        }
        else
            rb2d.drag = 0f;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Pickup")) {
            SoundFXManager.Instance.PlaySoundFXClip(pickUpSoundClip, transform, 1f);
            Destroy(other.gameObject);
        }
    }
}