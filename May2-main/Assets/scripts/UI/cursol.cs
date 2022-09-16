using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class cursol : MonoBehaviour
{
    private enum Iconstate { itemchoice, whilepick, skillchoice };
    Iconstate iconstate;
    [SerializeField]
    private Transform defaultpostion;

    [SerializeField]
    private CanvasGroup Mainpanel;

    [SerializeField]
    private RectTransform choice;

    [SerializeField]
    RectTransform selfrect;
    public int itemcolm;
    public int itemrow;
    [SerializeField]
    GridLayoutGroup gridspace;

    public GameObject[] Icons;
    public GameObject ParentObject;
    [SerializeField]
    Image leftitem;

    [SerializeField]
    Image downitem;

    [SerializeField]
    Image rightitem;

    [SerializeField]
    GameObject ItemsetCheck;

    private Image pickitem;

    public Vector3 defaultscale;

    [SerializeField]
    Transform[] iconinform;

    int nowcolum = 0;
    int nowrow = 0;
    private GameObject nowselectskill;

    [SerializeField]
    RectTransform selfskillselect;

    private string setname;
    private RectTransform[] selecter;
    float gridx = 0;
    private List<CreateItem> icon = new List<CreateItem>();

    public AudioSource click1;
    public AudioSource click2;
    public AudioSource move1;
    public AudioSource move2;
    private int cout = 0;
    private int cout1 = 0;
    private void AWake()
    {
        selfrect = GetComponent<RectTransform>();
        selfskillselect = GetComponent<RectTransform>();
        pickitem = GetComponent<Image>();
        gridspace = GetComponent<GridLayoutGroup>();
        defaultpostion = GetComponent<Transform>();
        iconstate = Iconstate.itemchoice;
        leftitem = GetComponent<Image>();
        rightitem = GetComponent<Image>();
        downitem = GetComponent<Image>();
        GetAllChildObject();
    }
    void OnEnable()
    {
        icon = ItemDataBase.Instance.GetItemList();
        //pickitem.transform.DOKill(true);
        defaultscale = iconinform[0].localScale;
        gridx = (gridspace.cellSize.x) * 1;
        selfrect.DOMove(iconinform[nowcolum].position, 0f).SetUpdate(true);
        choice.gameObject.SetActive(false);
        Debug.Log(icon[ItemDataBase.Instance.HasItemnumber(iconinform[0].gameObject.name)].Getitemlevels()[0].icons);
        seticons();
        GetAllChildObject();

    }

    private void GetAllChildObject()
    {
        Icons = new GameObject[ParentObject.transform.childCount];

        for (int i = 0; i < ParentObject.transform.childCount; i++)
        {
            Icons[i] = ParentObject.transform.GetChild(i).gameObject;
            Debug.Log(Icons[i]);
        }
        
        
    }

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




    // Start is called before the first frame update
    public void OnClickRight()
    {
        if (iconstate == Iconstate.itemchoice)
        {
            if (0 <= nowcolum && nowcolum <= itemcolm - 1)
            {
               
                move1.Play();
                //selfrect.DOMove(iconinform[nowcolum].position, 0.4f).SetUpdate(true);
                //choice.transform.position = iconinform[nowcolum].position;
                nowcolum++;
                Icons[nowcolum - 1].GetComponent<Image>().DOPause();
                Icons[nowcolum - 1].GetComponent<Image>().DOColor(Color.white, 0f);
                Icons[nowcolum].GetComponent<Image>().DOColor(Color.black, 1f).SetLoops(-1, LoopType.Yoyo);
            }
        }
        cout = 1;

    }

    public void OnClickLeft()
    {
        if (iconstate == Iconstate.itemchoice)
        {

            if (1 <= nowcolum && nowcolum <= itemcolm)
            {
             
                move1.Play();
             
                // selfrect.DOMove(iconinform[nowcolum].position, 0.4f).SetUpdate(true);
                //choice.transform.position = iconinform[nowcolum].position;
                nowcolum--;
                Icons [nowcolum + 1].GetComponent<Image>().DOPause();
                Icons[ nowcolum + 1].GetComponent<Image>().DOColor(Color.white, 0f);
                Icons[nowcolum].GetComponent<Image>().DOColor(Color.black, 1f).SetLoops(-1, LoopType.Yoyo);
            }
        }
        cout = 1;

    }

    public void OnClickUp()
    {
        if (iconstate == Iconstate.whilepick)
        {
            if (0 < nowrow && nowrow <= itemrow)
            {
                move2.Play();
                selecter[nowrow].GetComponent<Image>().DOPause();
                selecter[nowrow].GetComponent<Image>().DOColor(Color.white, 0.1f); ;
                nowrow--;
                Debug.Log(nowcolum);
                selecter[nowrow].GetComponent<Image>().DOColor(Color.black, 1f).SetLoops(-1, LoopType.Yoyo);
                // selfrect.position = new Vector3(currentpos.x+gridx, currentpos.y,currentpos.z);

            }
        }
        cout1 = 1;
    }

    public void OnClickDown()
    {
        if (iconstate == Iconstate.whilepick)
        {
            if (0 <= nowrow && nowrow < itemrow - 1)
            {
                move2.Play();
                selecter[nowrow].GetComponent<Image>().DOPause();
                selecter[nowrow].GetComponent<Image>().DOColor(Color.white, 0.1f);
                nowrow++;
                Debug.Log(nowcolum);
                selecter[nowrow].GetComponent<Image>().DOColor(Color.black, 1f).SetLoops(-1, LoopType.Yoyo);
            }
        }

        cout1 = 1;
    }

    public void OnClickChoice()
    {

        if (iconstate == Iconstate.itemchoice)
        {
            click1.Play();
            //iconinform[nowcolum].GetComponent<Image>().DOColor(Color.blue, 0.1f);
            //choice.transform.position = iconinform[nowcolum].position;
            //choice.gameObject.SetActive(true); //点滅素材
            iconstate = Iconstate.whilepick;
          
            var childIndex = 0;
            selecter = new RectTransform[iconinform[nowcolum].childCount];
            foreach (RectTransform child in iconinform[nowcolum])
            {
                selecter[childIndex++] = child; //子のボタン全取得
            }
            itemrow = selecter.Length;

        }
        else if (iconstate == Iconstate.whilepick) //スキル選択中
        {
            click2.Play();
           // for (int i = 0; i < iconinform.Length; i++)
            {
               // if (ItemDataBase.Instance.HasItem(iconinform[i].gameObject.name))
                {
                    //AttackWrapper.Instance.WeaponSetter(icon[ItemDataBase.Instance.HasItemnumber(iconinform[i].gameObject.name)].Getstatenames()[0]);
                    AttackWrapper.Instance.WeaponSetter(Icons[nowcolum].gameObject.name);
                    //Charactercontrolelr.CCInstance.setattack1Aname(icon[ItemDataBase.Instance.HasItemnumber(iconinform[i].gameObject.name)].Getstatenames()[0]);
                    //Charactercontrolelr.CCInstance.setattack2Aname(icon[ItemDataBase.Instance.HasItemnumber(iconinform[i].gameObject.name)].Getstatenames()[1]); //state名を取得
                    //Charactercontrolelr.CCInstance.setattack3Aname(icon[ItemDataBase.Instance.HasItemnumber(iconinform[i].gameObject.name)].Getstatenames()[2]);

                }

             
            }
           // Charactercontrolelr.CCInstance.setattack1Aname(iconinform[nowcolum].gameObject.name);



        }
    }

    public void Onclickitemset()
    {


    }

    public void reset()
    {
        if (iconstate == Iconstate.itemchoice)
        {
            StartMenu.menuInstance.Reset();

        }
        else {
            iconinform[nowcolum].GetComponent<Image>().DOColor(Color.white, 0.1f);
            selecter[nowrow].GetComponent<Image>().DOPause();
            selecter[nowrow].GetComponent<Image>().DOColor(Color.white, 0.1f);
            iconstate = Iconstate.itemchoice;
        }
    }
    private void OnDisable()
    {
        // pickitem.transform.DOKill(true);
        //pickitem.transform.DOScale(defaultscale, 0f);
        iconinform[nowcolum].GetComponent<Image>().DOColor(Color.white, 0.0f);
        nowrow = 0;
        nowcolum = 0;
        selfrect.DOMove(iconinform[nowcolum].position, 0f).SetUpdate(true);
        iconstate = Iconstate.itemchoice;

    }

    public void seticons()
    {
        for (int i = 0; i < Icons.Length; i++) {
            var childIndex = 0;
            selecter = new RectTransform[Icons[i].transform.childCount];
            foreach (RectTransform child in Icons[i].transform)
            {
                selecter[childIndex++] = child; //子のボタン全取得
            }
            itemrow = selecter.Length;
          

        for (int j = 0; j < selecter.Length;j++) {

                if (ItemDataBase.Instance.HasItem(iconinform[i].gameObject.name))
                {
                    selecter[j].GetComponent<Image>().sprite = icon[ItemDataBase.Instance.HasItemnumber(Icons[i].gameObject.name)].Getitemlevels()[j].icons;
                   
                    Debug.Log(iconinform[i].gameObject.name);
                    Debug.Log(icon[ItemDataBase.Instance.HasItemnumber(iconinform[i].gameObject.name)].Getitemlevels()[j].icons);
                }


            }

        }

    }
}
