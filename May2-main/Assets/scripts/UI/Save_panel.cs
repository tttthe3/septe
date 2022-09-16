using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using System.Collections;

public class Save_panel : MonoBehaviour
{
    private int cout = 0;
    [SerializeField]
    private CanvasGroup savelist;
    [SerializeField]
    RectTransform[] selfrect;
    int nowcolum = 0;
    public string homename;
    private string dataname;
    // Start is called before the first frame update
    const string datadir = "Assets/Resources";
    void Start()
    {
        savelist.gameObject.SetActive(true);
        selfrect[nowcolum].GetComponent<Image>().DOPause();
        selfrect[nowcolum].GetComponent<Image>().DOColor(Color.black, 1f).SetLoops(-1, LoopType.Yoyo);
     
    }

    void Update()
    {


        if (Playerinput.Instance.Select_Hoz.Value < 0 && cout == 0)
        {
            cout = 1;
            OnClickUp();

        }
        else if (Playerinput.Instance.Select_Hoz.Value == 1 && cout == 0)
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
        if (Playerinput.Instance.Select_Hoz.Value == 0)
            cout = 0;
    }

    // Update is called once per frame
    public void OnClickUp()
    {

        if (0 < nowcolum && nowcolum <= selfrect.Length)
        {

            selfrect[nowcolum].GetComponent<Image>().DOPause();
            selfrect[nowcolum].GetComponent<Image>().DOColor(Color.white, 0.1f); ;
            nowcolum--;
            Debug.Log(nowcolum);
            selfrect[nowcolum].GetComponent<Image>().DOColor(Color.black, 1f).SetLoops(-1, LoopType.Yoyo);
            // selfrect.position = new Vector3(currentpos.x+gridx, currentpos.y,currentpos.z);

        }
        cout = 1;
    }

    public void OnClickDown()
    {

        if (0 <= nowcolum && nowcolum < selfrect.Length - 1)
        {
            selfrect[nowcolum].GetComponent<Image>().DOPause();
            selfrect[nowcolum].GetComponent<Image>().DOColor(Color.white, 0.1f); ;
            nowcolum++;
            Debug.Log(nowcolum);
            selfrect[nowcolum].GetComponent<Image>().DOColor(Color.black, 1f).SetLoops(-1, LoopType.Yoyo);
        }

        cout = 1;

    }

    void OnClickChoice()
    {
        switch (nowcolum)
        {
            case 0:
                dataname = "sava_data1.json";
                break;
            case 1:
                dataname = "sava_data2.json";
                break;
            case 2:
                dataname = "sava_data3.json";
                break;
           
        }
        savainstance.Instance.saveupdate();
        savainstance.Instance.saveset(dataname);
        Time.timeScale = 1f;
        StartCoroutine(Savestart());
    }
    public void Reset()
    {
        for (int i = 0; i < selfrect.Length; i++)
        {

            {
                selfrect[i].gameObject.SetActive(true);
                selfrect[i].GetComponent<Image>().DOFade(1f, 1f);


            }
        }

    }

    IEnumerator Savestart()
    {
        
        Playerinput.Instance.ReleaseController(true);
        //StartCoroutine(ScreenFader.FadeSceneOut(ScreenFader.FadeType.Loading)); //フェードイン
        Debug.Log("load22");
        yield return SceneManager.LoadSceneAsync("opning"); 
        yield return new WaitForSeconds(1.0f); //wait one second before respawing
        Debug.Log("load");
       
        yield return new WaitForSeconds(3.0f);
        yield return StartCoroutine(ScreenFader.FadeSceneOut(ScreenFader.FadeType.Black));
       
        yield return null;
        yield return StartCoroutine(ScreenFader.FadeSceneIn());
        Playerinput.Instance.GainControl();

    }


}
