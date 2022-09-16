using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backattack : SceneLinkedSMB<Boss1>
{
    
    public ParticleSystem stamp;
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.skeletonAnimation.timeScale = 1.1f;
        m_MonoBehaviour.skeletonAnimation.AnimationState.SetAnimation(0, "backattack", false);



    }
    public override void OnSLStatePostEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.UpdateFacing();
        
    }

    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        m_MonoBehaviour.tukklechange();
        m_MonoBehaviour.jumpaim();

    }

    public override void OnSLStatePreExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        m_MonoBehaviour.skeletonAnimation.timeScale = 1f;


    }





}


