using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed;
    public float attackSpeed;
    //   private Rigidbody2D enemyRb;
    private GameObject player;
    public int attackCooldown;
   // public int retreatTime;

    private bool readyToAttack;
    private bool hasPreparedAttack = true;


    void Start()
    {

        player = GameObject.Find("Player");
        readyToAttack = true;

    }


    void Update()
    {


        if (readyToAttack == true)
        {
            hasPreparedAttack = true;
            Vector2 lookDirection = (player.transform.position - transform.position).normalized;

            transform.Translate(lookDirection * moveSpeed * Time.deltaTime, Space.World);


        }

        else if (readyToAttack == false)
        {
            if(hasPreparedAttack)
            {
                StartCoroutine("PrepareAttack");
            }
                Vector2 lookDirection = (transform.position - player.transform.position).normalized;

                transform.Translate(lookDirection * moveSpeed * Time.deltaTime);

               
            
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Gotcha");
            readyToAttack = false;
        }
        
    }

    IEnumerator PrepareAttack()
    {
        Debug.Log("Need to recharge");
        hasPreparedAttack = false;
        yield return new WaitForSeconds(attackCooldown);

        readyToAttack = true;

        Debug.Log("Im coming for you");


    }

    //   private void OnTriggerEnter2D(Collider2D other)
    //   {
    //       if (other.gameObject.CompareTag("Player"))
    //           {
    //          StartCoroutine("PrepareAttack");
    //          }
    //   }

 //   private void Retreat()
 //   {
        
 //       {
 //           Vector2 lookDirection = (transform.position - player.transform.position).normalized;

 //           transform.Translate(lookDirection * moveSpeed * Time.deltaTime);
 //       }

 //   }


}
