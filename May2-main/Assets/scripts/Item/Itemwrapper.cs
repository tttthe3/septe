using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemwrapper : MonoBehaviour
{
    static public Itemwrapper Instance { get { return s_Itemwrapper; } }
    static protected Itemwrapper s_Itemwrapper;
    private string leftItem;
    private string rightItem;

    public string itemname;
    public bool leftshoice=false;
    private void Awake()
    {
        s_Itemwrapper = this;
    }

    public virtual void FixedUpdate() { }
   
    public virtual void  firstframeLeft(Rigidbody2D rb2d, Animator animator)
    {
       

    }
    public virtual void updateframeLeft(Rigidbody2D rb2d,Animator animator)
    {
       
    }

    public virtual void endframeLeft() { }

    public virtual void firstframeRight(Rigidbody2D rb2d, Animator animator)
    {


    }
    public virtual void updateframeRight(Rigidbody2D rb2d, Animator animator)
    {

    }

    public virtual void endframeRight() { }
}
