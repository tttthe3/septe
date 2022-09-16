using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_slash12 : SceneLinkedSMB<Charactercontrolelr>
{
    int m_HashAirborneMeleeAttackState = Animator.StringToHash("slash1");
    float timer;
    public GameObject effect1;
    public GameObject effect2;
    public Vector3 effecthoise;
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //m_MonoBehaviour.Effectmaker2(effect1, effecthoise);
        //EffectMaker.CCInstance.Initeffect_Slash(effect2);
        EffectMaker.CCInstance.Initeffect(effect1, m_MonoBehaviour.GetPos()); ;
        m_MonoBehaviour.skeletonAnimation.timeScale = 1.6f;
        m_MonoBehaviour.skeletonAnimation.AnimationState.SetAnimation(0, "slash_movie2", false);
        timer = 0f;

       
    }
    public override void OnSLStatePostEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //m_MonoBehaviour.ForceNotHoldingGun();
        //m_MonoBehaviour.EnableMeleeAttack();
        m_MonoBehaviour.DelaySetHorizontalMovement(120f * 0.2f * 1f,0.5f);
        

    }

    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


        timer += Time.deltaTime;

        if (timer > 1f)
            m_MonoBehaviour.runtoidle();
    }

    public override void OnSLStatePreExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        timer = 0f;
       // EffectMaker.CCInstance.DestroyEffect(effect2);
        // m_MonoBehaviour.skeletonAnimation.timeScale = 1f;
        //  m_MonoBehaviour.DisableMeleeAttack();
        //m_MonoBehaviour.spineAnimation.AnimationState.SetAnimation(0, "nurt", true);
       

    }



}

