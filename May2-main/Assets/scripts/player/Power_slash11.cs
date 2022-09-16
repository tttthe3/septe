using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_slash11 : SceneLinkedSMB<Charactercontrolelr>
{
 
    int m_HashAirborneMeleeAttackState = Animator.StringToHash("slash1");
    float timer;
    public GameObject effect1;
    public Vector3 effecthoise;
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        m_MonoBehaviour.skeletonAnimation.timeScale = 1.1f;
        m_MonoBehaviour.skeletonAnimation.AnimationState.SetAnimation(0, "BloodSlash2", false);
        timer = 0f;
        //m_MonoBehaviour.Effectmaker2(effect1, effecthoise);
        EffectMaker.CCInstance.Initeffect(effect1, m_MonoBehaviour.GetPos());

    }
    public override void OnSLStatePostEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //m_MonoBehaviour.ForceNotHoldingGun();
        //m_MonoBehaviour.EnableMeleeAttack();
        m_MonoBehaviour.DelaySetHorizontalMovement(20f,0.3f) ;
        

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
        //m_MonoBehaviour.EffectDes(effect1);
        timer = 0f;
        // m_MonoBehaviour.skeletonAnimation.timeScale = 1f;
        //  m_MonoBehaviour.DisableMeleeAttack();
        //m_MonoBehaviour.spineAnimation.AnimationState.SetAnimation(0, "nurt", true);
       

    }



}
