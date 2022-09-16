using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
[Serializable]
[CreateAssetMenu(fileName = "Skill", menuName = "Make_WeaponSkill")]
public class WeaponSKill : ScriptableObject
{
    public enum KindofWeapon
    {
        Slash,Lans,Hunmer
    }



    public enum KindofSkill
    {
       NewAttack,Attackplus,CombPlus
    }

    [SerializeField]
    private KindofWeapon kindofWeapon;

    [SerializeField]
    private KindofSkill kindofSkill;

    [SerializeField]
    private string Skillname;

    [SerializeField]
    private string Information;

    [SerializeField]
    private bool getflag;

    [SerializeField]
    private Image icon;

    [SerializeField]
    private int needpoint;
    public KindofSkill GetKindofSkill()
    {
        return kindofSkill;
    }

    public KindofWeapon GetKindofWeapon()
    {
        return kindofWeapon;
    }

    public string GetSKillname()
    {
        return Skillname;
    }
    public string GetSKillInfo()
    {
        return Information;
    }

    public int Getneedpoint()
    {
        return needpoint;
    }

    public Image GetIcon()
    {
        return icon;
    }

    public bool Getflag()
    {
        return getflag;
    }

    public void Setflag(bool Getflag)
    {
        getflag=Getflag;
    }
}
