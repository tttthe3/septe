using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using System;
using DG.Tweening;
using TMPro;
using Cinemachine;
using System.IO;
public class TalkCOmmand : MonoBehaviour
{
    static public TalkCOmmand Instance { get { return s_talkcommand; } }
    static protected TalkCOmmand s_talkcommand;
    public GameObject parent;
    public GameObject sentaku1;
    public GameObject sentaku2;
    public string CameraZoom = "CameraZoom";
    public string TimerDealy = "TimerDealy";
    public string parenton = "ParentON";
    public string loadimage_LEFT = "LoadImageLEFT";
    public string loadimage_RIGHT = "LoadImageRIGHT";
    public string delightimage_LEFT = "DeLight_Image_LEFT";
    public string delightimage_RIGHT = "DeLight_Image_RIGHT";
    public string lightimage_LEFT = "Light_Image_LEFT";
    public string lightimage_RIGHT = "Light_Image_LEFT";
    public string Selctor = "InitSelect";
    public string Choice1 = "Selector1";
    public string Choice2 = "Select2";
    public string StartSelect = "StartSelect";
    public string SkipColum = "SkipColum";
    public string NextLoad = "NextLoad";
    public string FindTarget = "FindTarget";
    //Load_Image
    float timer = 0;
    [SerializeField]
    private CinemachineVirtualCamera camera;
    private void Awake()
    {
        s_talkcommand = this;
         parenton = "ParentON";
          CameraZoom = "CameraZoom";
     TimerDealy = "TimerDealy";
    loadimage_LEFT = "LoadImageLEFT";
    loadimage_RIGHT = "LoadImageRIGHT";
      delightimage_LEFT = "DeLight_Image_LEFT";
  delightimage_RIGHT = "DeLight_Image_RIGHT";
     lightimage_LEFT = "Light_Image_LEFT";
     lightimage_RIGHT = "Light_Image_LEFT";
     Selctor = "InitSelect";
    Choice1 = "Selector1";
     Choice2 = "Selector2";
     StartSelect = "StartSelect";
     SkipColum = "SkipColum";
     
}
    public bool CommandCheck(string text)
    {
        if (text == null)
            return false;

        Debug.Log(Regex.IsMatch(text, NextLoad));
        if (Regex.IsMatch(text, CameraZoom))
        {
            CamraMove();
            return true;
        }

        if (Regex.IsMatch(text, TimerDealy))
        {
            return TimerDelayethod(text);
           
        }

        if (Regex.IsMatch(text, CameraZoom))
        {
            parentON();
            return true;
        }



        if (Regex.IsMatch(text, parenton)){

            parentON();
            return true;
        }

            if (Regex.IsMatch(text, loadimage_LEFT)){
            LoadImage_LEFT(text);
            Debug.Log(Regex.IsMatch(text, loadimage_LEFT));
            return true;
        }

        if (Regex.IsMatch(text, loadimage_RIGHT))
        {
            LoadImage_RIGHT(text);
            return true;
        }

        if (Regex.IsMatch (text, delightimage_LEFT))
        {
            DeLightImage_LEFT();
            return true;
        }
        if (Regex.IsMatch(text, delightimage_RIGHT))
        {
            DeLightImage_RIGHT();
            return true;
        }
        if (Regex.IsMatch(text, lightimage_LEFT))
        {
            DeLightImage_LEFT();
            return true;
        }
        if (Regex.IsMatch(text, lightimage_RIGHT))
        {
            DeLightImage_RIGHT();
            return true;
        }
        if (Regex.IsMatch(text, Selctor))
        {
            Selector(text);
            return true;
        }
        if (Regex.IsMatch(text, Choice1))
        {
            Selector1(text);
            return true;
        }

        if (Regex.IsMatch(text, Choice2))
        {
            Selector2(text);
            return true;
        }

        return false;

    }



    public bool NotloopCommandCheck(string text)
    {
        if (Regex.IsMatch(text, StartSelect))
        {
            Startselect();
            return true;
        }
        return false;    
    }

    public string FindText(string textname) //後ろに次ぎロードするテキスト　フラグ書き換え用
    {
        string after = null;
        int idx = textname.IndexOf(NextLoad);
        if (idx >= 0)
        {
            after = textname.Remove(idx, NextLoad.Length);
        }

        return textname;
       
    }

    public string FindCounter(string counter) //後ろに次ぎロードするテキスト　フラグ書き換え用pepple餅のターゲットの持つフラグ一覧から検索をかけてヒットすればフラグオン
    {
        string after = null;
        int idx = counter.IndexOf(FindTarget);
        if (idx >= 0)
        {
            after = counter.Remove(idx, FindTarget.Length);
        }



        return after;

    }

