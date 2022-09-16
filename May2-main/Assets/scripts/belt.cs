using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class belt : MonoBehaviour
{
    public Vector2 force;
    public Vector2 velocrity;
    public LayerMask layer;
    private Rigidbody2D rb2d;
    public Transform body;
    Transform current;
    private bool on = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (body == null)
            return;
        body = collision.GetComponent<Transform>();
        rb2d = collision.GetComponent<Rigidbody2D>();
        on = true;
        current = body;
        Vector3 force = new Vector3(-4f,0,0);
        rb2d.AddForce(force);
        //Vector3 move = new Vector3(body.position.x+0.1f, body.position.y, body.position.z);
        //body.Translate(move);
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            body = collision.gameObject.GetComponent<Transform>();
            rb2d = collision.gameObject.GetComponent<Rigidbody2D>();
            on = true;
            current = body;

            //rb2d.AddForce(force);
            rb2d.velocity = velocrity;
        }
        else
        {
            rb2d = collision.gameObject.GetComponent<Rigidbody2D>();
            
            rb2d.AddForce(force,ForceMode2D.Force);
        }
        //Vector3 move = new Vector3(body.position.x+0.1f, body.position.y, body.position.z);
        //body.Translate(move);
    }


    // Update is called once per frame
    void Update()
    {
        

    }
}
