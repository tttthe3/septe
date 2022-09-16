using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Events;

public class loadgames : MonoBehaviour
{
    private int cout = 0;
    private enum state { select, other }
    state states = state.select;
    private RectTransform[] selecter; 
    public TextMeshProUGUI[] select;//セーブデータ
    int nowcolum = 0;
    private DOTweenTMPAnimator tmproanime;

    public CanvasGroup data1;
    public CanvasGroup data2;
    public CanvasGroup data3;
    public CanvasGroup choice;
    public savainstance data;
    public UnityEvent loadact;
    private string dataname;
    // Start is called before the first frame update
    void Start()
    {
        GameObject find = GameObject.Find("savainstance");
        data = find.GetComponent<savainstance>();
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
            choice.gameObject.SetActive(true);
            states = state.other;
            for (int i = 0; i < select.Length; i++)
                select[i].DOFade(0f, 0.2f);

            switch (nowcolum)
            {
                case 0:
                   data1.gameObject.SetActive(true);
                    dataname = "sava_data1.json";
                    fadeout(nowcolum);
                    break;
                case 1:
                    data2.gameObject.SetActive(true);
                    dataname = "sava_data2.json";
                    break;
                case 2:
                   data3.gameObject.SetActive(true);
                    dataname = "sava_data3.json";
                    break;

            }


        }
        else
        {
            if (Playerinput.Instance.Skill.Down)
            {

                savainstance.Instance.loadsave(dataname);
                Time.timeScale = 1.0f;
                SceneController.TransitionToSceneGame(data);
                //savainstance.Instance.loadcamera();
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

    void setdatacheck()
    {

    }

}
