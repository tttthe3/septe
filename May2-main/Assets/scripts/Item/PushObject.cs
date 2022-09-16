using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObject : MonoBehaviour
{

    public BoxCollider2D col;
    public Rigidbody2D rb2d;
    public Transform targetpos;
   public void pushs()
    {
       
;        if (Charactercontrolelr.CCInstance.GetAniState("push")) 
            
        {
            rb2d.bodyType = RigidbodyType2D.Dynamic;
        }
        else
        {
            rb2d.bodyType = RigidbodyType2D.Kinematic;
            Vector3 curent = rb2d.velocity;
            rb2d.velocity = new Vector3(0f,curent.y,0f);
        }
    }

    void Update()
    {
        pushs();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "pushtarget")
        {
            rb2d.bodyType = RigidbodyType2D.Dynamic;
            rb2d.mass = 23f;
            Vector3 curent = rb2d.velocity;
            rb2d.velocity = new Vector3(0f, curent.y, 0f);
        }
    }
    
}
