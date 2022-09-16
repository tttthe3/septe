using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
[CreateAssetMenu(fileName = "Item2", menuName = "Createitem2")]
public class CreateItem2 : ScriptableObject
{

    [System.Serializable]
    public class Itemlevel
    {
        public int level;
        public int damage;
        public Sprite icons;
        public string informations;
    }


    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private string Itemname; //共通

    [SerializeField]
    private string information;


    [SerializeField]
    string[] statenames;

    public Itemlevel[] items;

    public GameObject soundsource;

    public int nowlevel = 1;


    public Sprite GetSprite()
    {
        return icon;
    }

    public string getitemname()
    {
        return Itemname;
    }
    public string getinfomation()
    {
        return information;
    }

 

    public Itemlevel[] Getitemlevels()
    {
        return items;
    }

    public string[] Getstatenames()
    {
        return statenames;
    }



}