    public void NextTextload(string textname,List<string> words) //次の文書をロード
    {
        if (Regex.IsMatch(textname, NextLoad))
        {
            Debug.Log(textname);
            string after = null;
            int idx = textname.IndexOf(NextLoad);
            if (idx >= 0)
            {
                after = textname.Remove(idx, NextLoad.Length);
            }

            words.Clear();
            string path = "Assets/Resources/" + after;
            using (var fs = new StreamReader(path, System.Text.Encoding.GetEncoding("UTF-8")))

                while (fs.Peek() != -1)
                {
                    words.Add(fs.ReadLine());
                }
        }
        Debug.Log(words[0]);
        return;

    }

    public bool TimerDelayethod(string limit)
    {
        Debug.Log(timer);
        string after = null;
        int idx = limit.IndexOf(TimerDealy);
        if (idx >= 0)
        {
            after = limit.Remove(idx, TimerDealy.Length);
        }
        int num = Int32.Parse(after);
        timer += Time.deltaTime;
        StartCoroutine(TimerDelay2(num));
            return true;
        
    }

    IEnumerator  TimerDelay2(int limit)
    {
        yield return new WaitForSeconds(limit);
    }

    public void CamraMove()
    {
        
    }

    public void parentON()
    {
        if (!parent.activeSelf)
            parent.gameObject.SetActive(true);
    }
    public void LoadImage_LEFT(string text)
    {
        var LEFTICON = parent.transform.Find("Talk_LEFT_Character");
        Image left = LEFTICON.GetComponent<Image>();
        string after=null;
        int idx = text.IndexOf(loadimage_LEFT);
        if (idx >= 0)
        {
            after = text.Remove(idx, loadimage_LEFT.Length);
        }
      
        Sprite image= Resources.Load<Sprite>(after);
        Debug.Log(image);
        Debug.Log(LEFTICON);
        left.sprite = image;
        left.DOColor(Color.clear, 0f);
        left.DOColor(Color.white, 0.4f);
    }

    public void LoadImage_RIGHT(string text)
    {
        var RIGHTICON = parent.transform.Find("Talk_RIGHT_Character");
        Image RIGHT = RIGHTICON.GetComponent<Image>();
        string after = null;
        int idx = text.IndexOf(loadimage_RIGHT);
        if (idx >= 0)
        {
            after = text.Remove(idx, loadimage_RIGHT.Length);
        }
        Sprite image = Resources.Load<Sprite>(after);
        RIGHT.sprite = image;
        RIGHT.DOColor(Color.clear, 0f);
        RIGHT.DOColor(Color.white, 0.4f);
    }

    public void DeLightImage_LEFT()
    {
        var LEFTICON= parent.transform.Find("Talk_LEFT_Character");
        LEFTICON.GetComponent<Image>().DOColor(Color.clear,0f);
    }

    public void DeLightImage_RIGHT()
    {
        var RIGHTICON = parent.transform.Find("Talk_RIGHT_Character");
        RIGHTICON.GetComponent<Image>().DOColor(Color.clear, 0f);
    }

    public void LightImage_LEFT()
    {
        var LEFTICON = GameObject.Find("Talk_LEFT_Character");
        LEFTICON.GetComponent<Image>().DOColor(Color.white, 0f);
    }

    public void LightImage_RIGHT()
    {
        var RIGHTICON = GameObject.Find("Talk_RIGHT_Character");
        RIGHTICON.GetComponent<Image>().DOColor(Color.white, 0f);
    }

    public void Selector(string text)
    {
      
        
           sentaku1.SetActive(true);
      
            sentaku2.SetActive(true);
    }

    public void Selector1(string text)
    {
        var choice1 = sentaku1;
        TextMeshProUGUI texts =  choice1.GetComponent<TextMeshProUGUI>();
        
        string after = null;
        int idx = text.IndexOf(Choice1);
        if (idx >= 0)
        {
            after = text.Remove(idx, Choice1.Length);
        }
        texts.text = after;

    }

    public void Selector2(string text)
    {
        var choice1 = sentaku2;
        TextMeshProUGUI texts = choice1.GetComponent<TextMeshProUGUI>();

        string after = null;
        int idx = text.IndexOf(Choice2);
        if (idx >= 0)
        {
            after = text.Remove(idx, Choice2.Length);
        }
        texts.text = after;
    }

    public void Startselect()
    {

    }

    public int skip(string text)
    {
        string after = null;
        int idx = text.IndexOf(SkipColum);
        if (idx >= 0)
        {
            after = text.Remove(idx, SkipColum.Length);
        }
        int num = Int32.Parse(after);
        return num;
    }

    public void looptext()
    {

    }

}
