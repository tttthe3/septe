using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;
public class InteractWrapper : MonoBehaviour
{
    static public InteractWrapper Instance { get { return s_Interact; } }
    static protected InteractWrapper s_Interact;
    private string leftItem;
    private string rightItem;
    public bool HELD;

    private void Awake()
    {
        s_Interact = this;
    }

     public virtual void FixedUpdate() {

       
    
    }

   
    public virtual void firstframe(Rigidbody2D rb2d, Animator animator,Transform parent)
    {


    }
    public virtual void updateframe(Rigidbody2D rb2d, Animator animator,SkeletonAnimation animation)
    {

    }

    public virtual void endframe() { }

   
    
}
