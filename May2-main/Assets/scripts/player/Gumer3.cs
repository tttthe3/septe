using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gumer3 : SceneLinkedSMB<Charactercontrolelr>
{
    int m_HashAirborneMeleeAttackState = Animator.StringToHash("slash1");
    float timer;

    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        m_MonoBehaviour.skeletonAnimation.timeScale = 0.7f;
        m_MonoBehaviour.skeletonAnimation.AnimationState.SetAnimation(0, "Gumer3", false);
        timer = 0f;



    }
    public override void OnSLStatePostEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //m_MonoBehaviour.ForceNotHoldingGun();
        //m_MonoBehaviour.EnableMeleeAttack();
        m_MonoBehaviour.SetHorizontalMovement(70f * 0.2f * 1f);


    }

    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


        timer += Time.deltaTime;
        //m_MonoBehaviour.GroundedHorizontalMovement(false);

        //if (timer > 1f)            //攻撃ごとに設定
        // m_MonoBehaviour.Locomotionchange();

        //if (m_MonoBehaviour.CheckForMeleeAttackInput())
        // {
        //     m_MonoBehaviour.MeleeAttack2();
        // }
        m_MonoBehaviour.PowerAttacksupdate();

        //  useeffect.isplaying();
    }

    public override void OnSLStatePreExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        timer = 0f;
        // m_MonoBehaviour.skeletonAnimation.timeScale = 1f;
        //  m_MonoBehaviour.DisableMeleeAttack();
        //m_MonoBehaviour.spineAnimation.AnimationState.SetAnimation(0, "nurt", true);


    }

}
