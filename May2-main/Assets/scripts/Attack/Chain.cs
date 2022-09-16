using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Chain : Itemwrapper
{
   
    private bool lefttrigger=false;
    private bool whilechain = false;
    public RaycastHit2D chainbox;
    private int move = 0;
    private Transform m_chaincheck;
    private Vector2 boxsize = new Vector2(0.5f, 30f);
    private Vector2 boxdirec = new Vector2(1f, 0f);
    [SerializeField]
    private LayerMask m_boxtype;
    [SerializeField]
    GameObject grappleing;
    GameObject cara;
    GameObject child;
    GameObject caraFK;
    Rigidbody2D childFKrb2d;
    Rigidbody2D childhandrb2d;
    int pickcout = 0;
    float timer = 0f;
    public Collider2D chainboxs;
    private bool boxtrigger=false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == m_boxtype)
        {
            boxtrigger = true;
        }
        else
            boxtrigger = false;
    }



    public override void firstframeLeft(Rigidbody2D rb2d, Animator animator)
    {
        lefttrigger = true;
        if (m_chaincheck == null)
            m_chaincheck = GameObject.Find("Player").transform;
        Debug.Log(m_chaincheck);
        chainbox = Physics2D.BoxCast(m_chaincheck.position, boxsize, 0f, boxdirec, 25f, m_boxtype);
        if (boxtrigger)
        {
            timer = 0f;
            
            whilechain = true;
            Vector3 hitpoint = new Vector3(chainbox.point.x, chainbox.point.y, 0f);
            cara = Instantiate(grappleing, m_chaincheck.position, grappleing.transform.rotation); //ボタン入力で伸ばし開始
            cara.transform.localScale = new Vector3(1 / 20f, 1f, 1f);
            Vector3 currentplayer = rb2d.transform.position;
            Vector3 direc = hitpoint - currentplayer;
            Quaternion rot = Quaternion.Euler(0f, 0f, -Mathf.Atan2(direc.normalized.x, direc.normalized.y) * Mathf.Rad2Deg + 270f);
            cara.transform.rotation = rot;
            child = cara.transform.Find("chainanchor/hand").gameObject;
            caraFK = cara.transform.Find("chainanchor/FK").gameObject;
            childFKrb2d = child.GetComponent<Rigidbody2D>();
            childhandrb2d = caraFK.GetComponent<Rigidbody2D>();
            childFKrb2d.constraints = RigidbodyConstraints2D.FreezePosition;
            childhandrb2d.constraints = RigidbodyConstraints2D.FreezePosition;
            SpringJoint2D springFK = caraFK.AddComponent<SpringJoint2D>();
            springFK.connectedBody = chainbox.rigidbody;
            animator.CrossFadeInFixedTime("walk", 0f);
            rb2d.velocity = new Vector2(0f, 18f);
            pickcout = 0;
        }
       //chainAudioPlayer.PlayRandomSound();

    }

    public override void FixedUpdate()
    {

        if(m_chaincheck==null)
            m_chaincheck = GameObject.Find("Player").transform;

        if (Charactercontrolelr.CCInstance.m_SpriteForward.x > 0f)
            boxdirec = new Vector2(1f, 0f);
        else
            boxdirec = new Vector2(-1f, 0f);
        Debug.Log(m_chaincheck);
        chainbox = Physics2D.BoxCast(m_chaincheck.position, boxsize, 0f, boxdirec, 25f, m_boxtype);
        if (chainbox)
        {
            boxtrigger = true;

        }
        else
            boxtrigger = false;
    }
    public override void updateframeLeft(Rigidbody2D rb2d,Animator animator)
    {
        if (!lefttrigger)
            return;
        chainbox = Physics2D.BoxCast(m_chaincheck.position, boxsize, 0f, boxdirec, 25f, m_boxtype);
        if (whilechain)
            rb2d.velocity = new Vector2(0f, 0f);

        if (timer > 0.02f) { 
        if (rb2d.velocity.y != 0f && cara.transform.localScale.x < 0.2f)
            rb2d.velocity = new Vector2(0f, 0f);

        cara.transform.localScale = new Vector3(cara.transform.localScale.x + 0.05f, 1f, 1f);
            //chainAudioPlayer.PlayRandomSound();
            Debug.Log(cara.transform.localScale);
            if (cara.transform.localScale.x > 0.8f)
            {
                Debug.Log(pickcout);
                pickcout++;
      
                if (pickcout == 1)
                {
                   
                    Vector3 hitpoints = new Vector3(chainbox.point.x, chainbox.point.y, 0f);
                    Vector3 direc = hitpoints - rb2d.transform.position;
                    Vector3 Power = direc * 7f * (Mathf.Abs(direc.x) + Mathf.Abs(direc.y));
                    Debug.Log(Power);
                    Vector3 maxpower = new Vector3(100f, 100f, 0f);
                    Vector3 minpower = new Vector3(20f, 20f, 0f);
                    Debug.Log(Power);

                    Power.x = Mathf.Clamp(Power.x, -10000f, 10000f);
                    Power.y = Mathf.Clamp(Power.x, 2500f, 3200f);
                    whilechain = false;
                    rb2d.transform.DOMove(chainbox.point,0.3f);
                    //rb2d.AddForce(Power);
                }

                childFKrb2d.constraints = RigidbodyConstraints2D.None;
                childhandrb2d.constraints = RigidbodyConstraints2D.None;

                Vector3 hitpoint = new Vector3(chainbox.point.x, chainbox.point.y, 0f);

                child.transform.position = rb2d.transform.position;
                if (Mathf.Abs(Mathf.Sqrt((hitpoint.x - rb2d.transform.position.x) * (hitpoint.x - rb2d.transform.position.x) + (hitpoint.y - rb2d.transform.position.y) * (hitpoint.y - rb2d.transform.position.y))) < 10f)
                {
                    cara.SetActive(false);
                    animator.CrossFadeInFixedTime("jumpdown", 0f);

                }
            }
            timer = 0f;
           
        }
        timer += Time.deltaTime;
    }

    public override void endframeLeft() {
        if (!lefttrigger)
            return;
        pickcout = 0;
    }


    public override void firstframeRight(Rigidbody2D rb2d, Animator animator)
    {
        lefttrigger = false;
        m_chaincheck = GameObject.Find("Player").transform;
        Debug.Log(m_chaincheck);
        chainbox = Physics2D.BoxCast(m_chaincheck.position, boxsize, 0f, boxdirec, 25f, m_boxtype);
        if (boxtrigger)
        {
            timer = 0f;

            whilechain = true;
            Vector3 hitpoint = new Vector3(chainbox.point.x, chainbox.point.y, 0f);
            cara = Instantiate(grappleing, m_chaincheck.position, grappleing.transform.rotation); //ボタン入力で伸ばし開始
            cara.transform.localScale = new Vector3(1 / 20f, 1f, 1f);
            Vector3 currentplayer = rb2d.transform.position;
            Vector3 direc = hitpoint - currentplayer;
            Quaternion rot = Quaternion.Euler(0f, 0f, -Mathf.Atan2(direc.normalized.x, direc.normalized.y) * Mathf.Rad2Deg + 270f);
            cara.transform.rotation = rot;
            child = cara.transform.Find("chainanchor/hand").gameObject;
            caraFK = cara.transform.Find("chainanchor/FK").gameObject;
            childFKrb2d = child.GetComponent<Rigidbody2D>();
            childhandrb2d = caraFK.GetComponent<Rigidbody2D>();
            childFKrb2d.constraints = RigidbodyConstraints2D.FreezePosition;
            childhandrb2d.constraints = RigidbodyConstraints2D.FreezePosition;
            SpringJoint2D springFK = caraFK.AddComponent<SpringJoint2D>();
            springFK.connectedBody = chainbox.rigidbody;
            animator.CrossFadeInFixedTime("walk", 0f);
            rb2d.velocity = new Vector2(0f, 18f);
            pickcout = 0;
        }
        //chainAudioPlayer.PlayRandomSound();

    }


    public override void updateframeRight(Rigidbody2D rb2d, Animator animator)
    {
        if (lefttrigger)
            return;
        chainbox = Physics2D.BoxCast(m_chaincheck.position, boxsize, 0f, boxdirec, 25f, m_boxtype);
        if (whilechain)
            rb2d.velocity = new Vector2(0f, 0f);

        if (timer > 0.02f)
        {
            if (rb2d.velocity.y != 0f && cara.transform.localScale.x < 0.2f)
                rb2d.velocity = new Vector2(0f, 0f);

            cara.transform.localScale = new Vector3(cara.transform.localScale.x + 0.05f, 1f, 1f);
            //chainAudioPlayer.PlayRandomSound();
            Debug.Log(cara.transform.localScale);
            if (cara.transform.localScale.x > 0.8f)
            {
                Debug.Log(pickcout);
                pickcout++;

                if (pickcout == 1)
                {

                    Vector3 hitpoints = new Vector3(chainbox.point.x, chainbox.point.y, 0f);
                    Vector3 direc = hitpoints - rb2d.transform.position;
                    Vector3 Power = direc * 7f * (Mathf.Abs(direc.x) + Mathf.Abs(direc.y));
                    Debug.Log(Power);
                    Vector3 maxpower = new Vector3(100f, 100f, 0f);
                    Vector3 minpower = new Vector3(20f, 20f, 0f);
                    Debug.Log(Power);

                    Power.x = Mathf.Clamp(Power.x, -10000f, 10000f);
                    Power.y = Mathf.Clamp(Power.x, 2500f, 3200f);
                    whilechain = false;
                    rb2d.transform.DOMove(chainbox.point, 0.3f);
                    //rb2d.AddForce(Power);
                }

                childFKrb2d.constraints = RigidbodyConstraints2D.None;
                childhandrb2d.constraints = RigidbodyConstraints2D.None;

                Vector3 hitpoint = new Vector3(chainbox.point.x, chainbox.point.y, 0f);

                child.transform.position = rb2d.transform.position;
                if (Mathf.Abs(Mathf.Sqrt((hitpoint.x - rb2d.transform.position.x) * (hitpoint.x - rb2d.transform.position.x) + (hitpoint.y - rb2d.transform.position.y) * (hitpoint.y - rb2d.transform.position.y))) < 10f)
                {
                    cara.SetActive(false);
                    animator.CrossFadeInFixedTime("jumpdown", 0f);

                }
            }
            timer = 0f;

        }
        timer += Time.deltaTime;
    }

    public override void endframeRight()
    {
        if (lefttrigger)
            return;
        pickcout = 0;
    }

    public void moves(Rigidbody2D rb2d)
    {

        if (move == 0)
            rb2d.transform.position=new Vector3(10f,0f,0f);
        ++move;
    }
}
