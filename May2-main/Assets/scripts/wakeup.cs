using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wakeup :  SceneLinkedSMB < Charactercontrolelr >
{ 
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.skeletonAnimation.timeScale = 0.3f;
        m_MonoBehaviour.skeletonAnimation.AnimationState.SetAnimation(0, "wake", true);

    }

    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       

    }
    public override void OnSLStatePreExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
