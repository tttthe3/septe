using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using TMPro;

public class Config_panel : MonoBehaviour
{
    [SerializeField]
    RectTransform[] selfrect;
    private enum Iconstate { choice,controller, audio, graphic,save };
    Iconstate iconstate;
    int nowcolum = 0;

    [SerializeField]
    GameObject Equipmentpanel;

    [SerializeField]
    GameObject Otherpanel;

    [SerializeField]
    GameObject Infomationpanel;

    [SerializeField]
    GameObject Configpanel;
    private int cout = 0;
    // Start is called before the first frame update
    void OnEnable()
    {
        selfrect[nowcolum].GetComponent<TextMeshProUGUI>().DOPause();
        selfrect[nowcolum].GetComponent<TextMeshProUGUI>().DOColor(Color.black, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {


        if (Playerinput.Instance.Select_Vert.Value == 1 && cout == 0)
        {
            cout = 1;
            OnClickUp();

        }
        else if (Playerinput.Instance.Select_Vert.Value < 0 && cout == 0)
        {
            cout = 1;
            OnClickDown();
        }
        else if (Playerinput.Instance.Skill.Down)
        {
            OnClickChoice();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Reset();
        }
        if (Playerinput.Instance.Select_Vert.Value == 0)
            cout = 0;

    }

    public void OnClickUp()
    {
        if (iconstate == Iconstate.choice)
        {
            if (0 < nowcolum && nowcolum <= selfrect.Length)
            {

                selfrect[nowcolum].GetComponent<TextMeshProUGUI>().DOPause();
                selfrect[nowcolum].GetComponent<TextMeshProUGUI>().DOColor(Color.white, 0.1f); ;
                nowcolum--;
                Debug.Log(nowcolum);
                selfrect[nowcolum].GetComponent<TextMeshProUGUI>().DOColor(Color.black, 1f).SetLoops(-1, LoopType.Yoyo);
                // selfrect.position = new Vector3(currentpos.x+gridx, currentpos.y,currentpos.z);

            }
        }
        cout = 1;
    }

    public void OnClickDown()
    {
        if (iconstate == Iconstate.choice)
        {
            if (0 <= nowcolum && nowcolum < selfrect.Length - 1)
            {
                selfrect[nowcolum].GetComponent<TextMeshProUGUI>().DOPause();
                selfrect[nowcolum].GetComponent<TextMeshProUGUI>().DOColor(Color.white, 0.1f); ;
                nowcolum++;
                Debug.Log(nowcolum);
                selfrect[nowcolum].GetComponent<TextMeshProUGUI>().DOColor(Color.black, 1f).SetLoops(-1, LoopType.Yoyo);
            }
        }
        cout = 1;

    }

    public void OnClickChoice()
    {

        if (iconstate == Iconstate.choice)
        {

            switch (nowcolum)
            {
                case 0:
                    Equipmentpanel.SetActive(true);
                    fadeout(nowcolum);
                   
                    iconstate = Iconstate.controller;
                    break;
                case 1:
                    Otherpanel.SetActive(true);
                    fadeout(nowcolum);
                   
                    iconstate = Iconstate.audio;
                    break;
                case 2:
                    Infomationpanel.SetActive(true);
                    fadeout(nowcolum);
                    
                    iconstate = Iconstate.graphic;
                    break;
                case 3:
                    Configpanel.SetActive(true);
                    fadeout(nowcolum);
                   
                    iconstate = Iconstate.save;
                    break;
            }

        }
            


        }

    void fadeout(int rest)
    {

        for (int i = 0; i < selfrect.Length; i++)
        {
            
                selfrect[i].GetComponent<TextMeshProUGUI>().DOFade(0f, 1f);

            

        }

    }
    public void Reset()
    {
        for (int i = 0; i < selfrect.Length; i++)
        {

            {
                selfrect[i].gameObject.SetActive(true);
                selfrect[i].GetComponent<TextMeshProUGUI>().DOFade(1f, 1f);
                

            }
        }
        
        Equipmentpanel.SetActive(false);
        Otherpanel.SetActive(false);
        Infomationpanel.SetActive(false);
         iconstate= Iconstate.choice;
        //Configpanel.SetActive(false);
    }

}


