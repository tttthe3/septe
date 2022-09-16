using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using TMPro;
public class skilltree_Controller : MonoBehaviour
{
    private GameObject[] Icons;
    [SerializeField]
    GameObject[] ItemIcon;
    public enum Getskillselect { Choose, Select };
    public Getskillselect skillselect;
    private int cout = 0;
    private int cout1 = 0;
    public GameObject firstselect;
    public GameObject currentselect;
    public Image NGpanel;
    public Image OKpanel;
    int inputcount = 0;

    public TextMeshProUGUI Skillname;
    public TextMeshProUGUI Skillinfo;

    // Start is called before the first frame update
    void Start()
    {
        currentselect = firstselect;
        currentselect.GetComponent<Image>().DOColor(Color.black, 1f).SetLoops(-1, LoopType.Yoyo);
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
       
        else if (Playerinput.Instance.Skill.Down&&inputcount==0)
        {
             OnClickChoice();
        }
        
        if (Playerinput.Instance.Skill2.Down && inputcount == 0)
        {
            Reset();
        }
        if (Playerinput.Instance.Select_Vert.Value == 0)
            cout = 0;
        if (Playerinput.Instance.Select_Hoz.Value == 0)
            cout1 = 0;
        if (Playerinput.Instance.Skill.Up)
            inputcount = 0;
        if (Playerinput.Instance.Skill2.Up)
            inputcount = 0;
        skillinfo();

    }

   

    public void OnClickLeft()
    {
        if (currentselect.GetComponent<skillicon>().GetLeft() == null)
            return;
        currentselect.GetComponent<Image>().DOPause();
        currentselect.GetComponent<Image>().DOColor(Color.white, 0.1f);
        currentselect = currentselect.GetComponent<skillicon>().GetLeft();
        currentselect.GetComponent<Image>().DOColor(Color.black, 1f).SetLoops(-1, LoopType.Yoyo);

    }

    public void OnClickRight()
    {
        if (currentselect.GetComponent<skillicon>().GetRight() == null)
            return;
        currentselect.GetComponent<Image>().DOPause();
        currentselect.GetComponent<Image>().DOColor(Color.white, 0.1f);
        currentselect = currentselect.GetComponent<skillicon>().GetRight();
        currentselect.GetComponent<Image>().DOColor(Color.black, 1f).SetLoops(-1, LoopType.Yoyo);
    }
    public void OnClickChoice()
    {

        inputcount++;

        if (skillselect == Getskillselect.Choose)
        {
            if(SkillManager.Instance.GetSkillpoint()<currentselect.GetComponent<skillicon>().Getskill().Getneedpoint())
            { 
                
                NGpanel.gameObject.SetActive(true);
                NGpanel.gameObject.transform.DOScale(new Vector3(2f,1f,1f),0.2f); 
                skillselect = Getskillselect.Select;
                Debug.Log(SkillManager.Instance.GetSkillpoint());
                Debug.Log(currentselect.GetComponent<skillicon>().Getskill().Getneedpoint());
            }
            else
            {
                OKpanel.gameObject.SetActive(true);
                OKpanel.gameObject.transform.DOScale(new Vector3(2f, 1f, 1f), 0.2f);
                OKpanel.GetComponent<Image>().DOColor(Color.white, 0f).SetUpdate(true);
                skillselect = Getskillselect.Select;
                Debug.Log(SkillManager.Instance.GetSkillpoint());
                Debug.Log(currentselect.GetComponent<skillicon>().Getskill().Getneedpoint());

            }

            return;
           

        }
        if (OKpanel.gameObject.activeSelf && skillselect == Getskillselect.Select&& !currentselect.GetComponent<skillicon>().Getskill().Getflag())
        {
            //演出
            SkillManager.Instance.UseSkillpoint(currentselect.GetComponent<skillicon>().Getskill().Getneedpoint());
            currentselect.GetComponent<skillicon>().Getskill().Setflag(true);
            OKpanel.GetComponent<Image>().DOColor(Color.black,0.4f).SetUpdate(true);
            Debug.Log(SkillManager.Instance.GetSkillpoint());
            return;
        }

    }

    public void Reset()
    {
        inputcount++;

        if (skillselect == Getskillselect.Select)
        {
            if (NGpanel.gameObject.activeSelf)
            {
                NGpanel.gameObject.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f);
                NGpanel.gameObject.SetActive(false);
                skillselect = Getskillselect.Choose;
            }

            if (OKpanel.gameObject.activeSelf)
            {
                OKpanel.gameObject.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f);
                OKpanel.gameObject.SetActive(false);
                skillselect = Getskillselect.Choose;
            }
        }

            else 
        {
            //this.gameObject.SetActive(false);
            return;
        }


    }

    public void skillinfo()
    {
        Skillname.text = currentselect.GetComponent<skillicon>().Getskill().GetSKillname();
        Skillinfo.text = currentselect.GetComponent<skillicon>().Getskill().GetSKillInfo();
    }
}
