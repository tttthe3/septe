using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
[CreateAssetMenu(fileName = "Textset", menuName = "Creat_text")]
public class TextEvent : ScriptableObject
{
    public enum eventstate
    {
        text, init, dest
    }

    public enum timing
    {
        soon,trans,scene
    }
    public bool eventAlready;
    public eventstate progress;
    public string targetname;
    public string Textrefremce;
    public DirectorTtrigger trigger;
    public GameObject[] objects;
    public string Gettargetname()
    {
        return targetname;
    }
    public string GetTextref()
    {
        return Textrefremce;
    }

    public eventstate Geteventtype()
    {
        return progress;
    }

    public Flag_content[] needflags;

    public Flag_content[] Getflags()
    {
        return needflags;
    }

    public bool Flagscheck()
    {
        int cout = 0;
        for (int i = 0; i < needflags.Length; i++)
        {
            if (!needflags[i])
                return false;
            
                
        }
        return true;
    }

    public bool GetEventState()
    {
        return eventAlready;
    }

    public void SetEventState(bool trigger)
    {
       eventAlready=trigger;
    }
}
