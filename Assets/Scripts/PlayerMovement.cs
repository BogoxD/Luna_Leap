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


        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rb.AddForce(-transform.right * moveSpeed, ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rb.AddForce(transform.right * moveSpeed, ForceMode2D.Impulse);
        }

        ClampRotation();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Planet"))
            isGrounded = true;
            Debug.Log("grounded");

    }
    private void ClampRotation()
    {
        float zClamped = Mathf.Clamp(transform.eulerAngles.z, -20f, 20f);
        transform.eulerAngles = new Vector3(0, 0, zClamped);
    }
}
