using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    static protected SkillManager s_SkillBase;
    static public SkillManager Instance { get { return s_SkillBase; } }

    public WeaponSKill[] Slash;
    public WeaponSKill[] Lans;
    public WeaponSKill[] Hunmer ;

    public int Skillpoint = 11;

    [SerializeField]
    private List<WeaponSKill> haveitemList = new List<WeaponSKill>();

    // Start is called before the first frame update
    void Start()
    {
        s_SkillBase = this;
        Debug.Log(Slash[0]);
        Skillpoint = 11;


    }
    public WeaponSKill GetSkill(string name)
    {
        int hitnumber = 0;

        if (haveitemList.Count == 0)
            return null;

        for(int i = 0; i < haveitemList.Count; i++)
        {
            if (haveitemList[i].GetSKillname() == name)
            {
                hitnumber = i;
                break;
            }
        }
        if (hitnumber == haveitemList.Count - 1)
        return null;

        return haveitemList[hitnumber];
        
    }

    public void Add_SkillList(WeaponSKill skill)
    {
        haveitemList.Add(skill);
    }


    // Update is called once per frame
    public WeaponSKill GetSLash_SkillList(int number)
    {
        return Slash[number];
    }

    public WeaponSKill[] GetSLash_SkillLists()
    {
        return Slash;
    }

    public void SkillpointPlus()
    {
        Skillpoint++;
    }

    public void UseSkillpoint(int neddpoint)
    {
        Skillpoint = Skillpoint - neddpoint;
    }

    public int GetSkillpoint()
    {
        return Skillpoint;
    }
}
