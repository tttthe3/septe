using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpup : SceneLinkedSMB<Charactercontrolelr>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.skeletonAnimation.timeScale = 0.4f;
        //m_MonoBehaviour.skeletonAnimation.AnimationState.SetAnimation(0, "jumpup", false);
        m_MonoBehaviour.AirHorizontalMovement(true, 1.0f);

    }

    public override void OnSLStatePostEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.skeletonAnimation.timeScale = 0.6f;
        //m_MonoBehaviour.skeletonAnimation.AnimationState.SetAnimation(0, "idle_none", true);
        m_MonoBehaviour.jumpupset();
    }

    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.Airwall();
        m_MonoBehaviour.UpdateFacing();

        if (m_MonoBehaviour.iswallcheck()==false)
        {
            //m_MonoBehaviour.UpdateJump();
            m_MonoBehaviour.loopAirHorizontalMovement(true, 1f);
            //m_MonoBehaviour.AirborneVerticalMovement();
        }
        m_MonoBehaviour.jumpgrud();
        if (m_MonoBehaviour.CheckForGrounded())
        {
            Debug.Log("change");
           // m_MonoBehaviour.jummptoground();
            m_MonoBehaviour.skeletonAnimation.timeScale = 1f;
        }
        if (m_MonoBehaviour.iswallcheck())
        {

          m_MonoBehaviour.wallrefect();
        }

        if (m_MonoBehaviour.checkchangejump() == 0f)
            m_MonoBehaviour.JumpChange();
        if (m_MonoBehaviour.CheckForMeleeAttackInput())
        {
            m_MonoBehaviour.Attackfirstframe_air();
        }

        m_MonoBehaviour.JumpStopper();
        if (m_MonoBehaviour.CheckRightskillInput())
        {

           
            {

                m_MonoBehaviour.ItemfirstframeRight();
            }
        }

        //m_MonoBehaviour.grapplestart();
        if (m_MonoBehaviour.CheckLeftskillInput())
        {
           
                    m_MonoBehaviour.ItemfirstframeLeft();
              
        }

        if (m_MonoBehaviour.Gake())
            m_MonoBehaviour.Gakegrap();

        //m_MonoBehaviour.grapplestart();
    }
    public override void OnSLStatePreExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}

