using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gakeup : SceneLinkedSMB<Charactercontrolelr>
{
    float timer = 0f;
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.skeletonAnimation.timeScale = 0.4f;
        m_MonoBehaviour.skeletonAnimation.AnimationState.SetAnimation(0, "gravidle", true);
        m_MonoBehaviour.AirHorizontalMovement(true, 1.0f);

    }

    public override void OnSLStatePostEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.Gakemove();
    }

    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        //m_MonoBehaviour.Gakemove();
        if (m_MonoBehaviour.CheckForJumpInput())
        {
            
            m_MonoBehaviour.Gakeremove();
            
                m_MonoBehaviour.SetVerticalMovement(m_MonoBehaviour.jumpspeed * 2f);
                m_MonoBehaviour.jumpup();
            
        }
    }
    public override void OnSLStatePreExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;
    }
}
