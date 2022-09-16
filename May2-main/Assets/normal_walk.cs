using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normal_walk : SceneLinkedSMB<EnemyBehaviour>
{
    float timer = 0f;
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateEnter(animator, stateInfo, layerIndex);
        m_MonoBehaviour.skeletonAnimation.AnimationState.SetAnimation(0, "normal_walk", true);
        m_MonoBehaviour.OrientToTarget();

    }

    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateNoTransitionUpdate(animator, stateInfo, layerIndex);

        m_MonoBehaviour.CheckTargetStillVisible();
        m_MonoBehaviour.OrientToTarget();

        float amount = m_MonoBehaviour.speed * 2.0f;
        //if (m_MonoBehaviour.CheckForObstacle(amount))
        {

            //m_MonoBehaviour.ForgetTarget();
        }
        //else
        m_MonoBehaviour.GroundedHorizontalMovement();
        m_MonoBehaviour.CheckMeleeAttack();
    }



    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


    }
}
