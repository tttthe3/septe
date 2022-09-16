using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladder3 : SceneLinkedSMB<Charactercontrolelr>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //m_MonoBehaviour.skeletonAnimation.timeScale = 0.6f;
        m_MonoBehaviour.skeletonAnimation.AnimationState.SetAnimation(0, "ladder3", true);

    }
    public override void OnSLStatePostEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.skeletonAnimation.timeScale = 0.6f;

    }


    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.ladderchange();
        m_MonoBehaviour.ladderidle();
    }
    public override void OnSLStatePreExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
