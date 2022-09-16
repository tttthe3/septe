using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Spine.Unity;
using Spine;
using System;
using UnityEngine.UI;
using DG.Tweening;
public class Charactercontrolelr : MonoBehaviour
{
    public Damager slashdamger;

    public enum talkstate { non, talk };
    private bool talktriger = false;
    private talkmanager talkmanager;
    private talkstate state;
    
    Vector3 playpos;
    static protected Charactercontrolelr s_charactercotroller;
    GameObject cara;
    private bool leftitemtrigger;
    protected float m_TanHurtJumpAngle;
    public float totalhealth;
    static public Charactercontrolelr CCInstance { get { return s_charactercotroller; } }
    protected Vector2 m_StartingPosition = Vector2.zero;
    protected Coroutine m_FlickerCoroutine;
    Damager dameger;
    public MeshRenderer MeshRenderer;
    public Damagerable damageable;
    protected CheckPoint m_LastCheckpoint = null;
    protected string m_Lastsavepoint = null;
    protected string savepoint_name;
    protected bool m_StartingFacingLeft = false;
    Rigidbody2D rb2d;
    private bool m_faceright;
    private float limit_fallspeed;
    public float maxSpeed = 20f;
    public float jumpspeed = 100f;
    public float gravity = 50f;
    public float jumpAbortSpeedReduction = 100f;
   
    [SerializeField]
    private LayerMask m_groundtype;
    [SerializeField]
    private Transform m_groundcheck;
    [SerializeField]
    private LayerMask m_walltype;
    [SerializeField]
    private LayerMask m_boxtype;
    [SerializeField]
    private Transform m_wallcheck;
    [SerializeField]
    private Transform m_chaincheck;
    [SerializeField]
    private Transform head;
    [SerializeField]
    private Transform body;
    [SerializeField]
    private Transform foot;

    [SerializeField]
    private GameObject bomb;
    [SerializeField]
    private Transform hand;

    private Vector2 boxsize = new Vector2(0.5f, 30f);
    private Vector2 boxdirec = new Vector2(1f, 0f);
    protected const float k_MinHurtJumpAngle = 0.001f;
    protected const float k_MaxHurtJumpAngle = 89.999f;
    private bool ladders;
    private bool while_ladders;

    const float groundradius = 1f;
    const float wallradius = 3f;
    public SkeletonAnimation skeletonAnimation;
    public bool spriteOriginalFaceLeft;
    [SerializeField]
    
    private Animator animator;
    private bool isGround;
    public bool isWall;
    public bool ground;
    public bool jump;
    public bool dash;
    private bool canDash = true;
    private bool isdashing = false;
    private CapsuleCollider2D collider;
    Vector2 wallcheckdirection = Vector2.right;
    Vector2 wallcheckdirectionback = Vector2.left;
    public LayerMask walllayer;
    float wallcheckdis = 2f;
    public bool iswalls;
    private CharacterController2D characterController2D;

    public LayerMask graptype;

    public LayerMask pushtype;
        private bool pusher_check;
    
    public Transform effectpos;
    private GameObject effectprefab;

    GameObject child;
    GameObject caraFK;
    [Range(k_MinHurtJumpAngle, k_MaxHurtJumpAngle)] public float hurtJumpAngle = 45f;
    public Vector2 m_SpriteForward;

    private int pausecount = 0;
    public CanvasGroup Pose;
    public CanvasGroup Levelup;
    public CanvasGroup PlayerPose;
    public Slider health;

    private Vector3 velocity = Vector3.zero;
    
    Vector2 m_previousposition;
    Vector2 m_currentposition;
    Vector2 Nextposition;
    public bool facecheck=false;
    public GameObject effectset1;
    public GameObject effectset2;
    public GameObject effectset3;
    RaycastHit2D heads;
    RaycastHit2D bodys;
    RaycastHit2D foots;
    [SerializeField]
    GameObject grappleing;
    [SerializeField]
    GameObject fuk;
    Rigidbody2D childhandrb2d;
    Rigidbody2D childFKrb2d;
    SpringJoint2D spring;
    bool picker;
    int pickcout = 0;
    public bool saveon;
   
    private string leftattackname;
    private string rightattackname;
    private string downattackname1;
    private string downattackname2;
    private string downattackname3;
    private string PowerAttack;
    public float attackonduration;
    [Serializable]
    public class StagemoveEvent : UnityEvent<Damagerable>
    { }
    
    protected bool inpause;
    protected bool inSave;

    RaycastHit2D chainbox;
    public bool areachange = false;
    private bool boxtrigger = false;
    private bool whilechain = false;
    protected Vector2 m_Movevector;
    
    public Vector2 Velocity { get; protected set; }
    protected const float k_GroundedStickingVelocityMultiplier = 3f;
    Collider2D laddercol;

