using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mini_Devi_Attack : SceneLinkedSMB<EnemyBehaviour>
{ 
    float timer = 0f;
    public GameObject effect1;
    public Vector3 effecthoise;
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        m_MonoBehaviour.skeletonAnimation.AnimationState.SetAnimation(0, "MiniDevi_attack", true);
        m_MonoBehaviour.Effectmaker(effect1, effecthoise);
    }

    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;

        // We do this explicitly here instead of in the enemy class, that allow to handle obstacle differently according to state
        // (e.g. look at the ChomperRunToTargetSMB that stop the pursuit if there is an obstacle) 
        float dist = m_MonoBehaviour.speed;
        //if (m_MonoBehaviour.CheckForObstacle(dist))
        {

        }
        if (timer > 1.3f)
            m_MonoBehaviour.setidle();

        //m_MonoBehaviour.ScanForPlayer();
        Debug.Log("attack3");
    }



    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.EffectDes(effect1);
        timer = 0f;
    }
}
