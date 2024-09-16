using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpSpeed = 10;
    public float moveSpeed = 2;
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
        ClampRotation();
    }
    private void OnMove()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb2d.AddForce(-transform.right * moveSpeed, ForceMode2D.Force);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb2d.AddForce(transform.right * moveSpeed, ForceMode2D.Force);
        }
    }
    private void OnJump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded == true)
        {
            rb2d.AddForce(transform.up * jumpSpeed, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Planet"))
            isGrounded = true;
    }
    private void ClampRotation()
    {
        float zClamped = Mathf.Clamp(transform.eulerAngles.z, -20f, 20f);
        transform.eulerAngles = new Vector3(0, 0, zClamped);
    }
}
