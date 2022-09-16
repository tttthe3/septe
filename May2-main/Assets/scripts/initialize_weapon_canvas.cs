using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using DG.DemiLib;

public class initialize_weapon_canvas : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI tmpro;

    [SerializeField]
    private Text Maintext;

    [SerializeField]
    private Text Subtext1;

    

    [SerializeField]
    private Text Subtext2;


    void Start()
    {
        tmpro = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {

       tmpro.DOFade(0,0);
        DOTweenTMPAnimator animtor = new DOTweenTMPAnimator(tmpro);
        for (int i = 0; i < animtor.textInfo.characterCount; i++)
        {
            animtor.DOFadeChar(i, 1, 2f).SetDelay(i * 0.1f);
        }
    }

    private void OnDisable()
    {
        

    }





}



