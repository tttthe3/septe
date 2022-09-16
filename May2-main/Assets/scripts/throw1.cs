using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throw1 : SceneLinkedSMB<Charactercontrolelr>
{
    int m_HashAirborneMeleeAttackState = Animator.StringToHash("throw1");
    float timer;

    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        m_MonoBehaviour.skeletonAnimation.timeScale = 1.4f;
        m_MonoBehaviour.skeletonAnimation.AnimationState.SetAnimation(0, "throw1", false);
        timer = 0f;

    }
    public override void OnSLStatePostEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        m_MonoBehaviour.intractUpdate();
    }

    public override void OnSLStatePreExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {



    }
}
