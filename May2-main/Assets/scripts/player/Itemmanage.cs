using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;
public class Itemmanage : MonoBehaviour
{
    public enum State {itemchoice,set }
    public State state;
    int nowcolum = 0;
    int nowlow = 0;
    private GameObject ParentObject;
    private GameObject[] Icons=null;
    private List<CreateItem> Iconcheck = new List<CreateItem>();
    private int[] itemcount = null;
    private TextMeshProUGUI[] counts;
    private int[] countnumer=null;
    public Image Leftitem;
    public Image Centeritem;
    public Image Rightitem;
    public Transform lefticon;
    public Transform righticon;
    public Transform centericon;
    private int cout = 0;
    private int cout1 = 0;
    void Awake()
    {
        Iconcheck = ItemDataBase.Instance.GetUseItemList();
        ParentObject = this.gameObject;
        GetAllChildObject();
        counterset();
    }
    private void OnEnable()
    {
        
        if (Iconcheck == null) return;
        Debug.Log(Icons.Length);
        for (int i = 0; i < Icons.Length; i++)
        {
           if (ItemDataBase.Instance.HasItem_Use(Icons[i].gameObject.name))
            {
                Debug.Log(Icons[0].gameObject.name);
                for (int j = 0; j < ItemDataBase.Instance.GetUseItemList().Count; j++)
                {
                    //itemcount[0] = ItemDataBase.Instance.UseItemCount(Icons[0].gameObject.name);
                    if(Iconcheck[j].getitemname()==Icons[i].gameObject.name)
                    Icons[i].GetComponent<Image>().sprite = Iconcheck[j].GetSprite();
                    countnumer[i]++;
                }
            }
        }
        for (int i = 0; i < Icons.Length; i++)
        {
         
         counts[i] = Icons[i].GetComponentInChildren<TextMeshProUGUI>();
            counts[i].text = countnumer[i].ToString();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        if (Playerinput.Instance.Select_Hoz.Value == 1 && cout1 == 0)
        {
            cout1 = 1;
            OnClickRight();

        }
        else if (Playerinput.Instance.Select_Hoz.Value == -1 && cout1 == 0)
        {
            cout1 = 1;
            OnClickLeft();
        }

        else if (Playerinput.Instance.Select_Vert.Value == 1 && cout == 0)
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
            reset();
        }

        if (Playerinput.Instance.Select_Vert.Value == 0)
            cout = 0;
        if (Playerinput.Instance.Select_Hoz.Value == 0)
            cout1 = 0;
    }

   private void GetAllChildObject()
    {
       Icons = new GameObject[ParentObject.transform.childCount];

        for (int i = 0; i < ParentObject.transform.childCount; i++)
        {
            Icons[i] = ParentObject.transform.GetChild(i).gameObject;
        }
        Debug.Log(Icons[14]);
    }

    private void counterset()
    {
        counts = new TextMeshProUGUI[ParentObject.transform.childCount];
        countnumer = new int[ParentObject.transform.childCount];
        for (int i = 0; i < Icons.Length; i++)
            countnumer[i] = 0;
    }

    public void OnClickRight()
    {
        if (state == State.itemchoice)
        {

            if (0 <= nowcolum && nowcolum < Icons.Length / 3 - 1)
            {

                nowcolum++;
                Icons[nowcolum - 1].GetComponent<Image>().DOPause();
                Icons[nowcolum - 1].GetComponent<Image>().DOColor(Color.white, 0f);
                Icons[nowcolum].GetComponent<Image>().DOColor(Color.black, 1f).SetLoops(-1, LoopType.Yoyo);
                Icons[nowcolum - 1].GetComponent<Image>().DOPause();
                Icons[nowcolum - 1].GetComponent<Image>().DOColor(Color.white, 0f);
            }
        }
        else
        {
            Rightitem.sprite = Icons[nowlow * 5 + nowcolum].GetComponent<Image>().sprite;
            Debug.Log("suc3");
            ItemUseManager.Instance.WeaponrightSetter(Icons[nowlow * 5 + nowcolum].gameObject.name);
        }

        cout1 = 1;

    }

