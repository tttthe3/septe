using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class skillicon : MonoBehaviour
{


    public enum ItemHave { Have, None }
    public ItemHave itemhave = ItemHave.None;
    public Image currenticon;
    public GameObject lefticon;
    public GameObject righticon;
    public WeaponSKill skill;
    public string skillname;
    // Start is called before the first frame update
    private void OnEnable()
    {
        currenticon= skill.GetIcon();
        if (SkillManager.Instance.GetSkill(skillname)!=null)
            currenticon.color = Color.black;
    }

    public ItemHave GetIconstate()
    {
        return itemhave;
    }

    public GameObject GetLeft()
    {
        return lefticon;
    }
    public GameObject GetRight()
    {
        return righticon;
    }

    public WeaponSKill Getskill()
    {
        return skill;
    }
}
