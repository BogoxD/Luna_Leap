using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float jumpSpeed = 10;
    public float moveSpeed = 2;
    public float maxSpeed = 5f;

    [Header("Rotation")]
    public float playerMaxRotation = 20f;


    private Rigidbody2D rb2d;
    public bool isGrounded;

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
    }
    private void OnMove()
    {
        float horizontal = Input.GetAxis("Horizontal");

        rb2d.AddForce(horizontal * moveSpeed * transform.right, ForceMode2D.Force);

        //sets a max speed so the player doesn't accelerate to infinity
        LimitVelocity();
    }
    private void OnJump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded == true)
        {
            rb2d.AddForce(transform.up * jumpSpeed, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }
    private void ClampRotation()
    {
        Vector3 playerEulerAngles = transform.localEulerAngles;

        playerEulerAngles.z = (playerEulerAngles.z > 180) ? playerEulerAngles.z - 360 : playerEulerAngles.z;
        playerEulerAngles.z = Mathf.Clamp(playerEulerAngles.z, -playerMaxRotation, playerMaxRotation);

        transform.localRotation = Quaternion.Euler(playerEulerAngles);
    }
    private void LimitVelocity()
    {
        if (rb2d.velocity.magnitude > maxSpeed)
            rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity, maxSpeed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Planet"))
            isGrounded = true;
    }
}
