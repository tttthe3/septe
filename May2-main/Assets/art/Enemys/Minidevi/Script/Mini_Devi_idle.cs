using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mini_Devi_idle : SceneLinkedSMB<EnemyBehaviour>
{
    float timer = 0f;
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        //m_MonoBehaviour.skeletonAnimation.AnimationState.SetAnimation(0, "Zon_idle", true);

    }

    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        float dist = m_MonoBehaviour.speed;
        if (m_MonoBehaviour.CheckForObstacle(dist))
        {

        }
        m_MonoBehaviour.ScanForPlayer();
    }



    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


    }
}
