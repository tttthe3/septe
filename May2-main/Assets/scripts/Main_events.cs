using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_events : Eventmanager
{

    bool hitplayer = false;

    public FlagList items;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            hitplayer = true;
    }
    //callでイベントリストがリソースから生成される。各フラグを待ち状態にし、if(flagtype)でそれぞれのフラグ関数に代入する
    int currentflag = 0;
    bool current=false;
    public void flagselector()
    {
        if (items.Flags[currentflag].progress == event_item.eventstate.colid)
        {
            colid_flag(items.Flags[currentflag]); //プレイヤーにヒットしたらフラグをオン
            if (items.Flags[currentflag].flags == true)
            {
                Event_Maker.Instance.InitObject(items.Flags[currentflag].objects[0]);
                currentflag++;
            }
        }
    }

    private void Update()
    {
        if (items != null)
        {
            flagselector();
        }
    }

    public  void colid_flag(event_item item)
    {
        if (hitplayer)
            item.flags = true;

    }
}