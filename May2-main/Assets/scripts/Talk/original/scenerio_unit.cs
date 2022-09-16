using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "Scenario", menuName = "Make_scenario")]
public class scenerio_unit : ScriptableObject
{
    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private string[] Maintext;

    [SerializeField]
    private string[] Talker; //共通


    public string[] GetMainText()
    {
        return Maintext;
    }

    public string[] GetTalker()
    {
        return Talker;
    }

}
