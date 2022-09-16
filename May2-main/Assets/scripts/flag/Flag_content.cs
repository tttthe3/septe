using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
[CreateAssetMenu(fileName = "Flag", menuName = "flag")]
public class Flag_content : ScriptableObject
{
    public bool flag;
    public enum eventstate
    {
        colid, defeat, talk,breaks
    }
   
    [System.Serializable]
    public class colidtype
    {
        public string colidname;
        public string colidtag;
        public GameObject target;
    }

    [System.Serializable]
    public class talktype
    {
        public string counter;
        public string countertype;

    }
    [System.Serializable]
    public class Breaktype
    {
        
        public GameObject target;

    }
    // Start is called before the first frame update

    public talktype Talktype;
    public colidtype Colidtype;
    public eventstate state;
    public Breaktype brreaktype;
    public Flag_content_List[] referenceflag;
    public int[] reference_listnumber;
    public string selfname;


}
