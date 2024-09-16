using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpSpeed = 10;
    public float moveSpeed = 2;
    public Rigidbody2D rb;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded == true)
        {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode2D.Impulse);
            isGrounded = false;
        }


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(Vector3.left * moveSpeed, ForceMode2D.Force);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(Vector3.right * moveSpeed, ForceMode2D.Force);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Planet"))
            isGrounded = true;
            Debug.Log("grounded");

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Pickup"))
        {
            Destroy(other.gameObject);
        }
        
    }


}
