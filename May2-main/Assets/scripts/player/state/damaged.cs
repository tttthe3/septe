using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damaged: SceneLinkedSMB<Charactercontrolelr>
{
    public GameObject effect1;
    float timer;
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateEnter(animator, stateInfo, layerIndex);

        m_MonoBehaviour.skeletonAnimation.AnimationState.SetAnimation(0, "dameged", false);
        m_MonoBehaviour.damagedMovement(true,1f);
        timer = 0f;
    }

    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (m_MonoBehaviour.IsFalling())
            m_MonoBehaviour.CheckForGrounded();
        m_MonoBehaviour.UpdateJump();

        timer += Time.deltaTime;
        if (timer > 0.4f)
            m_MonoBehaviour.Locomotionchange();
    }

    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.Locomotionchange();
    }
}

