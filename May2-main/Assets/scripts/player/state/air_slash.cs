using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using Spine.Unity;
using Spine;


public class air_slash: SceneLinkedSMB<Charactercontrolelr>
{

    int m_HashAirborneMeleeAttackState = Animator.StringToHash("air_slash2");
    float timer;
    public GameObject effect1;
    public Vector3 effecthoise;
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;
        m_MonoBehaviour.skeletonAnimation.timeScale = 1.4f;
        m_MonoBehaviour.skeletonAnimation.AnimationState.SetAnimation(0, "air_slash2", false);
        //m_MonoBehaviour.DownAirHorizontalMovement(1000f, 1.2f);
        //m_MonoBehaviour.DownAirHorizontalMovement(-5000f, 1.2f);
        m_MonoBehaviour.Effectmaker(effect1, effecthoise);
        //m_MonoBehaviour.AirHorizontalMovement(true, 0.7f);
    }

    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        m_MonoBehaviour.Attackupdatefram_air();

    }

    public override void OnSLStatePreExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.EffectDes(effect1);
        //if (m_MonoBehaviour.checkchangejump() == 2f)
        //  m_MonoBehaviour.JumpChange();
        if (m_MonoBehaviour.IsFalling() && !m_MonoBehaviour.CheckForGrounded())
        {
            Debug.Log("fall");
            m_MonoBehaviour.Jumpdown();
        }

        m_MonoBehaviour.skeletonAnimation.timeScale = 1f;
        //  m_MonoBehaviour.DisableMeleeAttack();
        //m_MonoBehaviour.spineAnimation.AnimationState.SetAnimation(0, "nurt", true);


    }





}