    public RandomAudioPlayer footstepAudioPlayer;
    public RandomAudioPlayer meleeAttackAudioPlayer;
    public RandomAudioPlayer chainAudioPlayer;
    public RandomAudioPlayer SlideAudioPlayer;
    private bool walltrigger = false;
    //instantiateでaudioの下に追加していく、外した武器はdestroy
    private bool talk = false;
    private void Awake()
    {
        s_charactercotroller = this;
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        m_currentposition = rb2d.position;
        m_previousposition = rb2d.position;
        spring = GetComponent<SpringJoint2D>();
        collider = GetComponent<CapsuleCollider2D>();
        

    }

    public bool jumpstop;

    void Start()
    {
        effectpos.position = rb2d.transform.position;
        talkmanager = GetComponent<talkmanager>();
        state = talkstate.non;
        hurtJumpAngle = Mathf.Clamp(hurtJumpAngle, k_MinHurtJumpAngle, k_MaxHurtJumpAngle);
        m_TanHurtJumpAngle = Mathf.Tan(Mathf.Deg2Rad * hurtJumpAngle);
        playpos = rb2d.position;
        m_SpriteForward = spriteOriginalFaceLeft ? Vector2.left : Vector2.right;
        SceneLinkedSMB<Charactercontrolelr>.Initialise(animator, this);
        characterController2D = GetComponent<CharacterController2D>();
        health.value = damageable.CurrentHealth;
        totalhealth= damageable.CurrentHealth;
    }
    void FixedUpdate()
    {
        playpos = rb2d.position;


        bool wasGrounded = isGround;
        isGround = false;
        isWall = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_groundcheck.position, groundradius, m_groundtype);
        RaycastHit2D hitss = Physics2D.Raycast(m_groundcheck.position, Vector2.down, wallcheckdis * 1f, m_groundtype);

        //for (int i = 0; i < colliders.Length; i++)
        //{
        //   if (colliders[i].gameObject != gameObject)
        //       isGround = true;
    
        if (hitss) { isGround = true; }


        
         RaycastHit2D pusher = Physics2D.Raycast(m_wallcheck.position, m_SpriteForward,  1f, pushtype);

        if (pusher) pusher_check = true;
        else pusher_check = false;
       //RaycastHit2D hit = Physics2D.Raycast(m_wallcheck.position, m_SpriteForward, wallcheckdis, m_walltype);
        //RaycastHit2D hitback = Physics2D.Raycast(m_wallcheck.position, wallcheckdirectionback, wallcheckdis, m_walltype);
        

        if (isGround)
        {

            
        }
        /////////////////////
        RaycastHit2D heads = Physics2D.Raycast(head.position, m_SpriteForward, 1f);
        RaycastHit2D bodys = Physics2D.Raycast(body.position, m_SpriteForward, 1f);
        RaycastHit2D foots = Physics2D.Raycast(foot.position, m_SpriteForward, 1f);



        //////////////////////////////////
        dash = false;

        if (m_SpriteForward.x > 0f)
            boxdirec = new Vector2(1f, 0f);
        else
            boxdirec = new Vector2(-1f, 0f);

        //chainbox = Physics2D.BoxCast(m_chaincheck.position, boxsize, 0f, boxdirec, 25f, m_boxtype);

        if (chainbox)
        {
            boxtrigger = true;

        }
        else
            boxtrigger = false;

