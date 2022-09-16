using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Weapon_Skill_Icon : MonoBehaviour
{

    public enum ItemHave { Have,None}
    public ItemHave itemhave=ItemHave.None;
    public Image currenticon;

    public string skillname;
    // Start is called before the first frame update
    private void OnEnable()
    {
        if (SkillManager.Instance.GetSkill(skillname) != null)
        {
            itemhave = ItemHave.Have;
            currenticon = SkillManager.Instance.GetSkill(skillname).GetIcon();
        }
        else
        {
            itemhave = ItemHave.None;
        }
    }

    public ItemHave GetIconstate()
    {
        return itemhave;
    }
}