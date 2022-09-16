using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class air_lans2 : SceneLinkedSMB<Charactercontrolelr>
{
    int m_HashAirborneMeleeAttackState = Animator.StringToHash("air_slash1");
    float timer;

    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;
        m_MonoBehaviour.skeletonAnimation.timeScale = 2f;
        m_MonoBehaviour.skeletonAnimation.AnimationState.SetAnimation(0, "Throw_Lans", false);
        //m_MonoBehaviour.DownAirHorizontalMovement(1000f, 1.2f);
        //m_MonoBehaviour.DownAirHorizontalMovement(-5000f, 1.2f);

    }

    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if (timer > 0.5f)
            m_MonoBehaviour.jummptoground();

    }

    public override void OnSLStatePreExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;
        //if (m_MonoBehaviour.checkchangejump() == 2f)
        //  m_MonoBehaviour.JumpChange();
        if (m_MonoBehaviour.IsFalling() && !m_MonoBehaviour.CheckForGrounded())
        {
            Debug.Log("fall");
            //m_MonoBehaviour.Jumpdown();
        }

        m_MonoBehaviour.skeletonAnimation.timeScale = 1f;
        //  m_MonoBehaviour.DisableMeleeAttack();
        //m_MonoBehaviour.spineAnimation.AnimationState.SetAnimation(0, "nurt", true);


    }
}
