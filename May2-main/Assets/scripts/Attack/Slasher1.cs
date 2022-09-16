using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;
using Rewired;
using Rewired.ComponentControls;
public class Slasher1 :MonoBehaviour
{
    public enum combstate {one,two,three }
    public combstate now=combstate.one;
    private Joystick joystick;
    
    private float current;
    public float firsttimer;
    public float secondtimer;
    public float thirdtimer;
    float timer = 0f;
    public float chargetime;
    public RandomAudioPlayer meleeAttackAudioPlayer;
    public RandomAudioPlayer meleeAttackAudioPlayer2;
    public void firstframe(Rigidbody2D rb2d, float newHorizontalMovement,Animator animator,Damager damager)
    {
        
      
        current = 0f;
        if (now == combstate.one)
        {
            Debug.Log(now);
            rb2d.velocity = new Vector2(Charactercontrolelr.CCInstance.m_SpriteForward.x * newHorizontalMovement, 0f);
            animator.CrossFadeInFixedTime("slash1", 0f);
        }
        else if (now == combstate.two)
            rb2d.velocity = new Vector2(Charactercontrolelr.CCInstance.m_SpriteForward.x * newHorizontalMovement * 0.4f, 0f);
        else if (now == combstate.three)
            rb2d.velocity = new Vector2(Charactercontrolelr.CCInstance.m_SpriteForward.x * newHorizontalMovement * 0.2f, 0f);
    }
    public void updateframe(Animator animator,Damager damage) {
        current += Time.deltaTime;
       

        if (now == combstate.one)
        {
            if (current <0.4f)
            {
                //joystick.SetVibration(2f,2f);
                damage.enabled = true;
                if (Playerinput.Instance.Attack.Down)
                {
                    if (current > 0.1f)
                    {
                        meleeAttackAudioPlayer.PlayRandomSound();
                        animator.CrossFadeInFixedTime("slash2", 0f);
                        now = combstate.two;
                        current = 0;
                    }
                    
                }

            }
            else
            {
                //damage.enabled = false;
                damage.enabled=false;
                now = combstate.one;
                //joystick.SetVibration(0f, 0f);
                animator.CrossFadeInFixedTime("idle", 0f);
                current = 0f;
            }
        }

        if (now == combstate.two)
        {
            if (current < 0.4f)
            {
                if (Playerinput.Instance.Attack.Down)
                {
                    if (current > 0.1f)
                    {
                        damage.enabled = true;
                        //animator.CrossFadeInFixedTime("slash4", 0f);
                        now = combstate.three;
                    }
                   
                }

            }
            else
            {
                damage.enabled = false;
                now = combstate.one;
                animator.CrossFadeInFixedTime("idle", 0f);
                current = 0f;
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


       
    public void endframe() { }
    //最初のフレームで呼ばれる関数とループ関数を実装する
    //恒常的に必要な変数は使わないboolやrb2dで完結する

    public void setpos(Rigidbody2D rb2d, float newHorizontalMovement)
    {
        if(now==combstate.one)
        rb2d.velocity = new Vector2(Charactercontrolelr.CCInstance.m_SpriteForward.x * newHorizontalMovement, 0f);
        else if(now == combstate.one)
        rb2d.velocity = new Vector2(Charactercontrolelr.CCInstance.m_SpriteForward.x * newHorizontalMovement*0.4f, 0f);
        else if (now == combstate.three)
         rb2d.velocity = new Vector2(Charactercontrolelr.CCInstance.m_SpriteForward.x * newHorizontalMovement * 0.2f, 0f);
    }
    public void NextAttack(Animator animator)
    {
       
        if (Playerinput.Instance.Attack.Down)
        {
            if (now == combstate.one )
            {
                if (current < 1f&& current > 0.4f)
                {
                    now = combstate.two;
                    animator.CrossFadeInFixedTime("slash2", 0f);
                }
                else
                    current = 2f; //idleに戻す
            }

            if (now == combstate.two)
            {
                if (current < 0.7f)
                {
                    now = combstate.three;
                    animator.CrossFadeInFixedTime("slash3", 0f);
                }
                else
                    current = 2f; //idleに戻す
            }
            if (now == combstate.three)
            {
                current = 2f; //idleに戻す
            }



        }
    }

    public void firstframe_special(Rigidbody2D rb2d, float newHorizontalMovement, Animator animator,int set)
    {
        chargetime = 0f;
        if (set == 1)
        {
            rb2d.velocity = new Vector2(Charactercontrolelr.CCInstance.m_SpriteForward.x * newHorizontalMovement, 0f);
            animator.CrossFadeInFixedTime("Powerslash_wait", 0f);
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

    public void update_special(Rigidbody2D rb2d, float newHorizontalMovement, Animator animator, int set,SkeletonAnimation animation)
    {
        if (set == 1)
        {
            chargetime += Time.deltaTime;
            if (Playerinput.Instance.PowerAttack.Held)
            {
                if (chargetime > 1f)
                {
                    animator.CrossFadeInFixedTime("Powerslash_wait2", 0f);
                    
                }
            }
            else
                animator.CrossFadeInFixedTime("idle", 0f);
        }
        else if (set == 2)
        {
            
        }
        else if (set == 3)
        {
            
        }
    }

    IEnumerator timemargin()
    {
        yield return new WaitForSeconds(0.6f);
    }

    public void firstframe_air(Rigidbody2D rb2d, float newHorizontalMovement, Animator animator, int set)
    {
        chargetime = 0f;
        if (set == 1)
        {
            rb2d.velocity = new Vector2(Charactercontrolelr.CCInstance.m_SpriteForward.x * newHorizontalMovement, 0f);
            animator.CrossFadeInFixedTime("air_slash1", 0f);
            Charactercontrolelr.CCInstance.UpdateFacing();
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

    public void update_air(Rigidbody2D rb2d, float newHorizontalMovement, Animator animator, int set)
    {
        if (set == 1)
        {
            
            //m_MonoBehaviour.UpdateJump();
            timer += Time.deltaTime;
            Charactercontrolelr.CCInstance.loopAirHorizontalMovement(true, 1f);
            //m_MonoBehaviour.AirborneVerticalMovement();

            if (Charactercontrolelr.CCInstance.isWall)
            {
                Charactercontrolelr.CCInstance.wallrefect();
            }

            if (Charactercontrolelr.CCInstance.CheckForGrounded())
            {
                Debug.Log("go");
                Charactercontrolelr.CCInstance.jummptoground();
                Charactercontrolelr.CCInstance.skeletonAnimation.timeScale = 1f;
            }

            //m_MonoBehaviour.getstate().Complete += delegate { m_MonoBehaviour.skeletonAnimation.AnimationState.SetAnimation(0, "jumpdown", false);  } ;
            if (timer > 0.9f)
            {
                Charactercontrolelr.CCInstance.Jumpdown();
                timer = 0f;
            }
        }
        else if (set == 2)
        {

        }
        else if (set == 3)
        {

        }
    }


}
