using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "Createitem")]
public class CreateItem : ScriptableObject
{
    public enum KindofItem
    {
        Weapon, Useitem
    }

    [System.Serializable]
    public class Itemlevel
    {
        public int level;
        public int damage;
        public int SP;
        public GameObject effects;
        public Sprite icons;
        public string informations;
    }

    [SerializeField]
    private KindofItem kindofItem;

    [SerializeField]
    private Sprite icon; 

    [SerializeField]
    private string Itemname; //共通

    [SerializeField]
    private string information;

    [SerializeField]
    GameObject groundeffect;

    [SerializeField]
    GameObject aireffect;

    [SerializeField]
    int itemlevel=1;

    [SerializeField]
    string[] statenames;

    public Itemlevel[] items;

    public GameObject soundsource;

    public int nowlevel=1;

    public KindofItem GetKindofItem()
    {
        return kindofItem;
    }

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

    public GameObject getgroundeffect()
    {
        return groundeffect;
    }
    public GameObject getAireffect()
    {
        return aireffect;
    }

    public int Getitemlevel()
    {
        return itemlevel;
    }

    public void Setitemlevel(int level)
    {
        itemlevel = level;
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

