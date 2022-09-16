using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using TMPro;
public class Weapon_Config : MonoBehaviour
{

    private GameObject[] Icons;
    [SerializeField]
    GameObject[] ItemIcon;
    private enum Iconstate { Weapon,skill };
    Iconstate iconstate;
    int nowcolum = 0;
    int nowrow = 0;
    [SerializeField]
    GameObject Slash;

    [SerializeField]
    GameObject Lans;

    [SerializeField]
    GameObject Gunmer;

    [SerializeField]
    GameObject Configpanel;
    private int cout = 0;
    private int cout1 = 0;


    // Start is called before the first frame update
    void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (Playerinput.Instance.Select_Hoz.Value == 1 && cout1 == 0)
        {
            cout1 = 1;
            OnClickRight();

        }
        else if (Playerinput.Instance.Select_Hoz.Value < 0 && cout1 == 0)
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

        if (Playerinput.Instance.Skill2.Down)
        {
            Reset();
        }
        if(Playerinput.Instance.Select_Vert.Value == 0)
            cout = 0;
        if (Playerinput.Instance.Select_Hoz.Value == 0)
            cout1 = 0;

    }

    private void GetAllChildObject(GameObject parents)
    {
        Icons = new GameObject[parents.transform.childCount-1];

        for (int i = 0; i < parents.transform.childCount-1; i++)
        {
            Icons[i] = parents.transform.GetChild(i).gameObject;
        }


    }

    public void OnClickUp()
    {
        if (iconstate == Iconstate.Weapon)
        {
            if (0 < nowcolum && nowcolum <= ItemIcon.Length)
            {

                ItemIcon[nowcolum].GetComponent<Image>().DOPause();
                ItemIcon[nowcolum].GetComponent<Image>().DOColor(Color.white, 0.1f); ;
                nowcolum--;
                Debug.Log(nowcolum);
                ItemIcon[nowcolum].GetComponent<Image>().DOColor(Color.black, 1f).SetLoops(-1, LoopType.Yoyo);
                // selfrect.position = new Vector3(currentpos.x+gridx, currentpos.y,currentpos.z);

            }
        }
        cout = 1;
    }

    public void OnClickDown()
    {
        if (iconstate == Iconstate.Weapon)
        {
            if (0 <= nowcolum && nowcolum < ItemIcon.Length - 1)
            {
                ItemIcon[nowcolum].GetComponent<Image>().DOPause();
                ItemIcon[nowcolum].GetComponent<Image>().DOColor(Color.white, 0.1f); ;
                nowcolum++;
                Debug.Log(nowcolum);
                ItemIcon[nowcolum].GetComponent<Image>().DOColor(Color.black, 1f).SetLoops(-1, LoopType.Yoyo);
            }
        }
        cout = 1;

    }

    public void OnClickRight()
    {
        if (iconstate == Iconstate.skill)
        {
            if (0 <= nowrow && nowrow < Icons.Length - 1)
            {


                Icons[nowrow].GetComponent<Image>().DOPause();
                Icons[nowrow].GetComponent<Image>().DOColor(Color.white, 0.1f); ;
                nowrow++;
                Icons[nowrow ].GetComponent<Image>().DOPause();
                Icons[nowrow ].GetComponent<Image>().DOColor(Color.white, 0f);
                Icons[nowrow].GetComponent<Image>().DOColor(Color.black, 1f).SetLoops(-1, LoopType.Yoyo);
            }
        }
        cout1 = 1;

    }

    public void OnClickLeft()
    {
        if (iconstate == Iconstate.skill)
        {

            if (0 < nowrow && nowrow <= Icons.Length)
            {



                Icons[nowrow].GetComponent<Image>().DOPause();
                Icons[nowrow].GetComponent<Image>().DOColor(Color.white, 0.1f); ;
                nowrow--;
                Icons[nowrow].GetComponent<Image>().DOPause();
                Icons[nowrow].GetComponent<Image>().DOColor(Color.white, 0f);
                Icons[nowrow].GetComponent<Image>().DOColor(Color.black, 1f).SetLoops(-1, LoopType.Yoyo);
            }
        }
        cout1 = 1;

    }

    public void OnClickChoice()
    {

        if (iconstate == Iconstate.Weapon)
        {
            ItemIcon[nowcolum].transform.DOMoveX(ItemIcon[nowcolum].transform.position.x-90f,0.2f);
            iconstate = Iconstate.skill;
            GetAllChildObject(ItemIcon[nowcolum]);
            Debug.Log(Icons.Length);
        }
        else
        {
            AttackWrapper.Instance.WeaponSetter(ItemIcon[nowcolum].gameObject.name);
            //スキルセット

        }



    }

    void fadeout(int rest)
    {

        for (int i = 0; i < ItemIcon.Length; i++)
        {

            ItemIcon[i].GetComponent<TextMeshProUGUI>().DOFade(0f, 1f);



        }

    }

    private void OnDisable()
    {
        ItemIcon[nowcolum].GetComponent<Image>().DOPause();
        Icons[nowrow].GetComponent<Image>().DOPause();
        nowcolum = 0;
        nowrow=0;
        iconstate =Iconstate.Weapon;


    }

    public void Reset()
    {
        if (iconstate == Iconstate.Weapon && StartMenu.menuInstance.Getcursolreturn() == StartMenu.cursolstate.sleep) {
            StartMenu.menuInstance.OnReset();
            return;
        }

        if(iconstate == Iconstate.skill)
        {
            ItemIcon[nowcolum].transform.DOMoveX(ItemIcon[nowcolum].transform.position.x + 90f, 0.2f);
            Icons[nowrow].GetComponent<Image>().DOPause();
            Icons[nowrow].GetComponent<Image>().DOColor(Color.white, 0f);
            nowrow = 0;
            iconstate = Iconstate.Weapon;
        }
        
    }

}
