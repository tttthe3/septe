using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class GetitemShow : MonoBehaviour
{
    static protected GetitemShow s_Itemshow;
    static public GetitemShow Instance { get { return s_Itemshow; } }
    public GameObject panel;
    private CreateItem nowdate;
    public Ease easetype;
    public Image itemicon;
    public Image waiticon;
    public Text flavor;
    public Text Itemname;
    private Color endcolor = new Color(255, 255, 255);
    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
        s_Itemshow = this;
    }

    public void seticonname(string itemname)
    {
        Itemname.text = itemname;
    }

    // Update is called once per frame
    public void Getitem()
    {
        panel.SetActive(true);
        itemicon.sprite = ItemDataBase.Instance.GetItemData(Itemname.text).GetSprite();
        flavor.text= ItemDataBase.Instance.GetItemData(Itemname.text).getinfomation();
        itemicon.DOBlendableColor(endcolor,0f);
        flavor.DOFade(1f,2f);
        Itemname.DOFade(1f, 1f);
        StartCoroutine( waitforInput());
    }

    IEnumerator waitforInput()
    {
        yield return new WaitForSeconds(3f);
        waiticon.DOFade(0f, 1f).SetEase(this.easetype).SetLoops(-1, LoopType.Yoyo);
        yield return new WaitUntil(inputwait);

        panel.SetActive(false);
    }

    public bool inputwait()
    {
        return Playerinput.Instance.Intract.Down;
    } 

}
