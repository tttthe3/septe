using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;
public class Pushact : InteractWrapper
{
    private Vector3 velocity = Vector3.zero;
    
    public override void firstframe(Rigidbody2D rb2d, Animator animator,Transform parent)
    {
        animator.CrossFadeInFixedTime("push",0f);

    }
    public override void updateframe(Rigidbody2D rb2d, Animator animator, SkeletonAnimation animation)
    {
        if (Playerinput.Instance.Intract.Held)
        {
            Vector3 targetvelocity = new Vector2(Playerinput.Instance.Horizontal.Value * 5f, rb2d.velocity.y);
            rb2d.velocity = Vector3.SmoothDamp(rb2d.velocity, targetvelocity, ref velocity, 0.05f);
        }
        else
        {
            animator.CrossFadeInFixedTime("idle",0f);
        }
       
    }

    public override void endframe() { }
}
