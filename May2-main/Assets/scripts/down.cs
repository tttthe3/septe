using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class down : SceneLinkedSMB<Boss1>
{

    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.skeletonAnimation.timeScale = 1.1f;
        m_MonoBehaviour.skeletonAnimation.AnimationState.SetAnimation(0, "down", false);



    }
    public override void OnSLStatePostEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.UpdateFacing();
        
    }

    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {



    }

    public override void OnSLStatePreExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        m_MonoBehaviour.skeletonAnimation.timeScale = 1f;


    }
}

