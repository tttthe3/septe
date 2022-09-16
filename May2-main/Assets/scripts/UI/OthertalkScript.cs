using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Fungus;


public class OthertalkScript : MonoBehaviour
{
    [SerializeField]
    Fungus.Flowchart flow;

    [SerializeField]
    private string splitString;

    public enum State
    {
        Wait,
        Walk,
        Talk
    }

    [SerializeField]
    private talkconteny conversation = null;

    //　ユニティちゃんのTransform
    private Transform conversationPartnerTransform;
    //　村人がユニティちゃんの方向に回転するスピード
    [SerializeField]
    private float rotationSpeed = 2f;
    State state = State.Wait;

    private void Start()
    {
        
    }

    public void SetState(State state, Transform conversationPartnerTransform = null)
    {
        this.state = state;
        if (state == State.Wait)
        {
           
        }
        else if (state == State.Walk)
        {
            
        }
        else if (state == State.Talk)
        {
           
            this.conversationPartnerTransform = conversationPartnerTransform;
        }
    }

    //　Conversionスクリプトを返す
    public talkconteny GetConversation()
    {
        return conversation;
    }


    public Flowchart Getflowchart()
    {
        return flow;
    }


    public string getflowname()
    {
        return splitString;
    }
}
