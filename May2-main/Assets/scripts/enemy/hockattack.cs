using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hockattack : SceneLinkedSMB<Boss1>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.skeletonAnimation.timeScale = 0.6f;
        m_MonoBehaviour.skeletonAnimation.AnimationState.SetAnimation(0, "tukkle", false);
        m_MonoBehaviour.UpdateFacing();
        m_MonoBehaviour.jumptarget();
       // m_MonoBehaviour.tukkle();

    }
    public override void OnSLStatePostEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.UpdateFacing();
        m_MonoBehaviour.tukklecall();
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