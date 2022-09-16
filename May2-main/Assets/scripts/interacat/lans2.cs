using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lans2 : SceneLinkedSMB<Charactercontrolelr>
{
    int m_HashAirborneMeleeAttackState = Animator.StringToHash("slash2");
    float timer;
    public GameObject effect1;
    public Vector3 effecthoise;
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        m_MonoBehaviour.skeletonAnimation.timeScale = 1.0f;
        m_MonoBehaviour.skeletonAnimation.AnimationState.SetAnimation(0, "lans2", false);
        timer = 0f;

    }
    public override void OnSLStatePostEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //m_MonoBehaviour.ForceNotHoldingGun();
        //m_MonoBehaviour.EnableMeleeAttack();
        m_MonoBehaviour.SetHorizontalMovement(10f * 0.2f * 1f);
       // m_MonoBehaviour.Effectmaker(effect1, effecthoise);
    }

    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        m_MonoBehaviour.Attackupdateframe();
        m_MonoBehaviour.SetHorizontalMovement(20f * 0.2f * 1f);

    }

    public override void OnSLStatePreExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        m_MonoBehaviour.skeletonAnimation.timeScale = 1f;

       // m_MonoBehaviour.EffectDes(effect1);

    }

}
