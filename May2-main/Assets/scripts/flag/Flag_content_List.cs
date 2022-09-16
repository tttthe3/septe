using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
[CreateAssetMenu(fileName = "Flaglist", menuName = "make_flaglist")]
public class Flag_content_List : ScriptableObject
{
    public Flag_content[] flags;
}
