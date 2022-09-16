using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using TMPro;
public class Skill_Controller : MonoBehaviour
{
    public enum State { itemchoice, set }
    public State state;
    int nowcolum = 0;
    int nowlow = 0;
    private GameObject ParentObject;
    private GameObject[] Icons = null;
    private WeaponSKill[] Slash_Skillcheck ;
    private int[] itemcount = null;
    private TextMeshProUGUI[] counts;
    private int[] countnumer = null;
    private int cout = 0;
    private int cout1 = 0;
    public WeaponSKill[] skillstate;

    public Image selector;
    public Image notenough;

    // Start is called before the first frame update
    void Start()
    {
        Slash_Skillcheck = SkillManager.Instance.GetSLash_SkillLists();
        ParentObject = this.gameObject;
        GetAllChildObject();
        GetSkillinfo();
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
      
    }

    private void GetSkillinfo() //後で武器別
    {
            skillstate = SkillManager.Instance.GetSLash_SkillLists();
            
        

    }



    public void OnClickRight()
    {
        if (state == State.itemchoice)
        {

            if (0 <= nowcolum && nowcolum < Icons.Length / 3 )
            {

                nowcolum++;
                Icons[nowlow * 3 + nowcolum - 1].GetComponent<Image>().DOPause();
                Icons[nowlow * 3 + nowcolum - 1].GetComponent<Image>().DOColor(Color.white, 0f);
                Icons[nowlow * 3 + nowcolum].GetComponent<Image>().DOColor(Color.blue, 1f).SetLoops(-1, LoopType.Yoyo);
                Icons[nowlow * 3 + nowcolum - 1].GetComponent<Image>().DOPause();
                Icons[nowlow * 3 + nowcolum - 1].GetComponent<Image>().DOColor(Color.white, 0f);
            }
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
                Icons[nowlow * 3 + nowcolum + 1].GetComponent<Image>().DOPause();
                Icons[nowlow * 3 + nowcolum + 1].GetComponent<Image>().DOColor(Color.white, 0f);
                Icons[nowlow * 3 + nowcolum].GetComponent<Image>().DOColor(Color.blue, 1f).SetLoops(-1, LoopType.Yoyo);
                Icons[nowlow * 3 + nowcolum + 1].GetComponent<Image>().DOPause();
                Icons[nowlow * 3 + nowcolum + 1].GetComponent<Image>().DOColor(Color.white, 0f);
            }
        }
       

        cout1 = 1;
    }

    public void OnClickDown()
    {
        if (state == State.itemchoice)
        {

            if (0 <= nowlow && nowlow < Icons.Length / 3 - 1)
            {

                nowlow++;
                Icons[(nowlow - 1) *3 + nowcolum].GetComponent<Image>().DOPause();
                Icons[(nowlow - 1) * 3 + nowcolum].GetComponent<Image>().DOColor(Color.white, 0f);
                Icons[nowlow *3 + nowcolum].GetComponent<Image>().DOColor(Color.blue, 1f).SetLoops(-1, LoopType.Yoyo);
                Icons[(nowlow - 1) * 3 + nowcolum].GetComponent<Image>().DOPause();
                Icons[(nowlow - 1) * 3 + nowcolum].GetComponent<Image>().DOColor(Color.white, 0f);
            }


        }
        cout = 1;
    }

    public void OnClickUp()
    {
        if (state == State.itemchoice)
        {

            if (1 <= nowlow && nowlow <= Icons.Length / 3)
            {
                nowlow--;
                Icons[(nowlow + 1) * 3 + nowcolum].GetComponent<Image>().DOPause();
                Icons[(nowlow + 1) * 3 + nowcolum].GetComponent<Image>().DOColor(Color.white, 0f);
                Icons[nowlow * 3 + nowcolum].GetComponent<Image>().DOColor(Color.black, 1f).SetLoops(-1, LoopType.Yoyo);
                Icons[(nowlow + 1) * 3 + nowcolum].GetComponent<Image>().DOPause();
                Icons[(nowlow + 1) * 3 + nowcolum].GetComponent<Image>().DOColor(Color.white, 0f);
            }
        }
        else
        {
            
        }

        cout = 1;
    }

    public void OnClickChoice()
    {
        

        if (state == State.itemchoice)
        {
            if (!skillstate[nowlow * 3 + nowcolum].Getflag())
            {
                Debug.Log("now");
                if (SkillManager.Instance.GetSkillpoint() > skillstate[nowlow * 3 + nowcolum].Getneedpoint())
                {
                    Debug.Log("now2");
                    selector.gameObject.SetActive(true);
                    state = State.set;
                    return;
                }
                else
                {
                    notenough.gameObject.SetActive(true);
                    state = State.set;
                    return;
                }
            }

        }
        else
        {
            if (notenough.gameObject.activeSelf)
            {
                notenough.gameObject.SetActive(false);
                state = State.itemchoice;
                return;
            }
            SkillManager.Instance.GetSLash_SkillList(nowlow * 3 + nowcolum).Setflag(true);
            selector.gameObject.SetActive(false);
            state = State.itemchoice;
        }

    }

    public void SkillGet()
    {

    }

    private void OnDisable()
    {
        // pickitem.transform.DOKill(true);
        //pickitem.transform.DOScale(defaultscale, 0f);
        Icons[nowlow * 5 + nowcolum].GetComponent<Image>().DOColor(Color.white, 0.0f);
        nowlow = 0;
        nowcolum = 0;
        state = State.itemchoice;
       
    }

    public void reset()
    {
        if (state == State.itemchoice)
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
