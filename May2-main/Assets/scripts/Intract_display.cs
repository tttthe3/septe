using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Intract_display : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI icontext;
    public Collider2D Player;
    private void Start()
    {
        icon.gameObject.SetActive(false);
    }

    

    private void Update()
    {
        if (Talkmanage.Instance.talksend())
        {
            icon.CrossFadeAlpha(1, 0, false);
            icontext.text = "talk";
            return;
        }

        
       

        if (IntractManager.Instance.getIntractname() == null)
        {
            icon.CrossFadeAlpha(0, 0, false);
            icontext.text = "";
           
        }
        else
        {
            icon.CrossFadeAlpha(1, 0, false);
            icontext.text = IntractManager.Instance.getIntractname();
        }



    }
}
