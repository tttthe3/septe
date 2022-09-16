using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using TMPro;

public class Gamerestart_config : MonoBehaviour
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
        if (Input.GetKeyDown(KeyCode.D))
        {
            OnClickRight();

        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            OnClickLeft();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            OnClickChoice();
        }

    }

    public void OnClickRight()
    {


        if (0 <= nowcolum && nowcolum < select.Length - 1)
        {

            select[nowcolum].DOColor(Color.white, 0f);
            ++nowcolum;
           
            select[nowcolum].DOColor(Color.black, 0.1f);

        }

    }

    public void OnClickLeft()
    {

        if (0 < nowcolum && nowcolum <= select.Length)
        {

            select[nowcolum].DOColor(Color.white, 0.1f);
            nowcolum--;
         
            select[nowcolum].DOColor(Color.black, 0.1f);

        }
    }

    public void savejson()
    {
        PersistentDataManager.SaveAllData();
        string json = JsonUtility.ToJson(PersistentDataManager.Instance.m_Storejsonreturn(), true);
        Debug.Log(json);

    }

    public void OnClickChoice()
    {

        if (nowcolum == 0)
        {
            savejson();
        }
    }
}