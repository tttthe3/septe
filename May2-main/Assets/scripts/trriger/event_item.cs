using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Fungus;

[Serializable]
[CreateAssetMenu(fileName = "event", menuName = "Create_event")]
public class event_item : ScriptableObject
{
    public enum eventstate
    {
        colid,defeat,talk,
    }

    public eventstate progress;
        public bool flags;
        public DirectorTtrigger trigger;
        public GameObject[] objects;
        public Flowchart flow;
        public bool nextphase;
        public bool afterdelete;
        public string hitobjectname;
       public bool Ison { get { return flags; } }
       
       public void initflag()
    {
        flags = false;
    }

    public void setflag(bool value = true)
    {
        flags = value;
    }
}
