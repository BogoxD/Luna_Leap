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
    public float rotationMultiplier = 2f;


    private Rigidbody2D rb2d;
    public bool isGrounded;
    public bool isJetpacking;

    //Sound Effects
    [SerializeField] AudioClip pickUpSoundClip;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        OnJump();
    }
    private void FixedUpdate()
    {
        OnMove();

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

        //rotate player towards horizontal input
        transform.eulerAngles -= Vector3.forward * horizontal * rotationMultiplier;

        //sets a max speed so the player doesn't accelerate to infinity
        LimitVelocity();

        ClampRotation();
    }
    private void OnJump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded == true)
        {
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }
    private void JetPacking()
    {
        float vertical = Input.GetAxis("Vertical");

        rb2d.AddForce(vertical * moveForce * Vector2.up, ForceMode2D.Force);
    }
    private void ClampRotation()
    {
        Vector3 playerEulerAngles = transform.localEulerAngles;

        playerEulerAngles.z = (playerEulerAngles.z > 180) ? playerEulerAngles.z - 360 : playerEulerAngles.z;
        playerEulerAngles.z = Mathf.Clamp(playerEulerAngles.z, -playerMaxRotation, playerMaxRotation);

        transform.rotation = Quaternion.Euler(playerEulerAngles);
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
            SoundFXManager.Instance.PlaySoundFXClip(pickUpSoundClip, transform, 1f);
            Destroy(other.gameObject);
        }
    }
}
