using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slash2 : SceneLinkedSMB<Charactercontrolelr>
{
    float timer = 0f;
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.skeletonAnimation.timeScale = 1f;
        m_MonoBehaviour.skeletonAnimation.AnimationState.SetAnimation(0, "jumpup", false);
        timer = 0f;


    }

    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //m_MonoBehaviour.chainstopper();

        //timer += Time.deltaTime;
        //if (timer >0.03f)
       // {
        //    m_MonoBehaviour.relongchain();
            timer = 0f;
        // }
       // if (m_MonoBehaviour.LeftChecks())
        m_MonoBehaviour.ItemupdateframeLeft();
       // else
            m_MonoBehaviour.ItemupdateframeRight();

        if ( !m_MonoBehaviour.CheckForGrounded())
        {
            
        }
    }


    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.ItemEndframe();
        //m_MonoBehaviour.pickreset();

    }
}
