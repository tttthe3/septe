using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using TMPro;



public class Opningcursol : MonoBehaviour
{
    private enum state { select, other }
    state states = state.select;
    private RectTransform[] selecter;
    public TextMeshProUGUI[] select;
    int nowcolum = 0;
    private DOTweenTMPAnimator tmproanime;

    public CanvasGroup start;
    public CanvasGroup load;
    public CanvasGroup config;
    public CanvasGroup Quit;
    private int cout = 0;

    // Start is called before the first frame update
    void Start()
    {
        //  tmproanime = new DOTweenTMPAnimator(select[nowcolum]);
        // //for (int i = 0; i < tmproanime.textInfo.characterCount; i++)
        //{
        //     tmproanime.DOColorChar(i, Color.red, 2f).SetDelay(i * 0.1f);
        // }
        select[nowcolum].DOColor(Color.black, 0.1f);



    }
    // Update is called once per frame
    void Update()
    {
        if (Playerinput.Instance.Select_Hoz.Value == 1 && cout == 0)
        {
            cout = 1;
            OnClickRight();

        }
        else if (Playerinput.Instance.Select_Hoz.Value < 0 && cout == 0)
        {
            cout = 1;
            OnClickLeft();
        }
        else if (Playerinput.Instance.Skill.Down)
        {
            OnClickChoice();
        }
        if (Playerinput.Instance.Select_Hoz.Value == 0)
            cout = 0;

    }

    public void OnClickRight()
    {

        if (states == state.select)
        {

            if (0 <= nowcolum && nowcolum < select.Length - 1)
            {

                //select[nowcolum].DOPause();
                select[nowcolum].DOColor(Color.white, 0f);
                ++nowcolum;
                // tmproanime = new DOTweenTMPAnimator(select[nowcolum]);
                //for (int i = 0; i < tmproanime.textInfo.characterCount; i++)
                //{
                //    tmproanime.DOColorChar(i, Color.red, 2f).SetDelay(i * 0.1f);

                // }
                select[nowcolum].DOColor(Color.black, 0.1f);

            }

        }
        cout = 1;
    }

    public void OnClickLeft()
    {
        if (states == state.select)
        {

            if (0 < nowcolum && nowcolum <= select.Length)
            {



                select[nowcolum].DOColor(Color.white, 0.1f);
                nowcolum--;
                //tmproanime = new DOTweenTMPAnimator(select[nowcolum]);
                //for (int i = 0; i < tmproanime.textInfo.characterCount; i++)
                //{
                //    tmproanime.DOColorChar(i, Color.red, 2f).SetDelay(i * 0.1f);
                // }
                select[nowcolum].DOColor(Color.black, 0.1f);

            }
        }
        cout = 1;
    }

    public void OnClickChoice()
    {

        if (states == state.select)
        {
            
            states = state.other;
            for (int i = 0; i < select.Length; i++)
                select[i].DOFade(0f, 0.2f);

            switch (nowcolum)
            {
                case 0:
                    start.gameObject.SetActive(true);
                    fadeout(nowcolum);
                    break;
                case 1:
                    load.gameObject.SetActive(true);
                    break;
                case 2:
                    config.gameObject.SetActive(true);
                    break;
                case 3:
                    Quit.gameObject.SetActive(true);
                    break;

            }


        }
    }

    void fadeout(int rest)
    {

        for (int i = 0; i < select.Length; i++)
        {
            //iconinform[i].gameObject.SetActive(false);
            if (i != rest)
            {
                select[i].DOFade(0f, 1f);
               

            }
            else
            {
                select[i].gameObject.SetActive(false);
                

            }

        }

    }

}