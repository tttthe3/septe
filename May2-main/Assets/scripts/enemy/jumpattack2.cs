using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpattack2 : SceneLinkedSMB<Boss1>
{
    public ParticleSystem punch;
    float timer = 0f;
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.skeletonAnimation.timeScale = 1.1f;
        m_MonoBehaviour.skeletonAnimation.AnimationState.SetAnimation(0, "punch", false);
        m_MonoBehaviour.jumptarget();

    }
    public override void OnSLStatePostEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.UpdateFacing();
        
    }

    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if (timer <0.4f)
            m_MonoBehaviour.jumpaim();


    }

    public override void OnSLStatePreExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;
        m_MonoBehaviour.skeletonAnimation.timeScale = 1f;


    }





}