        if (!isGround) {
            if (foots && bodys && !heads)
            {
                isGround = true;
            }

            } else if (foots && bodys && heads) {
            walltrigger = true;
            }


        }
    

    void Update()
    {
        if (Playerinput.Instance.Horizontal.Value == 1)
            facecheck = true;
        if(Playerinput.Instance.Horizontal.Value == -1)
            facecheck = false;
        if (state == talkstate.non)
        {
            if (Playerinput.Instance.Pause.Down)
            {
                    Debug.Log("fafea");
                   
                if (!inpause)
                {
                    pausecount++;
                    Playerinput.Instance.ReleaseController(true);
                    Playerinput.Instance.Pause.GainControl();
                    Playerinput.Instance.Select_Hoz.GainControl();
                    Playerinput.Instance.Select_Vert.GainControl();
                    Playerinput.Instance.Skill.GainControl();
                    Playerinput.Instance.Skill2.GainControl();
                    inpause = true;
                    Time.timeScale = 0.0f;
                    PlayerPose.gameObject.SetActive(false);
                    Pose.gameObject.SetActive(true);       //ポーズ画面
                    

                }
                else
                {
                    Unpause();
                }
                
            }

            if (Playerinput.Instance.Intract.Down && saveon)
            {

                if (!inpause)
                {
                    pausecount++;
                    Playerinput.Instance.ReleaseController(true);
                    Playerinput.Instance.Pause.GainControl();
                    Playerinput.Instance.Select_Hoz.GainControl();
                    Playerinput.Instance.Select_Vert.GainControl();
                    Playerinput.Instance.Skill.GainControl();
                    Playerinput.Instance.Skill2.GainControl();
                    inpause = true;
                    Time.timeScale = 0.0f;
                    Levelup.gameObject.SetActive(true);       //ポーズ画面
                    Levelup.alpha = 1f;

                }
                else
                {
                    UnpauseSave();
                }
            }
           


            if (talkmanager.Geconversation() != null
                    && Input.GetKeyDown(KeyCode.Q)            //Qでトーク開始
                    )
            {
                SetState(talkstate.talk);



            }
        }
        else if (state == talkstate.talk) { }

    }

    public void Unpause()
    {
        if (Time.timeScale > 0f)
        {
            return;
        }
        
        StartCoroutine(UnpauseCoroutine());

    }

    public void UnpauseSave()
    {
        if (Time.timeScale > 0f)
        {
            return;
        }

        StartCoroutine(UnSaveCoroutine());

    }

    public bool Gake()
    {
        heads = Physics2D.Raycast(head.position, m_SpriteForward, 1f,graptype);
        bodys = Physics2D.Raycast(body.position, m_SpriteForward, 1f, graptype);
        foots = Physics2D.Raycast(foot.position, m_SpriteForward, 1f, graptype);

        if (heads && !bodys && !foots)
            return true;
        else
            return false;
    }

    public void Gakegrap()
    {
        if (m_SpriteForward.x == 1 && Playerinput.Instance.Horizontal.Value > 0)
        {
            animator.CrossFadeInFixedTime("getitem", 0f);
            rb2d.velocity = new Vector2(0f, 0f);
            rb2d.gravityScale = 0f;
        }
        else if (m_SpriteForward.x == -1 && Playerinput.Instance.Horizontal.Value < 0)
        {
            animator.CrossFadeInFixedTime("getitem", 0f);
            rb2d.velocity = new Vector2(0f, 0f);
            rb2d.gravityScale = 0f;
        }
    }
    public Vector2 Getvelocity()
    {
        return rb2d.velocity;
    }

    public void Gakemove()
    {
        //if (bodys.collider.gameObject.GetComponent<climbobject>())
        {
            rb2d.velocity = new Vector2(0f, 0f);
            rb2d.transform.position = heads.collider.gameObject.transform.position;
            
        }
    }
    
    public void Gakeremove()
    {
        rb2d.gravityScale = 6f;
    }

    bool samples=false;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "StageChange")
            rb2d.position = Vector2.zero;
        areachange = true;

        if (other.gameObject.tag == "talktrigger")
            talktriger = true;

        laddercol = other;

        if (other.gameObject.tag == "Water"&&!samples)
        {
            Event_Maker.Instance.callevent("tiya1");
            samples = true;
            Debug.Log("call");
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "laddr")
            ladders = true;
        if (other.gameObject.tag == "Character")
        {
            talk = true;
        }
        if (other.gameObject.tag == "SavePoint")
        {
            saveon = true;
        }
        else
            saveon = false;


    }

    public void jumpgrud()
    {
        if(Math.Abs(rb2d.velocity.y) < 0.1f)
        {
            rb2d.velocity = new Vector2(transform.localScale.x * maxSpeed * Playerinput.Instance.Horizontal.Value * 0.00f, -0.8f);
        }
    }

    public void Airwall()
    {
        RaycastHit2D hit = Physics2D.Raycast(m_wallcheck.position, m_SpriteForward, wallcheckdis, m_walltype);
        if (hit)
            iswalls = true;
        else
            iswalls = false;

        RaycastHit2D colliders = chainbox = Physics2D.BoxCast(m_wallcheck.position, new Vector2(1f, 1f), 0f, boxdirec, 1f, m_walltype);
        if (colliders)
        {
            iswalls = true;
        }
    }

    protected IEnumerator UnpauseCoroutine()
    {

        Time.timeScale = 1f;
        // UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Posemenu");


        Pose.gameObject.SetActive(false);
        PlayerPose.gameObject.SetActive(true);
        Playerinput.Instance.GainControl();
        //we have to wait for a fixed update so the pause button state change, otherwise we can get in case were the update ポーズ解除
        //of this script happen BEFORE the input is updated, leading to setting the game in pause once again
        yield return new WaitForFixedUpdate();
        yield return new WaitForEndOfFrame();
        inpause = false;
    }

    protected IEnumerator UnSaveCoroutine()
    {

        Time.timeScale = 1f;
        // UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Posemenu");


        Levelup.gameObject.SetActive(false);
        Levelup.alpha = 1f;
        Playerinput.Instance.GainControl();
        //we have to wait for a fixed update so the pause button state change, otherwise we can get in case were the update ポーズ解除
        //of this script happen BEFORE the input is updated, leading to setting the game in pause once again
        yield return new WaitForFixedUpdate();
        yield return new WaitForEndOfFrame();
        inpause = false;
    }

    public void effectstamp()
    {
        var p = Instantiate(effectset1);
        p.transform.position = rb2d.position;
        DOVirtual.DelayedCall(5f, () => desroyefffect(p));
    }

    public Transform GetPos()
    {
        return rb2d.transform;
    }

    public void desroyefffect(GameObject effect)
    {
        Destroy(effect);
    }

    public void updateface(bool faceLeft)
    {

        bool faceleft = Playerinput.Instance.Horizontal.Value < -0.1f;
        bool faceright = Playerinput.Instance.Horizontal.Value > 0.1f;

        if (faceleft)
        {
            skeletonAnimation.skeleton.FlipX = true;
        }

        if (faceright)
        {
            skeletonAnimation.skeleton.FlipX = true;

        }

    }

    public void updateface2()
    {

        bool faceleft = Playerinput.Instance.Horizontal.Value < -0.1f;
        bool faceright = Playerinput.Instance.Horizontal.Value > 0.1f;

        if (Playerinput.Instance.Horizontal.Value < -0.1f)
        {
            skeletonAnimation.skeleton.FlipX = true;
        }

        if (Playerinput.Instance.Horizontal.Value > 0.1f)
        {
            skeletonAnimation.skeleton.FlipX = true;

        }

    }

    public void TurnLeft()
    {
        Debug.Log("turn");
        skeletonAnimation.skeleton.FlipX = false;
    }

    public void UpdateFacing()
    {
        bool faceLeft = rb2d.velocity.x > 0.05f;
        bool faceRight = rb2d.velocity.x < -0.05f;

        if (faceLeft)
        {
            SetFacingData(-1);
        }
        else if (faceRight)
        {
            SetFacingData(1);
        }
    }

    public void SetFacingData(int facing)
    {
        if (facing == 1)
        {
            skeletonAnimation.skeleton.FlipX = spriteOriginalFaceLeft;
            m_SpriteForward = spriteOriginalFaceLeft ? Vector2.right : Vector2.left;
        }
        else if (facing == -1)
        {
            skeletonAnimation.skeleton.FlipX = !spriteOriginalFaceLeft;
            m_SpriteForward = spriteOriginalFaceLeft ? Vector2.left : Vector2.right;
        }
    }


    public void GroundedHorizontalMovement(bool useInput, float speedScale = 1f)
    {
        Vector3 targetvelocity = new Vector2(Playerinput.Instance.Horizontal.Value * 34f, rb2d.velocity.y);
        rb2d.velocity = Vector3.SmoothDamp(rb2d.velocity, targetvelocity, ref velocity, 0.05f);
        //rb2d.gravityScale = 6f;
        //rb2d.velocity = new Vector2(transform.localScale.x * maxSpeed*Playerinput.Instance.Horizontal.Value*speedScale, 0);　地面移動
    }

    public void AirHorizontalMovement(bool useInput, float speedScale = 1f)
    {

        Vector2 force = new Vector2(0f, 1400f * speedScale);
        rb2d.AddForce(force);
    }

    public void damagedMovement(bool useInput, float speedScale = 1f)
    {
        Debug.Log("up");
        Vector2 force = GetHurtDirection();
        rb2d.AddForce(force);
    }

    public void loopAirHorizontalMovement(bool useInput, float speedScale = 1f)
    {
        rb2d.velocity = new Vector2(transform.localScale.x * maxSpeed * Playerinput.Instance.Horizontal.Value * speedScale, rb2d.velocity.y);

    }

    public void DownAirHorizontalMovement(float downpowert, float speedScale = 1f)
    {
        Vector2 force = new Vector2(0, downpowert);
        rb2d.AddForce(force);
    }

    public void SetHorizontalMovement(float newHorizontalMovement)
    {
        rb2d.velocity = new Vector2(m_SpriteForward.x * newHorizontalMovement, 0f);
    }

    public void DelaySetHorizontalMovement(float newHorizontalMovement,float delay)
    {
        DOVirtual.DelayedCall(delay, () => SetHorizontalMovement(newHorizontalMovement), false);
    }

    public void Dashmove()
    {
        //SlideAudioPlayer.PlayRandomSound();

        if (dash && canDash)
        {

            animator.CrossFadeInFixedTime("slide", 0f);
            SlideAudioPlayer.PlayRandomSound();
            StartCoroutine(Dashcool());
            damageable.dashinvul();
        }
        if (isdashing)
        {

            rb2d.velocity = new Vector2(m_SpriteForward.x * transform.localScale.x * 3 * maxSpeed, 0);
        }

    }

    public void Attackfirstframe()
    {
        AttackWrapper.Instance.firstframe(rb2d,3f,animator,this.gameObject.transform);
    }

    public void Attackupdateframe()
    {
        AttackWrapper.Instance.updateframe(animator);
    }
    public void Attackfirstframe_air()
    {
        AttackWrapper.Instance.Air_firstframe(rb2d,2f ,animator,1,this.gameObject.transform);
    }
    public void Attackupdatefram_air()
    {
        AttackWrapper.Instance.Air_updateframe(rb2d, 2f, animator, 1);
    }

    public void idleset()
    {
        
        {
            skeletonAnimation.AnimationState.SetAnimation(0, "idle_none", true);
        }
    }

    public void runset()
    {
       
            skeletonAnimation.AnimationState.SetAnimation(0, "run_none", true);
        
    }

    public void slideset()
    {
       
       
            skeletonAnimation.AnimationState.SetAnimation(0, "slide_none", true);
        
    }

    public void jumpupset()
    {
        
       
            skeletonAnimation.AnimationState.SetAnimation(0, "jumpup_none", true);
        
    }
    public void jumpdownset()
    {
       
       
            skeletonAnimation.AnimationState.SetAnimation(0, "jumpdown_none", true);
        
    }

    public bool LeftChecks()
    {
        return ItemUseManager.Instance.leftChecker();
    }

    public void ItemfirstframeLeft()
    {
      
        ItemUseManager.Instance.FirstframeLeft(rb2d,animator);
    }

    public void ItemupdateframeLeft()
    {
        ItemUseManager.Instance.UpdateframeLeft(rb2d, animator);
    }

    public void ItemEndframe()
    {
        ItemUseManager.Instance.EndframeLeft();
    }

    public void ItemfirstframeRight()
    {

        ItemUseManager.Instance.FirstframeRight(rb2d, animator);
    }

    public void ItemupdateframeRight()
    {
        ItemUseManager.Instance.UpdateframeRight(rb2d, animator);
    }

    public void ItemEndframeRight()
    {
        ItemUseManager.Instance.EndframeRight();
    }

    public bool throwbomb()
    {
        return Playerinput.Instance.Skill2.Down;
        
    }

    public void MeleeAttack()
    {
        
        //ここに武器のenumからstateを変更する起債をする
        Debug.Log(downattackname1);
        animator.CrossFadeInFixedTime(downattackname1, 0f);
        meleeAttackAudioPlayer.PlayRandomSound();
        //Weraponeffect.Instance.MakeAIreffect();
        //Weraponeffect.Instance.MakeAIreffect();
    }

    public void MeleeAttack2() //タイムマージンを入力変数
    {

        //ここに武器のenumからstateを変更する起債をする
        Debug.Log(downattackname2);
        animator.CrossFadeInFixedTime(downattackname2, 0f);
        meleeAttackAudioPlayer.PlayRandomSound();
        //Weraponeffect.Instance.MakeAIreffect();
        //Weraponeffect.Instance.MakeAIreffect();
    }

    public void MeleeAttack3()
    {

        //ここに武器のenumからstateを変更する起債をする
        Debug.Log(downattackname3);
        animator.CrossFadeInFixedTime(downattackname3, 0f);
        meleeAttackAudioPlayer.PlayRandomSound();
        Weraponeffect.Instance.MakeAIreffect();
        //Weraponeffect.Instance.MakeAIreffect();
    }

    public void AirMeleeAttack()
    {
        //ここに武器のenumからstateを変更する起債をする
       
        animator.CrossFadeInFixedTime("air_slash1", 0f);
        //Weraponeffect.Instance.MakeAIreffect();

    }

    public void　PowerAttacks()
    {

        AttackWrapper.Instance.special_firstframe(rb2d,2f, animator,1,this.transform);

    }

    public void PowerAttacksupdate()
    {

        AttackWrapper.Instance.special_updateframe(rb2d, 2f, animator, 1,skeletonAnimation);

    }

    public abstract class attacks
    {
        public abstract void infight1();
        public abstract void infight2();
        public abstract void infight3();

    }

    public void gurd()
    {
        animator.CrossFadeInFixedTime("gurd1", 0f);
    }

    public bool CheckForGurdInput() //ここに〇ボタン
    {

        return Input.GetKey(KeyCode.G);
    }


    public bool CheckForMeleeAttackInput() //ここに〇ボタン
    {

        return Playerinput.Instance.Attack.Down;
    }

    public bool CheckForMeleePowerAttackInput() //ここに〇ボタン
    {

        return Playerinput.Instance.PowerAttack.Down;
    }

    public Vector2 GetHurtDirection()
    {
        Vector2 damageDirection = damageable.GetDamageDirection();

        if (damageDirection.y < 0f)
            return new Vector2(Mathf.Sign(damageDirection.x), 0f);

        float y = Mathf.Abs(damageDirection.x) * m_TanHurtJumpAngle;

        return new Vector2(damageDirection.x, y).normalized;
    }

    public bool Getdash()
    {
        if (Playerinput.Instance.Dash.Down)
            dash = true;

        return Playerinput.Instance.Dash.Down;
    }

    IEnumerator Dashcool()
    {
        
        isdashing = true;
        canDash = false;
        yield return new WaitForSeconds(0.1f);
        isdashing = false;
        yield return new WaitForSeconds(0.5f);
        canDash = true;


    }

    public float GetSpriteword()
    {
        return m_SpriteForward.x;
    }

    public void AirborneVerticalMovement()
    {
        if (Mathf.Approximately(rb2d.velocity.y, 0f) || rb2d.velocity.y > 0f)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);

        }
        rb2d.velocity = new Vector2(rb2d.velocity.x, -40f * gravity * Time.deltaTime * Playerinput.Instance.Vertical.Value);
    }

    public void UpdateJump()
    {


        if (Playerinput.Instance.Horizontal.Value != 0f && Mathf.Approximately(rb2d.velocity.x, 0f))
        {
            rb2d.velocity = new Vector3(0f, 0f, 0f);
        }

        if (!Playerinput.Instance.Jump.Held && rb2d.velocity.y > 0.5f)
        {
            rb2d.velocity -= new Vector2(rb2d.velocity.x * 0.6f, jumpAbortSpeedReduction * 0.8f * Time.deltaTime);
        }
    }

    public bool CheckForJumpInput()
    {

        return Playerinput.Instance.Jump.Down;

    }

    public void JumpStopper()
    {
        if (Playerinput.Instance.Horizontal.Value != 0f && Mathf.Approximately(rb2d.velocity.x, 0f))
        {
            rb2d.velocity = new Vector3(0f, 0f, 0f);
        }
    }

    public void SetVerticalMovement(float newVerticalMovement)
    {
        if (Playerinput.Instance.Jump.Down && !Playerinput.Instance.Jump.Held)
            rb2d.AddForce(new Vector2(0f, 0.7f * newVerticalMovement));
        else if (Playerinput.Instance.Jump.Down && Playerinput.Instance.Jump.Held)
            rb2d.AddForce(new Vector2(0f, 1f * newVerticalMovement));


    }

    public void runtoidle()
    {
        if ((Playerinput.Instance.Horizontal.Value == 0f))
            animator.CrossFadeInFixedTime("idle", 0f);
    }
    public void MakeIdle()
    {
        rb2d.velocity = new Vector2(0f,0f);
        animator.CrossFadeInFixedTime("idle", 0f);
       
    }

    public void idletorun()
    {
        if (Playerinput.Instance.Horizontal.Value != 0)
        {
            animator.CrossFadeInFixedTime("run", 0f);
        }
    }

    public void Locomotionchange()
    {

        if (Playerinput.Instance.Horizontal.Value == 0)
        {
            animator.CrossFadeInFixedTime("idle", 0f);
        }
        else if (Playerinput.Instance.Horizontal.Value != 0 || rb2d.velocity.x == 0f)
        {
            animator.CrossFadeInFixedTime("run", 0f);
        }

    }

    public void laddercheck()
    {
        if(!ladders&&!while_ladders)
            if (Input.GetKeyDown(KeyCode.L))
            {
                while_ladders = true;
                rb2d.gravityScale = 0f;
                animator.CrossFadeInFixedTime("ladder3", 0f);
            }
    }

    public void ladderchange()
    {
        if (while_ladders)

        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                skeletonAnimation.AnimationState.SetAnimation(0, "laddr", false);
                rb2d.transform.position = new Vector3(rb2d.transform.position.x, rb2d.transform.position.y + 2f, rb2d.transform.position.z);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                skeletonAnimation.AnimationState.SetAnimation(0, "ladder2", false);
                rb2d.transform.position = new Vector3(rb2d.transform.position.x, rb2d.transform.position.y - 2f, rb2d.transform.position.z);
            }

        }
    }

    public void ladderidle()
    {
        PlatformEffector2D plat = laddercol.GetComponent<PlatformEffector2D>();

        if (plat&&while_ladders)
        {
            Vector2 force = new Vector2(0f, 1000f * 1f);
            rb2d.AddForce(force);
            animator.CrossFadeInFixedTime("idle", 0f);
            while_ladders = false;
            rb2d.gravityScale = 6f;
        }
    }
    
  

    public bool CheckLeftskillInput()
    {
        return Playerinput.Instance.Skill2.Down;
    }

    public bool CheckRightskillInput()
    {
        return Playerinput.Instance.Skill.Down;
    }

    public bool CheckIntractInput()
    {
        return Playerinput.Instance.Intract.Down;
    }

   
   

    public void Actpush()
    {
        Vector3 targetvelocity = new Vector2(Playerinput.Instance.Horizontal.Value * 5f, rb2d.velocity.y);
        rb2d.velocity = Vector3.SmoothDamp(rb2d.velocity, targetvelocity, ref velocity, 0.05f);
        if (!pusher_check)
        {
            animator.CrossFadeInFixedTime("idle", 0f);
        }
    }

    public void intractFirst()
    {
        IntractManager.Instance.Firstframe(rb2d,animator,this.transform);
    }

    public void intractUpdate()
    {
        IntractManager.Instance.Updateframe(rb2d, animator,skeletonAnimation);
    }

    public bool GetAniState(string name)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(name);
    }

    public void Gravi3()
    {
        if (Input.GetKeyDown(KeyCode.V))
            animator.CrossFadeInFixedTime("gravidle", 0f);
    }

    public void  observeobject()
    {
        this.transform.parent = null;
    }

    public void eventanimator()
    {

    }

    public void whilethrow()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            animator.CrossFadeInFixedTime("idle", 0f);
    }



    public void pickreset()
    {
        pickcout = 0;
    }

    public void attackon_check()
    {
        
    }

    public void JumpChange()

    {
        if (rb2d.velocity.y < -1f)
            animator.CrossFadeInFixedTime("jumpdown", 0f);
        else if (rb2d.velocity.y > 1f)
            animator.CrossFadeInFixedTime("jumpup", 0f);

    }

    public void Jumpdown()

    {

        animator.CrossFadeInFixedTime("jumpdown", 0f);

    }

    public void jumpup()
    {
        animator.CrossFadeInFixedTime("jumpup", 0f);
    }

    public float checkchangejump()
    {
        if (rb2d.velocity.y > 0.5f)
            return 2.0f;
        else
            return 0f;
    }

    public void powerslope()
    {
        RaycastHit2D hito = Physics2D.Raycast(m_wallcheck.position, Vector2.right, wallcheckdis * 5f, m_groundtype);

        if (hito && isGround)
        {
            Vector2 power = new Vector2(100f, -400f);
            rb2d.AddForce(power);
            Debug.Log("power");
        }
    }
    public void jummptoground()
    {

        RaycastHit2D hito = Physics2D.Raycast(m_wallcheck.position, Vector2.right, wallcheckdis * 5f);

        if (hito)
        {
            if (rb2d.velocity.x == 0f)
                animator.CrossFadeInFixedTime("idle", 0f);
            else
                animator.CrossFadeInFixedTime("run", 0f);

            return;
        }

        //if (rb2d.velocity.y > -1f)
        {
            Debug.Log(rb2d.velocity);
            if (rb2d.velocity.x == 0f)
                animator.CrossFadeInFixedTime("idle", 0f);
            else
                animator.CrossFadeInFixedTime("run", 0f);
        }
    }

    public bool IsFalling()

    {

        return !isGround && rb2d.velocity.y < -0.3f;

    }

    public bool iswallcheck()
    {
        return iswalls;
    }

    public void wallrefect()
    {
        Debug.Log(m_SpriteForward.x);
        Debug.Log(Playerinput.Instance.Horizontal.Value);
        if ((m_SpriteForward.x == 1 && Playerinput.Instance.Horizontal.Value < 0) || (m_SpriteForward.x == -1 && Playerinput.Instance.Horizontal.Value > 0))
            rb2d.velocity = new Vector2(transform.localScale.x * maxSpeed * Playerinput.Instance.Horizontal.Value * 1, rb2d.velocity.y);
        //rb2d.velocity = new Vector2(0f, rb2d.velocity.y);
    }




    public bool CheckForGrounded()
    {
        bool wasGrounded = ground;
        bool grounded = isGround;

        if (grounded)
        {
            if (!wasGrounded && rb2d.velocity.y < -1.0f)
            {//only play the landing sound if falling "fast" enough (avoid small bump playing the landing sound)
             // landingAudioPlayer.PlayRandomSound(m_CurrentSurface);
            }
        }
        else
            ground = grounded;
        return grounded;
    }

    public void GroundedVerticalMovement()
    {
        rb2d.velocity -= new Vector2(0, gravity * Time.deltaTime);

        if (rb2d.velocity.y < -gravity * Time.deltaTime * k_GroundedStickingVelocityMultiplier)
        {
            rb2d.velocity = new Vector2(0, -gravity * Time.deltaTime * k_GroundedStickingVelocityMultiplier);
        }
    }


    public void OnHurt(Damager damager, Damagerable damageable)
    {
        Debug.Log("damega");
        //if the player don't have control, we shouldn't be able to be hurt as this wouldn't be fair
        if (!Playerinput.Instance.HaveControl)
            return;

        rb2d.velocity = new Vector2(20f, 10f);
       totalhealth -= damager.damage;
        damageable.EnableInvulnerability();
        health.value = totalhealth;
        animator.CrossFadeInFixedTime("dameged", 0f);
        Debug.Log(totalhealth);
        //we only force respawn if helath > 0, otherwise both forceRespawn & Death trigger are set in the animator, messing with each other.
        if (damageable.CurrentHealth > 0 && damager.forceRespawn)
            animator.CrossFadeInFixedTime("slide", 0f);

        // m_Animator.SetBool(m_HashGroundedPara, false);
        // hurtAudioPlayer.PlayRandomSound();

        //if the health is < 0, mean die callback will take care of respawn
        if (damager.forceRespawn && damageable.CurrentHealth > 0)
        {
            StartCoroutine(DieRespawnCoroutine(false, true));
            Debug.Log("failres2");
            Debug.Log(damageable.CurrentHealth);
        }
    }

    IEnumerator DieRespawnCoroutine(bool resetHealth, bool useCheckPoint)
    {
        
        Playerinput.Instance.ReleaseController(true);
       
        yield return new WaitForSeconds(1.0f); //wait one second before respawing
        
        yield return StartCoroutine(ScreenFader.FadeSceneOut(useCheckPoint ? ScreenFader.FadeType.Black : ScreenFader.FadeType.GameOver));
        if (!useCheckPoint)
            yield return new WaitForSeconds(2f);
       
        Respawn(resetHealth, useCheckPoint);
        //yield return new WaitForEndOfFrame();
        yield return null;
        yield return StartCoroutine(ScreenFader.FadeSceneIn());
        Playerinput.Instance.GainControl();
        
    }
    public void StopFlickering()
    {
        StopCoroutine(m_FlickerCoroutine);
        MeshRenderer.enabled = true;

    }

    public void OnDie()
    {
        animator.CrossFadeInFixedTime("dead", 0f);

        StartCoroutine(DieRespawnCoroutine(true, false));
    }
    public void Respawn(bool resetHealth, bool useCheckpoint)
    {
       
        if (resetHealth)
            damageable.SetHealth(damageable.startingHealth);

        //we reset the hurt trigger, as we don't want the player to go back to hurt animation once respawned

        if (m_FlickerCoroutine != null)
        {//we stop flcikering for the same reason
            StopFlickering();
        }

        if (useCheckpoint && m_LastCheckpoint != null)
        {
            updateface(m_LastCheckpoint.respawnFacingLeft);
            GameObjectTeleporter.Teleport(gameObject, m_LastCheckpoint.transform.position);
            Debug.Log("failreport");
        }
        else
        {
            updateface(m_StartingFacingLeft);
            GameObjectTeleporter.Teleport(gameObject, m_StartingPosition);
        }

        Debug.Log("matiga");
    }

    IEnumerator usefultimer()
    {
        yield return new WaitForSeconds(0.9f);

    }


    public void SetChekpoint(CheckPoint checkpoint)
    {
        
        m_LastCheckpoint = checkpoint;
    }

    public void SetSavepoint(string savepoint,string savename)
    {
        savepoint_name = savename;
        m_Lastsavepoint = savepoint;
    }

    public string GetSavepoint1()
    {
        Debug.Log("set");
        return savepoint_name;
        
    }

    public string GetSavepoint2()
    {
        return m_Lastsavepoint;
    }


    public void SetState(talkstate state)
    {
        this.state = state;

        if (state == talkstate.talk)
        {
            rb2d.velocity = Vector2.zero;
            Playerinput.Instance.ReleaseController(true);
            talkmanager.Starttalking();
        }
    }
    public talkstate GetState()
    {
        return state;
    }

    public void PlayFootstep()
    {
        footstepAudioPlayer.PlayRandomSound();
        var footstepPosition = transform.position;
        footstepPosition.z -= 1;
    }


    public void setattack1Aname(string attackname)
    {
        downattackname1 = attackname;
    }

    public void setattack2Aname(string attackname)
    {
        downattackname2 = attackname;
    }

    public void setattack3Aname(string attackname)
    {
        downattackname3 = attackname;
    }

    public void setPowerAname(string attackname)
    {
        PowerAttack = attackname;
    }

    public void Effectmaker(GameObject effect, Vector3 hosei)
    {
        Quaternion angle;
        if (facecheck)
            angle = Quaternion.Euler(0, 0, 0);
        else
            angle = Quaternion.Euler(180, 0, 0);
        Debug.Log(angle);
        hosei = new Vector3(hosei.x * m_SpriteForward.x, hosei.y, hosei.z);
        // effect.transform.localScale = new Vector3(effect.transform.localScale.x * m_SpriteForward.x, effect.transform.localScale.y, effect.transform.localScale.z);

        Vector3 pos = effectpos.transform.position + hosei;
        //effect.transform.position = effectpos.transform.position;
        effectprefab = Instantiate(effect, pos, angle) as GameObject;

        if (facecheck)
            effectprefab.GetComponent<ParticleSystem>().startRotation3D = new Vector3(0f, 180f, 0f);
        else
            effectprefab.GetComponent<ParticleSystem>().startRotation3D = new Vector3(0f, 0f, 0f);
    }

    public void Effectmaker2(GameObject effect, Vector3 hosei)
    {
       
        hosei = new Vector3(hosei.x * m_SpriteForward.x, hosei.y, hosei.z);
        // effect.transform.localScale = new Vector3(effect.transform.localScale.x * m_SpriteForward.x, effect.transform.localScale.y, effect.transform.localScale.z);

        Vector3 pos = effectpos.transform.position + hosei;
        //effect.transform.position = effectpos.transform.position;
       
        effectprefab = Instantiate(effect, pos, Quaternion.identity) as GameObject;
        if (m_SpriteForward.x == 1)
            effectprefab.transform.localScale = new Vector3(-1, 1, 1);
        else if (m_SpriteForward.x == -1)
            effectprefab.transform.localScale = new Vector3(1, 1, 1);

    }


    public void EffectDes(GameObject effect)
    {
        Destroy(effectprefab);
    }

    public void damagetimer(float time)
    {

    }

    public void savejson()
    {
        PersistentDataManager.SaveAllData();
        string json = JsonUtility.ToJson(Charactercontrolelr.CCInstance);
        Debug.Log(json);
       
    }

}


