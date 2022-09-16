using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class run : SceneLinkedSMB<Charactercontrolelr>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.skeletonAnimation.timeScale = 1.1f;
       
    }
    public override void OnSLStatePostEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.skeletonAnimation.timeScale = 0.9f;
        //m_MonoBehaviour.skeletonAnimation.AnimationState.SetAnimation(0, "idle_none", true);
        m_MonoBehaviour.runset();
    }
    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.UpdateFacing();

        m_MonoBehaviour.GroundedHorizontalMovement(true, 1.4f);
        // m_MonoBehaviour.GroundedVerticalMovement();
        //if (m_MonoBehaviour.checkchangelocomotion() == 0f)
        //   m_MonoBehaviour.Locomotionchange();
        //m_MonoBehaviour.PlayFootstep();
        m_MonoBehaviour.runtoidle();
        if (m_MonoBehaviour.CheckForJumpInput())
        {

            m_MonoBehaviour.SetVerticalMovement(m_MonoBehaviour.jumpspeed * 2f);
            m_MonoBehaviour.jumpup();
        }

        
        
        else if (m_MonoBehaviour.CheckForMeleeAttackInput())
            m_MonoBehaviour.MeleeAttack();

        if (m_MonoBehaviour.Getdash())
        {

            m_MonoBehaviour.Dashmove();
        }

       

       // m_MonoBehaviour.powerslope();
        if (m_MonoBehaviour.IsFalling() && !m_MonoBehaviour.CheckForGrounded())
        {
            Debug.Log("fall");
            m_MonoBehaviour.Jumpdown();
        }

       // m_MonoBehaviour.grapplestart();
        if (m_MonoBehaviour.CheckLeftskillInput())
        {
            m_MonoBehaviour.ItemfirstframeLeft();
        }
    }
    public override void OnSLStatePreExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
