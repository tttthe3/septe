using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class People : MonoBehaviour
{
    private GameObject player;
    [System.NonSerialized]
    public GameObject speechBubbleOb;
    private Talkmanage gameManager;

    public List<string> talkingWords = new List<string>();
    private bool talks=false;
    public bool man;
    
    private AudioSource audioSource;
    public float audioPitch;
    private CapsuleCollider capsuleCollider;
    public Flag_content[] targetflag;
    public enum talkstate {during,cant }
    public talkstate state=talkstate.during;
    private bool talkcan = false;
    // Start is called before the first frame update
    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("Talkmanage").GetComponent<Talkmanage>();
        speechBubbleOb = transform.Find("SpeechBubble").gameObject;
        audioSource = GetComponent<AudioSource>();
        AddWords();
        Gender();
    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log(Talkmanage.Instance.Gettalkender());
        if (speechBubbleOb == null) return;
        float disToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (disToPlayer < 2.5f && speechBubbleOb.activeSelf == false)
        {
            if(Talkmanage.Instance.Gettalkender())
            StartCoroutine("DisplaySB");
        }
        if (disToPlayer < 2.5f && speechBubbleOb.activeSelf == true)
        {
            if (!Talkmanage.Instance.Gettalkender())
                StartCoroutine("CloseSB");
        }

        if (disToPlayer >= 2.5f && speechBubbleOb.activeSelf == true)
        {
           
            StartCoroutine("CloseSB");
            
            Talkmanage.Instance.CloseMessageWindow();
        }

        if (disToPlayer >= 2.5f && speechBubbleOb.activeSelf == false)
        {
            Talkmanage.Instance.Settalkender(true);
            Debug.Log(Talkmanage.Instance.Gettalkender());
            //Talkmanage.Instance.CloseMessageWindow();
        }

        if (speechBubbleOb.activeSelf == true)
        {
            

                if (Talkmanage.Instance.messageWindow.activeSelf == true)
                {
                    if (Talkmanage.Instance.select1.gameObject.activeSelf && Talkmanage.Instance.select2.gameObject.activeSelf)
                    {
                       
                        Talkmanage.Instance.Selctor();
                    }
                }

            if (Talkmanage.Instance.messageWindow.activeSelf == true)
            {  //毎ループ
                Talkmanage.Instance.commandshot();
                
            }

            if (Playerinput.Instance.Intract.Down)
            {

                if (Talkmanage.Instance.messageWindow.activeSelf == false )
                {
                    state = talkstate.during;
                    Talkmanage.Instance.DisplayMessageWindow(talkingWords, this.gameObject.name);
                    Talkmanage.Instance.ProceedingTalk(audioSource, audioPitch);
                    talks = true;
                }
                else if (Talkmanage.Instance.talkingText.gameObject.activeSelf)
                {
                    
                    Talkmanage.Instance.ProceedingTalk(audioSource, audioPitch);
                } 


            }
                  
        }
    }
    private IEnumerator DisplaySB()
    {
        if (Charactercontrolelr.CCInstance.Getvelocity().x > -1f && Charactercontrolelr.CCInstance.Getvelocity().x < 1f && Charactercontrolelr.CCInstance.Getvelocity().y < 1f)
        {
            speechBubbleOb.SetActive(true);
            yield break;
        }
        //Talkmanage.Instance.talktrigger_set(true);
        float c = 0.2f;
        while (speechBubbleOb.transform.localScale.x < 0.7f)
        {
            yield return new WaitForSeconds(0.01f);
            speechBubbleOb.transform.localScale += new Vector3(c, c, c);
        }
        yield break;
    }

    public IEnumerator CloseSB()
    {
        Talkmanage.Instance.talktrigger_set(false);
        float c = 0.05f;
        while (speechBubbleOb.transform.localScale.x > 0f)
        {
            yield return new WaitForSeconds(0.01f);
            speechBubbleOb.transform.localScale -= new Vector3(c, c, c);
        }
        speechBubbleOb.transform.localScale = new Vector3(0, 0, 0);
        speechBubbleOb.SetActive(false);
        yield break;
    }

    public virtual void AddWords()
    {

    }

    public virtual void Gender()
    {

    }

    public virtual void Selection()
    {

    }
    public virtual void writeflag()
    {

    }
}