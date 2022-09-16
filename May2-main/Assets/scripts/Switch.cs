using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public Sprite before;
    public Sprite after;
    public DirectorTtrigger events;
    public int count=0;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "push")
        {
            if (count == 0)
            {
               
            }
            
        }
    }
}
