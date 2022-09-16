using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
public class ReadSkillFlag : MonoBehaviour
{
    public Image iamge;
    public int skillnumber;
    
    // Start is called before the first frame update
   

    public void OnEnable()
    {
        Color defaults = iamge.color;
        iamge.color = new Color (0,0,0,255);
        if (SkillManager.Instance.GetSLash_SkillList(skillnumber).Getflag())
            iamge.color = new Color(0, 0, 0, 255);
        else
            iamge.color = defaults;

    }
}
