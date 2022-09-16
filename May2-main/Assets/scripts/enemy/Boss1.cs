using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BTAI;
using UnityEngine.Events;
using System;
using Spine;
using Spine.Unity;
using DG.Tweening;
using UnityEngine.Playables;

public class Boss1 : MonoBehaviour
#if UNITY_EDITOR
    , BTAI.IBTDebugable
#endif
{
    [System.Serializable]
    public class BossRound
    {
        public float platformSpeed = 1;
        
        public GameObject[] enableOnProgress;
        public int bossHP = 15;
        public int shieldHP = 10;
    }
    
    public Transform target;
    public Animator anima;
    public Playable director;
    public int laserStrikeCount = 2;
    public float laserTrackingSpeed = 30.0f;
    public float delay = 2;
    public float beamDelay, grenadeDelay, lightningDelay, cleanupDelay, deathDelay;
    static protected Boss1 s_boss1;
    static public Boss1 BossInstance { get { return s_boss1; } }
    public SkeletonAnimation skeletonAnimation;
    public GameObject tukkleeffects;
    public Rigidbody2D rb2d;
    public Damagerable damageable;
    public float lightningTime = 1;
    public Vector2 grenadeLaunchVelocity;
    [Space]
    public BossRound[] rounds;
    [Space]
    public GameObject[] disableOnDeath;
    public UnityEvent onDefeated;
  
    [Header("Audio")]
    public AudioClip bossDeathClip;
    public AudioClip playerDeathClip;
    public AudioClip postBossClip;
    public AudioClip bossMusic;
    
    [Space]
    public AudioSource roundDeathSource;
    public AudioClip startRound2Clip;
    public AudioClip startRound3Clip;
    public AudioClip deathClip;


    public AudioSource punchsound;
    public AudioSource backsound;
    public bool special = false;
    [Header("UI")]
    public RectTransform Bosscanvas;
    public Slider healthSlider;
    //public Slider healthSlider2;
    //public Slider shieldSlider;

    bool onFloor = false;
    int round = 0;

    bool tukkuketrigger = false;

    Vector3 tcurrent;
    private float tukkletimer = 0f;

    private int m_TotalHealth = 0;
    private int m_CurrentHealth = 0;
    protected Vector2 m_SpriteForward;
    private bool damageshake = false;
    public Transform leftpos;
    public Transform rightpos;
    private float shakercounter = 0f;
    //used to track target movement, to correct for it.
    private Vector2 m_PreviousTargetPosition;
    public bool spriteOriginalFaceLeft;

    public Transform tukkeleft;
    public Transform tukkleright;

    public GameObject punch;
    public GameObject stamp;
    public Transform punchpos;
    public Transform stamppos;
    public DirectorTtrigger direc;

    void start()
    {
        
        s_boss1 = this;
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        rb2d.GetComponent<Rigidbody2D>();
    }

    public void SetEllenFloor(bool onFloor)
    {
        this.onFloor = onFloor;
    }

    public Animator animator;
    Root ai;
    Vector3 originShieldScale;



    void OnEnable()
    {
        SceneLinkedSMB<Boss1>.Initialise(animator, this);
        if (Charactercontrolelr.CCInstance != null)
        {
            Charactercontrolelr.CCInstance.damageable.OnDie.AddListener(PlayerDied);
        }
       
        animator = GetComponent<Animator>();

        round = 0;

        ai = BT.Root();
        ai.OpenBranch(
            //First Round
            BT.Repeat(rounds.Length).OpenBranch(
                BT.Call(NextRound),
                //grenade enabled is true only on 2 and 3 round, so allow to just test if it's the 1st round or not here
                BT.If(GrenadeEnabled).OpenBranch(
                    BT.AniChange(animator, "cut")
                    ),
                BT.Wait(9f),
                BT.Wait(delay),
                    BT.While(tailhappen).OpenBranch(
                    BT.RandomSequence(new int[] { 1, 1, 1 }).OpenBranch(
                        BT.Root().OpenBranch(
                            BT.AniChange(animator, "punch"),
                            BT.Call(effectpunch),
                            BT.Wait(2f),
                            BT.AniChange(animator, "idle"),
                            BT.Wait(2f)
                            ),
                        BT.Repeat(laserStrikeCount).OpenBranch(
                            BT.AniChange(animator, "kick"),
                            BT.Wait(beamDelay),
                            BT.Wait(delay),
                            BT.AniChange(animator, "idle"),
                            BT.Wait(2f)
                        ),
                        BT.Root().OpenBranch(
                            BT.AniChange(animator, "tukkle"),
                            BT.Wait(lightningDelay),
                            BT.Wait(lightningTime),
                            BT.Wait(delay),
                            BT.AniChange(animator, "idle"),
                            BT.Wait(2f)
                        )         


                    ) //行動ループ
                 //トリガー
                ),
                BT.AniChange(animator,"down"),
                 BT.Wait(1f),
                    BT.Call(movies),
                    
                BT.Wait(5f),
                BT.While(tailhappen2).OpenBranch(
                    BT.RandomSequence(new int[] { 1, 1, 3}).OpenBranch(
                        BT.Root().OpenBranch(
                            BT.AniChange(animator, "punch"),
                            BT.Wait(2f),
                            BT.AniChange(animator, "idle"),
                            BT.Wait(2f)

                            ),
                        BT.Repeat(laserStrikeCount).OpenBranch(
                            BT.AniChange(animator, "backattack"),
                            BT.Call(effectstamp),
                            BT.Wait(2f),
                            BT.AniChange(animator, "punch"),
                            BT.Call(FireLaser)
                            
                            
                        ),
                        BT.Root().OpenBranch(
                            BT.AniChange(animator, "tukkle"),
                            BT.Wait(lightningDelay),
                            BT.Wait(lightningTime),
                            BT.Wait(4f),
                            BT.AniChange(animator, "idle"),
                            BT.Wait(2f)
                        )
                      
                        
                    )
                )

            ),
            BT.Trigger(animator, "dead"),
            BT.SetActive(damageable.gameObject, false),
            BT.Wait(cleanupDelay),
            BT.Call(Cleanup),
            BT.Wait(deathDelay),
            BT.Call(Die),
            BT.Terminate()
        );


        //we aggregate the total health to set the slider to the proper value
        //(as the boss is actually "killed" every round and regenerated, we can't use directly its current health)
        for (int i = 0; i < rounds.Length; ++i)
        {
            m_TotalHealth += rounds[i].bossHP;
        }
        m_CurrentHealth = m_TotalHealth;

        healthSlider.maxValue = m_TotalHealth;
        healthSlider.value = m_TotalHealth;
        //healthSlider2.maxValue = m_TotalHealth;
        //healthSlider2.value = m_TotalHealth;

        if (target != null)
            m_PreviousTargetPosition = target.transform.position;

        Debug.Log(round);
    }

    private void OnDisable()
    {
        if (Charactercontrolelr.CCInstance != null)
        {
            Charactercontrolelr.CCInstance.damageable.OnDie.RemoveListener(PlayerDied);
        }
    }

    void PlayerDied(Damager d, Damagerable da)
    {
        //BackgroundMusicPlayer.Instance.PushClip(playerDeathClip);
    }

    private bool Specialattack()
    {

        return special;
    }

    private void movies()
    {
        direc.playmovie();
    }
   

    public void UpdateFacing()
    {
        bool faceLeft = rb2d.transform.position.x- target.transform.position.x > 0f;
        bool faceRight = rb2d.transform.position.x - target.transform.position.x < 0f;

        if (faceLeft)
        {
            SetFacingData(1);
        }
        else if (faceRight)
        {
            SetFacingData(-1);
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

    void FireLaser()
    {
       // var p = Instantiate(projectile);
       
        //p.transform.position = beamLaser.transform.position;
       // p.initialForce = new Vector3(dir.x, dir.y) * 1000;
    }

    void ThrowGrenade()
    {
       

       // var p = Instantiate(grenade);
       // p.transform.position = grenadeSpawnPoint.position;
       // p.initialForce = grenadeLaunchVelocity;
    }

    bool GrenadeEnabled()
    {
        Debug.Log(damageable.CurrentHealth);
        return round >= 1;
    }

    public void tukkle()
    {
        Vector3 current = rb2d.transform.position;
        
        Vector2 power = new Vector3(200f * (target.transform.position.x - rb2d.transform.position.x), 0f);
        //rb2d.transform.position = Vector3.Lerp(current, target.transform.position, Time.time*5f/100f);
        if (power.x < 0f)
        {
            float x = Mathf.Lerp(current.x, leftpos.position.x, Time.deltaTime * 3f);
            //rb2d.transform.position = new Vector3(x, rb2d.transform.position.y, rb2d.transform.position.z);
        }
        else
        {
            float x = Mathf.Lerp(current.x, rightpos.position.x, Time.deltaTime * 3f);
            //rb2d.transform.position = new Vector3(x, rb2d.transform.position.y, rb2d.transform.position.z);
        }
    }
    

    void DeactivateLighting()
    {
        //lightingAttackAudioPlayer.Stop();
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            m_PreviousTargetPosition = target.position;
        }
    }

    void Update()
    {
        ai.Tick();

        if (target != null)
        {
            Vector2 targetMovement = (Vector2)target.position - m_PreviousTargetPosition;
            targetMovement.Normalize();
            Vector3 targetPos = target.position + Vector3.up * (1.0f + targetMovement.y * 0.5f);

           
        }

        if (damageshake) {
            shakercounter += Time.deltaTime;
            damageshaker();
            if (shakercounter > 0.8f)
            {
                damageshake = false;
                shakercounter = 0f;
            }
        }

    }

    void Cleanup()
    {
        //shieldSlider.GetComponent<Animator>().Play("BossShieldDefeat");
        //healthSlider.GetComponent<Animator>().Play("BossHealthDefeat");

        

        roundDeathSource.clip = deathClip;
        roundDeathSource.loop = false;
        roundDeathSource.Play();

        foreach (var g in disableOnDeath)
            g.SetActive(false);
        
    }

    void Die()
    {
        onDefeated.Invoke();
    }

    bool EllenOnPlatform()
    {
        return !onFloor;
    }

    public void damageshaker()
    {
        float  value1 = Mathf.Clamp(-20f, 0f, 20f);
        float value2 = Mathf.Clamp(-20f, 0f, 20f);
        Vector2 power = new Vector2(value1,value2);
        Vector2 current = rb2d.transform.position;
        Vector2 after = new Vector2(current.x + value1, current.y + value2);
        //this.transform.position = after;
        //rb2d.AddForce(power*10f);
        
    }

    bool EllenOnFloor()
    {
        return onFloor;
    }

    bool IsAlive()
    {
        Debug.Log("aliver");
        var alive = damageable.CurrentHealth > 0;
        return alive;
    }

    bool IsNotAlmostDead()
    {
        var alive = damageable.CurrentHealth > 1;
        return alive;
    }

    public void cameramove()
    {
        CameraShaker.Shake(0.1f,0.4f);
    }

    bool tailhappen()
    {

        return m_CurrentHealth>15;
    }

    bool tailhappen2()
    {

        return m_CurrentHealth <= 15;
    }

    bool alivecheck()
    {

        return m_CurrentHealth > 0;
    }

    public void jumptarget()
    {
       tcurrent = target.transform.position;

    } 

    public void jumpaim() {

        Vector3 current = rb2d.transform.position;
        tukkletimer += Time.deltaTime;

        tukkleeffects.SetActive(true);
        Vector2 power = new Vector3(200f*( target.transform.position.x-rb2d.transform.position.x ), 0f);
        //rb2d.transform.position = Vector3.Lerp(current, target.transform.position, Time.time*5f/100f);
        float x = Mathf.Lerp(current.x, tcurrent.x, Time.deltaTime * 3f);
        if (tukkuketrigger == true)
        {
            tukkleeffects.SetActive(true);
            rb2d.transform.position = new Vector3(x, rb2d.transform.position.y, rb2d.transform.position.z);
            Destroy(tukkleeffects, 2f);
        }
        else
        {
            tukkuketrigger = false;
            tukkletimer = 0f;
        }
    }

    public void tukklechange()
    {
        if (tukkletimer > 0.4f)
        {
            tukkuketrigger = true;
        }
    }

    public void tukklemove()
    {
        Vector3 before = rb2d.transform.position;
        Vector3 targets =  new Vector3(tukkeleft.position.x, before.y, before.z);
        if ((m_PreviousTargetPosition.x - rb2d.position.x) < 0f)
        {
             targets = new Vector3(tukkeleft.position.x, before.y, before.z);
            
        }
        else
        {
            targets = new Vector3(tukkleright.position.x, before.y, before.z);
           
        }
        rb2d.transform.DOMove(targets, 1f);
    }

    public void tukklecall()
    {
        DOVirtual.DelayedCall(2.2f, () => tukklemove());
    }


    void NextRound()
    {

        
        damageable.SetHealth(rounds[round].bossHP);
        damageable.EnableInvulnerability(true);
       
        foreach (var g in rounds[round].enableOnProgress)
        {
            g.SetActive(true);
        }
        round++;

        if (round == 2)
        {
            //roundDeathSource.clip = startRound2Clip;
            //roundDeathSource.loop = true;
            //roundDeathSource.Play();
        }
       
    }

    void Disabled()
    {

    }

    void Enabled()
    {

    }

    public void Damaged(Damager damager, Damagerable damageable)
    {
        damageshake = true;
        m_CurrentHealth -= damager.damage;
        healthSlider.value = m_CurrentHealth;
        //healthSlider2.value = Mathf.MoveTowards(m_CurrentHealth + damager.damage, m_CurrentHealth, Time.deltaTime * 1f);
        //rb2d.transform.localPosition = rb2d.transform.localPosition + new Vector3(Mathf.PerlinNoise(Time.time + 1f,0), Mathf.PerlinNoise(Time.time + 1f,0), Mathf.PerlinNoise(Time.time + 1f,0))*0.2f;
        //rb2d.transform.DOShakePosition(0.1f, 0.5f, 3, 1, false, true);
        //Bosscanvas.DOShakePosition(0.4f, 3f, 5, 1, false, true);
        //Debug.Log("chai");
        Debug.Log(m_CurrentHealth);
    }

    public void ShieldDown()
    {
       // shieldDownAudioPlayer.PlayRandomSound();
        damageable.DisableInvulnerability();
    }

    public void ShieldHit()
    {
       
    }

    public void effectpunch()
    {
        var p= Instantiate(punch);
        p.transform.position = punchpos.position;
        punchsound.PlayDelayed(1.5f);
        DOVirtual.DelayedCall(5f, () => desroyefffect(p));
            }

    public void effectstamp()
    {
        var p = Instantiate(stamp);
        p.transform.position = stamppos.position;
        backsound.PlayDelayed(1.5f);
        DOVirtual.DelayedCall(5f, () => desroyefffect(p));
    }

    public void desroyefffect(GameObject effect)
    {
        Destroy(effect);
    }

    public void PlayStep()
    {
        //stepAudioPlayer.PlayRandomSound();
    }

#if UNITY_EDITOR
    public BTAI.Root GetAIRoot()
    {
        return ai;
    }
#endif
}
