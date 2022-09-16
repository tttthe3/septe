using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Fungus;

[Serializable]
[CreateAssetMenu(fileName = "eventlist", menuName = "Create_eventlist")]
public class FlagList : ScriptableObject
{
    [SerializeField]
    List<event_item> flagss ;
    public List<event_item> Flags { get { return flagss; } }
    public void InitFlags()
    {
        foreach (event_item f in flagss)
        {
            f.initflag();
        }
    }

    public void SetFlag(event_item flag, bool value = true)
    {
        foreach (event_item f in flagss)
        {
            if (f == flag)
            {
                f.setflag(value);
                return;
            }
        }
    }

    public bool GetFlagStatus(event_item flag)
    {
        foreach (event_item f in flagss)
        {
            if (f == flag)
            {
                return f.Ison;
            }
        }
        return false;
    }



}
