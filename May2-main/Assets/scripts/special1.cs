using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class special1 : SceneLinkedSMB<Charactercontrolelr>
{

    int m_HashAirborneMeleeAttackState = Animator.StringToHash("special1");
    float timer;

    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("setter2");
       
        timer = 0f;

    }
    public override void OnSLStatePostEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Debug.Log("setter");
    }

    
    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.runtoidle();


    }
}
