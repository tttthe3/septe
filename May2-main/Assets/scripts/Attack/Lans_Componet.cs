using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;
using Rewired;
using DG.Tweening;
public class Lans_Componet : AttackWrapper
{
    public enum combstate { one, two, three }
    public combstate now = combstate.one;
    private Damager damages;
    private float current;
    public float firsttimer;
    public float secondtimer;
    public float thirdtimer;

    public float chargetime;
    private GameObject WeaponParent;
    public RandomAudioPlayer Slash_first;
    // Start is called before the first frame update
    public void Playsound()
    {
        Slash_first.PlayRandomSound();
    }
    public override void Firstframe(Rigidbody2D rb2d, float newHorizontalMovement, Animator animator, Transform parent)
    {
        Slash_first = parent.Find("sounds/Lance/Lance1").GetComponent<RandomAudioPlayer>();
        Debug.Log(parent);
        damages = parent.Find("SkeletonUtility-SkeletonRoot/root/lance1").GetComponent<Damager>(); ;
        damages.enabled = false;

        current = 0f;
        if (now == combstate.one)
        {
            DOVirtual.DelayedCall(0.4f, () => Playsound(), false);
            Debug.Log(now);
            rb2d.velocity = new Vector2(Charactercontrolelr.CCInstance.m_SpriteForward.x * newHorizontalMovement, 0f);
            animator.CrossFadeInFixedTime("lans1", 0f);
        }
        else if (now == combstate.two)
            rb2d.velocity = new Vector2(Charactercontrolelr.CCInstance.m_SpriteForward.x * newHorizontalMovement * 0.4f, 0f);
        else if (now == combstate.three)
            rb2d.velocity = new Vector2(Charactercontrolelr.CCInstance.m_SpriteForward.x * newHorizontalMovement * 0.2f, 0f);
    }

    public override void Updateframe(Animator animator)
    {
        current += Time.deltaTime;


        if (now == combstate.one)
        {
            if (current < 0.4f)
            {
                damages.enabled = true;
                if (Playerinput.Instance.Attack.Down)
                {
                    if (current > 0.1f)
                    {
                        animator.CrossFadeInFixedTime("lans2", 0f);
                        now = combstate.two;
                    }

                }

            }
            else
            {
                //damage.enabled = false;
                now = combstate.one;
                animator.CrossFadeInFixedTime("idle", 0f);
                current = 0f;
                damages.enabled = false;
            }
        }

        if (now == combstate.two)
        {
            if (current < 0.4f)
            {
                if (Playerinput.Instance.Attack.Down)
                {
                    damages.enabled = true;
                    //animator.CrossFadeInFixedTime("slash4", 0f);
                    now = combstate.three;

                }

            }
            else
            {
                //damage.enabled = false;
                now = combstate.one;
                animator.CrossFadeInFixedTime("idle", 0f);
                current = 0f;
                damages.enabled = false;
            }
        }

        if (now == combstate.three)
        {
            if (current > 1f)
            {
                animator.CrossFadeInFixedTime("idle", 0f);
                now = combstate.one;
            }
        }

    }

    public override void Special_Firstframe(Rigidbody2D rb2d, float newHorizontalMovement, Animator animator, int set, Transform parent)
    {
        chargetime = 0f;
        if (set == 1)
        {
            rb2d.velocity = new Vector2(Charactercontrolelr.CCInstance.m_SpriteForward.x * newHorizontalMovement, 0f);
            animator.CrossFadeInFixedTime("lans3", 0f);
        }
        else if (set == 2)
        {
            rb2d.velocity = new Vector2(Charactercontrolelr.CCInstance.m_SpriteForward.x * newHorizontalMovement * 0.4f, 0f);
            animator.CrossFadeInFixedTime("slash1", 0f);
        }
        else if (set == 3)
        {
            rb2d.velocity = new Vector2(Charactercontrolelr.CCInstance.m_SpriteForward.x * newHorizontalMovement * 0.2f, 0f);
            animator.CrossFadeInFixedTime("slash1", 0f);
        }

    }
    public override void Special_Updateframe(Rigidbody2D rb2d, float newHorizontalMovement, Animator animator, int set, SkeletonAnimation animation)
    {
        if (set == 1)
        {
            chargetime += Time.deltaTime;

            if (chargetime > 1f)
            {
                animator.CrossFadeInFixedTime("idle", 0f);


            }
        }
        else if (set == 2)
        {

        }
        else if (set == 3)
        {

        }
    }

    public override void Air_Firstframe(Rigidbody2D rb2d, float newHorizontalMovement, Animator animator, int set, Transform parent)
    {
        chargetime = 0f;
        if (set == 1)
        {
            rb2d.velocity = new Vector2(Charactercontrolelr.CCInstance.m_SpriteForward.x * newHorizontalMovement, 0f);
            animator.CrossFadeInFixedTime("air_lans", 0f);
            Vector2 power = new Vector2(0f, -30f);
            rb2d.AddForce(power, ForceMode2D.Impulse);

        }
        else if (set == 2)
        {
            rb2d.velocity = new Vector2(Charactercontrolelr.CCInstance.m_SpriteForward.x * newHorizontalMovement * 0.4f, 0f);
            animator.CrossFadeInFixedTime("slash1", 0f);
        }
        else if (set == 3)
        {
            rb2d.velocity = new Vector2(Charactercontrolelr.CCInstance.m_SpriteForward.x * newHorizontalMovement * 0.2f, 0f);
            animator.CrossFadeInFixedTime("slash1", 0f);
        }
    }

    public override void Air_Updateframe(Rigidbody2D rb2d, float newHorizontalMovement, Animator animator, int set)
    {
        if (set == 1)
        {
            if (Charactercontrolelr.CCInstance.CheckForGrounded())
                animator.CrossFadeInFixedTime("air_lans2", 0f);
        }
        else if (set == 2)
        {

        }
        else if (set == 3)
        {

        }
    }
}
