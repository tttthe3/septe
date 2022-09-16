using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag_judge : MonoBehaviour
{
    static  bool hitplayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            hitplayer = true;
    }

    public static void colid_flag(event_item item)
    {
        if(hitplayer)
                item.flags = true;     

    }

    

    public  void defeat_flag()
    {

    }

    public void talk_flag()
    {

    }

}
