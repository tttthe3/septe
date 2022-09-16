using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversatiotrigger : MonoBehaviour
{
    

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player"
            && col.GetComponent<Charactercontrolelr>().GetState() != Charactercontrolelr.talkstate.talk
            )
        {
            Debug.Log("talkstart");
            //　ユニティちゃんが近づいたら会話相手として自分のゲームオブジェクトを渡す
            col.GetComponent<talkmanager>().Setconversation(transform.gameObject);
        }
        

        
        }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player"
            && col.GetComponent<Charactercontrolelr>().GetState() != Charactercontrolelr.talkstate.talk
            )
        {
            //　ユニティちゃんが遠ざかったら会話相手から外す
            col.GetComponent<talkmanager>().resetconversationcounter(transform.gameObject);
        }
    }
}