    public void OnClickLeft()
    {
        if (state == State.itemchoice)
        {

            if (1 <= nowcolum && nowcolum <= Icons.Length / 3)
            {
                nowcolum--;
                Icons[nowlow * 5 + nowcolum + 1].GetComponent<Image>().DOPause();
                Icons[nowlow * 5 + nowcolum + 1].GetComponent<Image>().DOColor(Color.white, 0f);
                Icons[nowlow*5+nowcolum].GetComponent<Image>().DOColor(Color.black, 1f).SetLoops(-1, LoopType.Yoyo);
                Icons[nowlow * 5 + nowcolum + 1].GetComponent<Image>().DOPause();
                Icons[nowlow * 5 + nowcolum + 1].GetComponent<Image>().DOColor(Color.white, 0f);
            }
        }
        else
        {
            Leftitem.sprite = Icons[nowlow * 5 + nowcolum].GetComponent<Image>().sprite;
            ItemUseManager.Instance.WeaponLeftSetter(Icons[nowlow * 5 + nowcolum].gameObject.name);
        }

        cout1 = 1;
    }

    public void OnClickDown()
    {
        if (state == State.itemchoice)
        {

            if (0 <= nowlow && nowlow < Icons.Length / 5 - 1)
            {

                nowlow++;
                Icons[(nowlow - 1) * 5 + nowcolum].GetComponent<Image>().DOPause();
                Icons[(nowlow - 1) * 5 + nowcolum].GetComponent<Image>().DOColor(Color.white, 0f);
                Icons[nowlow * 5 + nowcolum].GetComponent<Image>().DOColor(Color.black, 1f).SetLoops(-1, LoopType.Yoyo);
                Icons[(nowlow-1) * 5 + nowcolum ].GetComponent<Image>().DOPause();
                Icons[(nowlow - 1) * 5 + nowcolum ].GetComponent<Image>().DOColor(Color.white, 0f);
            }


        }
        cout = 1;
    }

    public void OnClickUp()
    {
        if (state == State.itemchoice)
        {

            if (1 <= nowlow && nowlow <= Icons.Length / 5)
            {
                nowlow--;
                Icons[(nowlow + 1) * 5 + nowcolum].GetComponent<Image>().DOPause();
                Icons[(nowlow + 1) * 5 + nowcolum].GetComponent<Image>().DOColor(Color.white, 0f);
                Icons[nowlow * 5 + nowcolum].GetComponent<Image>().DOColor(Color.black, 1f).SetLoops(-1, LoopType.Yoyo);
                Icons[(nowlow + 1) * 5 + nowcolum ].GetComponent<Image>().DOPause();
                Icons[(nowlow + 1) * 5 + nowcolum ].GetComponent<Image>().DOColor(Color.white, 0f);
            }
        }
        else
        {
            Centeritem.sprite = Icons[nowlow * 5 + nowcolum].GetComponent<Image>().sprite;
        }

        cout = 1;
    }


    public void OnClickChoice()
    {

        if (state == State.itemchoice)
        {
            
            //Icons[nowlow * 5 + nowcolum].GetComponent<Image>().DOColor(Color.blue, 0.1f);
            //choice.transform.position = iconinform[nowcolum].position;
            //choice.gameObject.SetActive(true); //点滅素材
            state = State.set;
            Leftitem.gameObject.transform.DOMoveX(800f, 0.5f);
            Centeritem.gameObject.transform.DOMoveX(920f, 0.5f);
            Rightitem.gameObject.transform.DOMoveX(1040f, 0.5f);


        }

    }

    private void OnDisable()
    {
        // pickitem.transform.DOKill(true);
        //pickitem.transform.DOScale(defaultscale, 0f);
        Icons[nowlow * 5 + nowcolum].GetComponent<Image>().DOColor(Color.white, 0.0f);
        nowlow = 0;
        nowcolum = 0;
        state = State.itemchoice;
        Leftitem.gameObject.transform.DOMove(lefticon.position, 0.5f);
        Centeritem.gameObject.transform.DOMove(centericon.position, 0.5f);
        Rightitem.gameObject.transform.DOMove(righticon.position, 0.5f);
    }

    public void reset()
    {
        if (state ==State.itemchoice)
        {
            StartMenu.menuInstance.Reset();

        }
        else
        {
            Icons[nowlow * 5 + nowcolum].GetComponent<Image>().DOColor(Color.white, 0.1f);
            state = State.itemchoice;
        }
    }
}
