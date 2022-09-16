using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Fungus;

[Serializable]
[CreateAssetMenu(fileName = "trigger", menuName = "triggeritem")]
public class talkconteny : ScriptableObject
{
 
    public enum talktype
    {
        loop, once
    }

    public enum talktype_start { 
     colid,botton,events
    }

    public enum choices
    {
        non,choces
    }

    [SerializeField]
    private choices[] choice;  

    [SerializeField]
    private talktype_start talktype_Start;


    [SerializeField]
    private Transform playerpos;

    [SerializeField]
    private Transform counterpos;

    [SerializeField]
    private string[] talkchara;

    [SerializeField]
    private bool[] trigger;


    [SerializeField]
    private talktype talktypes;

    [SerializeField]
    [Multiline(100)]
    private string message;


    public Fungus.Flowchart flowchart = null;

    public Transform Getplayerpos()
    {
        return playerpos;
    }

    public Transform Getcounterpos()
    {
        return counterpos;
    }

    public string[] gettalkchara()
    {
        return talkchara;
    }
    public bool[] gettrigger()
    {
        return trigger;
    }

    public string GetConversationMessage()
    {
        return message;
    }

   

    public talktype_start Gettalktype_start()
    {
        return talktype_Start;
    }

    public Flowchart Getflowchart()
    {
        return flowchart;
    }
}
