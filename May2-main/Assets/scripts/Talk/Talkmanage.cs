using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Playables;
using DG.Tweening;
public class Talkmanage : MonoBehaviour
{
    private bool flagwrite;
    int num = 0;
    public bool night, talkingNow;
    public GameObject messageWindow;
    private Image messageWindowI;
    public List<string> charaTalkingWords = new List<string>();
    public TextMeshProUGUI talkingText, nameText;
    public AudioClip proceedingTalkSE;
    public UnityEvent Duringtalk;
    public UnityEvent Talkend;
    public UnityEvent talkset;
   
    private bool talktrigger=false;
    static public Talkmanage Instance { get { return s_talkmanage; } }
    static protected Talkmanage s_talkmanage;
    public TextMeshProUGUI select1;
    public TextMeshProUGUI select2;
    public bool selecttime;
    public bool choice1;
    private bool talkendcheck=true;
    private TextMeshProUGUI currenttext;
    void Start()
    {
        s_talkmanage = this;
        messageWindowI = messageWindow.GetComponent<Image>();
        currenttext = select1;
    }

    public bool talksend()
    {
        return talktrigger;    }

    public void talktrigger_set(bool trigger)
    {
        talktrigger=trigger;
    }


    public void DisplayMessageWindow(List<string> words, string name)
    {
        flagwrite = false;
        Duringtalk.Invoke();
        Playerinput.Instance.Intract.GainControl();
        Playerinput.Instance.Select_Vert.GainControl();
        nameText.text = name;
        charaTalkingWords.Clear();
        charaTalkingWords = new List<string>(words);
        messageWindow.SetActive(true);
        talkingText.text = charaTalkingWords[0];
     
    }

   

    public void Settalkender(bool trigger)
    {
        talkendcheck=trigger ;
    }

    public bool Gettalkender()
    {
       return talkendcheck;
    }

    public void ProceedingTalk(AudioSource charaAS, float pitch)
    {

        
            if (charaTalkingWords.Count > 0)
            {
            //loopcommad();
            //notloopcommand();
                if (talkingNow == true) return;
            Debug.Log(charaTalkingWords[0]);
                StartCoroutine(TalkText(charaAS, pitch));
            if (charaTalkingWords.Count==1)
                talkendcheck = false;
                //audioSourceSE.PlayOneShot(proceedingTalkSE);
            }
            else
            {

            {
                talkendcheck = false;
                messageWindow.SetActive(false);
                talkendcheck = false;
            }
            //audioSourceSE.PlayOneShot(proceedingTalkSE);

        }
    }

    public void commandshot()
    {
        if (charaTalkingWords.Count == 0)
        {

            {
                talkendcheck = false;
                messageWindow.SetActive(false);
                talkendcheck = false;
                Talkend.Invoke();
            }
            return;
        }

        if (talkingText.gameObject.activeSelf)
        {
            TalkCOmmand.Instance.NextTextload(charaTalkingWords[0], charaTalkingWords);
            notloopcommand();
            loopcommad();
            
        }
    }
    public void CloseMessageWindow()
    {
        
        messageWindow.SetActive(false);
    }

    public void Selctor()
    {  
        Debug.Log(currenttext);
        if (Playerinput.Instance.Select_Vert.Value == 1)
        {
            Debug.Log(currenttext);
            currenttext = select1;
            select1.DOColor(Color.black,0.1f);
            select2.DOColor(Color.white, 0.1f);
        }
        if(Playerinput.Instance.Select_Vert.Value ==-1) {
            currenttext = select2;
            select1.DOColor(Color.white, 0.1f);
            select2.DOColor(Color.black, 0.1f);
        }
        if (Playerinput.Instance.Intract.Down)
        {
            //num = TalkCOmmand.Instance.skip(charaTalkingWords[0]) ;
            talkingText.gameObject.SetActive(true);
            select1.gameObject.SetActive(false);
            select2.gameObject.SetActive(false);
            selecttime = false;
            talkingNow = false;
            if (currenttext == select2)
            {  
                //Debug.Log(TalkCOmmand.Instance.skip(charaTalkingWords[0]));
                //for (int i = 0; i < num; i++) {
                  //  charaTalkingWords.RemoveAt(0);
                }
            }
        
        return;
    }

    public bool emptycheck()
    {
        return charaTalkingWords.Count==0;
    }

    private IEnumerator TalkText(AudioSource charaAS, float pitch)
    {
        Debug.Log(charaTalkingWords.Count);
        talkingNow = true;
        int messageCount = 0; //現在表示中の文字数
        talkingText.text = ""; //テキストのリセット
        float minPitch = pitch - 0.5f;
        float maxPitch = pitch + 0.5f;

      
        //command確認
        Debug.Log(charaTalkingWords[0]);
            while (charaTalkingWords[0].Length > messageCount)//文字をすべて表示していない場合ループ
            {
                if (messageCount % 2 == 0)
                {
                    charaAS.pitch = Random.Range(minPitch, maxPitch);
                    charaAS.PlayOneShot(proceedingTalkSE);
                }
                talkingText.text += charaTalkingWords[0][messageCount];//一文字追加
                messageCount++;//現在の文字数
                yield return new WaitForSeconds(0.02f);
            }
        if (charaTalkingWords[0] != null)
        {
            Debug.Log(charaTalkingWords[0]);
            charaTalkingWords.RemoveAt(0);
        }
        if (charaTalkingWords.Count == 0)
        {
           
            {
               
                {
                    talkendcheck = false;
                    messageWindow.SetActive(false);
                    Talkend.Invoke();
                }
            }
        }
       
        talkingNow = false;
       
        Debug.Log(charaTalkingWords.Count);

    }

    void loopcommad()
    {
        if (TalkCOmmand.Instance.CommandCheck(charaTalkingWords[0]))

        {
            
            charaTalkingWords.RemoveAt(0);
            return;
      
        }else 
            return;

    }


    void notloopcommand()
    {
        Debug.Log("end");
        if (TalkCOmmand.Instance.NotloopCommandCheck(charaTalkingWords[0]))
        { //選択し開始
            selecttime = true;
            talkingText.gameObject.SetActive(false);
            charaTalkingWords.RemoveAt(0);
            talkingNow = true;
            return;
        }
    }

    public bool inputwait()
    {
        return Playerinput.Instance.Intract.Down;
    }
}