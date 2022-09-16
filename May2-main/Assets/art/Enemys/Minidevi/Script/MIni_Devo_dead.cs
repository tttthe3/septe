using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIni_Devo_dead : SceneLinkedSMB<EnemyBehaviour>
{
    public Material deathhit;
    public GameObject effect1;
    float timer = 0f;
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.DisableDamage();
        m_MonoBehaviour.skeletonAnimation.AnimationState.SetAnimation(0, "MiniDevi_dead", false);
        //m_MonoBehaviour.Effectmaker(effect1);
    }

    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        m_MonoBehaviour.materialchange(deathhit, timer * 2);
        if (timer > 1f)
        {
            m_MonoBehaviour.gameObject.SetActive(false);
        }
    }



    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;

    }
}
