using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gurd : SceneLinkedSMB<Charactercontrolelr>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.skeletonAnimation.timeScale = 0.9f;
        m_MonoBehaviour.skeletonAnimation.AnimationState.SetAnimation(0, "gurd1", true);

    }

    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.UpdateFacing();



        if (!m_MonoBehaviour.CheckForGurdInput())
            m_MonoBehaviour.runtoidle();


    }
    public override void OnSLStatePreExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
