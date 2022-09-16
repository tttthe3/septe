using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;
public class Boss1_Special : InteractWrapper
{
    float time = 0f;
    private bool boxtrigger = false;
    private Vector3 velocity = Vector3.zero;
    private bool whilechain = false;
    public RaycastHit2D chainbox;
    private Transform m_chaincheck;
    private Vector2 boxsize = new Vector2(0.5f, 30f);
    private Vector2 boxdirec = new Vector2(1f, 0f);
    [SerializeField]
    private LayerMask m_boxtype;
    public override void FixedUpdate()
    {
        if (m_chaincheck == null)
            m_chaincheck = GameObject.Find("Player").transform;

        if (Charactercontrolelr.CCInstance.m_SpriteForward.x > 0f)
            boxdirec = new Vector2(1f, 0f);
        else
            boxdirec = new Vector2(-1f, 0f);
        Debug.Log(m_chaincheck);
        chainbox = Physics2D.BoxCast(m_chaincheck.position, boxsize, 0f, boxdirec, 7f, m_boxtype);
        if (chainbox)
        {
            boxtrigger = true;

        }
        else
            boxtrigger = false;
    }

    public override void firstframe(Rigidbody2D rb2d, Animator animator,Transform parent)
    {
        if(boxtrigger)
        animator.CrossFadeInFixedTime("throw", 0f);

    }
    public override void updateframe(Rigidbody2D rb2d, Animator animator, SkeletonAnimation animation)
    {
        time += Time.deltaTime;
        if (time > 1f)
        {
            animator.CrossFadeInFixedTime("idle",0f);
        }

    }

    public override void endframe() { }
}
