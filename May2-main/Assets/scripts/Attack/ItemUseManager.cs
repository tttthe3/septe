using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUseManager : MonoBehaviour
{
    // Start is called before the first frame update
    private string leftItem;
    private string rightItem;
    [SerializeField]
    private Itemwrapper[] Itemlist;
    private Itemwrapper Leftcurrentitem=null;
    private Itemwrapper Rightcurrentitem = null;
    private bool Lefton = false;
    static public ItemUseManager Instance { get { return s_ItemUse; } }
    static protected ItemUseManager s_ItemUse;

    private void Awake()
    {
        s_ItemUse = this;
        //再開前の設定を保持する仕様

    }

    private void FixedUpdate()
    {
        if (Leftcurrentitem != null)
        Leftcurrentitem.FixedUpdate();
        if (Rightcurrentitem != null)
        Rightcurrentitem.FixedUpdate();
    }

    public void WeaponLeftSetter(string ItemName)
    {
        leftItem = ItemName;
        for (int i=0;i<Itemlist.Length;i++)
        if (Itemlist[i].itemname==leftItem)
        {
                Leftcurrentitem = Itemlist[i];
                Leftcurrentitem.leftshoice = true;
                Debug.Log(Leftcurrentitem);
            }
        Lefton = true;
        
    }

    public void WeaponrightSetter(string ItemName)
    {
        rightItem = ItemName;
        for (int i = 0; i < Itemlist.Length; i++)
            if (Itemlist[i].itemname == rightItem)
            {
                
                Rightcurrentitem = Itemlist[i];
                Rightcurrentitem.leftshoice =false;
                Debug.Log(Rightcurrentitem);
            }

    }

    public bool leftChecker()
    {
        if (Leftcurrentitem == null)
            return false;
        if (Leftcurrentitem.leftshoice)
            return true;
        else
            return false;
    }

    
    public  void FirstframeLeft(Rigidbody2D rb2d, Animator animator)
    {
        if (Leftcurrentitem == null)
            return;
            Leftcurrentitem.firstframeLeft(rb2d,animator);

    }
    public  void UpdateframeLeft(Rigidbody2D rb2d, Animator animator)
    {
        if (Leftcurrentitem == null)
            return;
        Leftcurrentitem.updateframeLeft(rb2d,animator);
    }

    public  void EndframeLeft() {
        if (Leftcurrentitem == null)
            return;
    }
    public void FirstframeRight(Rigidbody2D rb2d, Animator animator)
    {
        if (Rightcurrentitem == null)
            return;
        Rightcurrentitem.firstframeRight(rb2d, animator);

    }
    public void UpdateframeRight(Rigidbody2D rb2d, Animator animator)
    {
        if (Rightcurrentitem == null && Rightcurrentitem.leftshoice == false)
            return;
        Rightcurrentitem.updateframeRight(rb2d, animator);
    }

    public void EndframeRight()
    {
    }
}
