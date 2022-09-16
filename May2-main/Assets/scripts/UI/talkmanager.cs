using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using System;
using Fungus;

public class talkmanager : MonoBehaviour
{
    [SerializeField]
    private talkconteny talkconteny;


    // Start is called before the first frame update

    private GameObject conversationcounter;

    [SerializeField]
    private GameObject talkicon;

    [SerializeField]
    private GameObject talkUI;

    private string allmessage;

    private string allmessageplayer;

    private string[] allmessages;

    private string[] allmessageplayers;

    private int messagesize;

    private talkconteny.talktype_start startevent;

    [SerializeField]
    private string splitString = "<>";

    public Text messageplayer = null;

    public Text choice1 ;

    public Text choice2;

    public Button choice1_button;

    public Button choice2_button;

    public GameObject choices;

    private string[] splitmessageplayer;

    private string[] splitmessage;

    private string counter;

    private int messageNum=2;

    private int nowTextnum = 0;

    private bool isEndmessage=false;

    private bool isOnemessage=false;

    private bool playertalk = false;
   
    

    public Text messenger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MOB")
        {
            counter = collision.gameObject.name;
        }
    }

    public string Trans_counter()
    {
        return counter;
    }

    //会話導入パターン　コライダ衝突のみ、コライダは言ってAボタン、シーン導入

    private void Start()
    {
       // talks = GetComponent<TMP_Typewriter>();
    }

    void Update()
    {

     

    }

    public void Setconversation(GameObject counter)
    {
        talkicon.SetActive(true);
        conversationcounter= counter;

    }

    public void comple() {

        isOnemessage = true;
    }
    

    public void resetconversationcounter(GameObject counter)
    {
        if (counter == null)
            return;
        if (conversationcounter.GetInstanceID() == counter.GetInstanceID())
        {
            talkicon.SetActive(false);
            conversationcounter = null;
        }
    }

    public GameObject Geconversation()
    {
        return conversationcounter;
    }

    public void Starttalking()
    {
        var othertalkScript = conversationcounter.GetComponent<OthertalkScript>();
        othertalkScript.SetState(OthertalkScript.State.Talk, transform);
        Fungus.Flowchart flowcharts = othertalkScript.Getflowchart();
        string flowname = othertalkScript.getflowname();
        //this.allmessageplayer = othertalkScript.GetConversation().GetConversationMessageplayer();

        //messagesize = allmessage.Length;
        //this.startevent = othertalkScript.GetConversation().Gettalktype_start();
        //　分割文字列で一回に表示するメッセージを分割する

        //　初期化処理


        nowTextnum = 0;
        messageNum = 2;
        //talkUI.SetActive(true);
        //talks.Play("a", 3f, onComplete: () => Debug.Log("reset"));
        messageplayer.text = "";
        //talkicon.SetActive(false);
       // choices.SetActive(false);
        isOnemessage = false;
        isEndmessage = false;
        //　会話開始時の入力は一旦リセット
        //Input.ResetInputAxes();

        flowcharts.SendFungusMessage(flowname);
       
    }

    void EndTalking()
    {
        isEndmessage = true;
        nowTextnum = 0;
        talkUI.SetActive(false);
        //　ユニティちゃんと村人両方の状態を変更する
        var othertalkScript = conversationcounter.GetComponent<OthertalkScript>();
        othertalkScript.SetState(OthertalkScript.State.Wait);
        GetComponent<Charactercontrolelr>().SetState(Charactercontrolelr.talkstate.non);
        Playerinput.Instance.GainControl();
        //Input.ResetInputAxes();
    }

    public void choose1()
    {

      
        nowTextnum++;
        playertalk = false;


    }

    public void choose2()
    {
        
        nowTextnum++;
        playertalk = false;
    }

}
