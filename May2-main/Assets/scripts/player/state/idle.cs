using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;


public class idle : SceneLinkedSMB<Charactercontrolelr>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.idleset();
    }
    public override void OnSLStatePostEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.skeletonAnimation.timeScale = 0.6f;
        //m_MonoBehaviour.skeletonAnimation.AnimationState.SetAnimation(0, "idle_none", true);
        m_MonoBehaviour.idleset();
        m_MonoBehaviour.UpdateFacing();
    }


    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        m_MonoBehaviour.GroundedHorizontalMovement(true, 1.4f);
    
        if(m_MonoBehaviour.CheckForGurdInput())
        m_MonoBehaviour.gurd();
        
        m_MonoBehaviour.Gravi3();
        m_MonoBehaviour.idletorun();
        if (m_MonoBehaviour.CheckForJumpInput())
        {

            m_MonoBehaviour.SetVerticalMovement(m_MonoBehaviour.jumpspeed * 2f);
            m_MonoBehaviour.jumpup();
        }
        if (m_MonoBehaviour.IsFalling()&&!m_MonoBehaviour.CheckForGrounded())
        {

            m_MonoBehaviour.Jumpdown();
        }

        //if (m_MonoBehaviour.CheckForMeleeAttackInput())
        //  m_MonoBehaviour.MeleeAttack();
        if (m_MonoBehaviour.CheckForMeleeAttackInput())
            m_MonoBehaviour.Attackfirstframe();

        if (m_MonoBehaviour.CheckForMeleePowerAttackInput())
            m_MonoBehaviour.PowerAttacks();

        if (m_MonoBehaviour.Getdash())
        {
            m_MonoBehaviour.Dashmove();
        }
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

        if (m_MonoBehaviour.CheckIntractInput())
        {
            m_MonoBehaviour.intractFirst();
        }

    }
    public override void OnSLStatePreExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
