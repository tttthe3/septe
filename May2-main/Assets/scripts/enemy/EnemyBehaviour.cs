using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using System;
using UnityEngine.UI;
using DG.Tweening;
using Spine.Unity;
using Spine;
#endif
public class EnemyBehaviour : MonoBehaviour
{

    　　public DirectorTtrigger special; //ダウン時に再生する
    
        static Collider2D[] s_ColliderCache = new Collider2D[16];
    private bool second;
        public Vector3 moveVector { get { return m_MoveVector; } }
        public Transform Target { get { return m_Target; } }
        public bool facecheck = false;
    [Tooltip("If the sprite face left on the spritesheet, enable this. Otherwise, leave disabled")]
        public bool spriteFaceLeft = false;
        public Transform effectpos;
    [Header("Movement")]
        public float speed;
        public float gravity = 10.0f;
        public Material currentmat;
        [Header("References")]
        [Tooltip("If the enemy will be using ranged attack, set a prefab of the projectile it should use")]
        //public Bullet projectilePrefab;

        [Header("Scanning settings")]
       
        [Range(0.0f, 360.0f)]
        public float viewDirection = 0.0f;
        [Range(0.0f, 360.0f)]
        public float viewFov;
        public float viewDistance;
        [Tooltip("Time in seconds without the target in the view cone before the target is considered lost from sight")]
        public float timeBeforeTargetLost = 3.0f;

        [Header("Melee Attack Data")]
        [EnemyMeleeRangeCheck]
        public float meleeRange = 3.0f;
        public Damager meleeDamager;
        public Damager contactDamager;
        [Tooltip("if true, the enemy will jump/dash forward when it melee attack")]
        public bool attackDash;
        [Tooltip("The force used by the dash")]
        public Vector2 attackForce;
             public LayerMask Ground;
        [Header("Range Attack Data")]
        [Tooltip("From where the projectile are spawned")]
        public Transform shootingOrigin;

        [Header("Audio")]
        public RandomAudioPlayer shootingAudio;
        public RandomAudioPlayer meleeAttackAudio;
        public RandomAudioPlayer dieAudio;
        public RandomAudioPlayer footStepAudio;

        [Header("Misc")]
        [Tooltip("Time in seconds during which the enemy flicker after being hit")]
        public float flickeringDuration;

        protected MeshRenderer m_SpriteRenderer;
        protected CharacterController2D m_CharacterController2D;
        protected Collider2D m_Collider;
        protected Animator m_Animator;

        protected Vector3 m_MoveVector;
        protected Transform m_Target;
        protected Vector3 m_TargetShootPosition;
        protected float m_TimeSinceLastTargetView;
    private Vector3 velocity = Vector3.zero;
    protected float m_FireTimer = 0.0f;
    public float disolvemount = 0;
        //as we flip the sprite instead of rotating/scaling the object, this give the forward vector according to the sprite orientation
        protected Vector2 m_SpriteForward;
        protected Bounds m_LocalBounds;
        protected Vector3 m_LocalDamagerPosition;

        protected RaycastHit2D[] m_RaycastHitCache = new RaycastHit2D[8];
        protected ContactFilter2D m_Filter;

        protected Coroutine m_FlickeringCoroutine = null;
        protected Color m_OriginalColor;
        private GameObject effectprefab;
    public Spine.Unity.SkeletonAnimation skeletonAnimation;

    protected bool m_Dead = false;
    private Rigidbody2D rb2d;
        private bool find=false;
        private bool lost = false;
        private bool Attack = false;
        private bool damaged=false;
        private bool death=false;
        private bool isGround=false;
       
        protected readonly int m_HashShootingPara = Animator.StringToHash("Shooting");
        public string Plusstatename;
       
       
        public bool armers;
        private void Awake()
        {
            m_CharacterController2D = GetComponent<CharacterController2D>();
            m_Collider = GetComponent<Collider2D>();
            m_Animator = GetComponent<Animator>();
            m_SpriteRenderer = GetComponent<MeshRenderer>();

       
        skeletonAnimation = GetComponent<Spine.Unity.SkeletonAnimation>();
        //if (projectilePrefab != null)
        //m_BulletPool = BulletPool.GetObjectPool(projectilePrefab.gameObject, 8);
       
        m_SpriteForward = spriteFaceLeft ? Vector2.left : Vector2.right;
        rb2d = GetComponent<Rigidbody2D>();
       
       
            if (meleeDamager != null)
                EndAttack();
        }

        private void OnEnable()
        {
            if (meleeDamager != null)
                EndAttack();

            m_Dead = false;
            m_Collider.enabled = true;
        }
    public void setSKelAnimation(string name)
    {
        skeletonAnimation.AnimationState.SetAnimation(0, Plusstatename+name, false);
    }
    private void Start()
        {
        
        SceneLinkedSMB<EnemyBehaviour>.Initialise(m_Animator, this);
        //m_OriginalColor = skeletonAnimation.skeleton.GetColor();  //1
        m_LocalBounds = new Bounds();
            

            m_Filter = new ContactFilter2D();
            
            m_Filter.useLayerMask = true;
            m_Filter.useTriggers = false;

            if (meleeDamager)
                m_LocalDamagerPosition = meleeDamager.transform.localPosition;
        }

        void FixedUpdate()
        {
           // if (m_Dead)
             //   return;
             
            m_MoveVector.y = Mathf.Max(m_MoveVector.y - gravity * Time.deltaTime, -gravity);

            

            UpdateTimers();

            //m_Animator.SetBool(m_HashGroundedPara, m_CharacterController2D.IsGrounded);
            isGround = true;
        }

        void UpdateTimers()
        {
            if (m_TimeSinceLastTargetView > 0.0f)
                m_TimeSinceLastTargetView -= Time.deltaTime;

            if (m_FireTimer > 0.0f)
                m_FireTimer -= Time.deltaTime;
        }

        public void SetHorizontalSpeed(float horizontalSpeed)
        {
            m_MoveVector.x = horizontalSpeed * m_SpriteForward.x;
        }

        public bool CheckForObstacle(float forwardDistance)
        {
            //we circle cast with a size sligly small than the collider height. That avoid to collide with very small bump on the ground
            if (Physics2D.CircleCast(m_Collider.bounds.center, m_Collider.bounds.extents.y - 0.7f, m_SpriteForward, forwardDistance, m_Filter.layerMask.value))
            {
                return true;
            }

            Vector3 castingPosition = (Vector2)(transform.position + m_LocalBounds.center) + m_SpriteForward * m_LocalBounds.extents.x;
            Debug.DrawLine(castingPosition, castingPosition + Vector3.down * (m_LocalBounds.extents.y + 0.2f));

            if (!Physics2D.CircleCast(castingPosition, 0.1f, Vector2.down, m_LocalBounds.extents.y + 0.2f, Ground))
            {
                return true;
            }

            return false;
        }

        public void SetFacingData(int facing)
        {
        if (facing == -1)
        {
            facecheck = true;
           // this.transform.localScale = new Vector3( m_SpriteForward.x, 1, 1);
            skeletonAnimation.skeleton.ScaleX = spriteFaceLeft?-1:1;
            m_SpriteForward = spriteFaceLeft ? Vector2.right : Vector2.left;
            
        }
            else if (facing == 1)
            {
            facecheck = false;
            //this.transform.localScale = new Vector3(m_SpriteForward.x, 1, 1);
            skeletonAnimation.skeleton.ScaleX = spriteFaceLeft ? 1 : -1;
            m_SpriteForward = spriteFaceLeft ? Vector2.left : Vector2.right;
            this.transform.localScale = new Vector3(m_SpriteForward.x, 1, 1);
        }
        }

        public void SetMoveVector(Vector2 newMoveVector)
        {
            m_MoveVector = newMoveVector;
        }

    public void GroundedHorizontalMovement( float speedScale = 1f)
    {
        Vector3 targetvelocity = new Vector2(1f * 23f, rb2d.velocity.y);
        //rb2d.velocity = Vector3.SmoothDamp(rb2d.velocity, targetvelocity, ref velocity, 0.05f);
        rb2d.velocity = new Vector2(m_SpriteForward.x* 11f, 0f);
        Debug.Log("walk");
        //rb2d.gravityScale = 6f;
        //rb2d.velocity = new Vector2(transform.localScale.x * maxSpeed*Playerinput.Instance.Horizontal.Value*speedScale, 0);　地面移動
    }

    public void UpdateFacing()
        {
            bool faceLeft = rb2d.velocity.x > 0f;
            bool faceRight = rb2d.velocity.x < 0f;

            if (faceLeft)
            {
                SetFacingData(-1);
            }
            else if (faceRight)
            {
                SetFacingData(1);
            }
        }

        public void ScanForPlayer()
        {
            //If the player don't have control, they can't react, so do not pursue them
            if (!Playerinput.Instance.HaveControl)
                return;
            
            Vector3 dir = Charactercontrolelr.CCInstance.transform.position - transform.position;

            if (dir.sqrMagnitude > viewDistance * viewDistance)
            {
                return;
            }

            Vector3 testForward = Quaternion.Euler(0, 0, spriteFaceLeft ? Mathf.Sign(m_SpriteForward.x) * -viewDirection : Mathf.Sign(m_SpriteForward.x) * viewDirection) * m_SpriteForward;

            float angle = Vector3.Angle(testForward, dir);

            if (angle > viewFov * 0.5f)
            {
               return;
            }

            m_Target = Charactercontrolelr.CCInstance.transform;
            m_TimeSinceLastTargetView = timeBeforeTargetLost;

        find = true;
        m_Animator.CrossFadeInFixedTime(Plusstatename + "walk",0f);
        setSKelAnimation("walk");
    }

        public void OrientToTarget()       //プレイヤーとの距離を計算。m_spriteforwardを再設定
        {
            if (m_Target == null)
                return;

            Vector3 toTarget = m_Target.position - transform.position;

            if (Vector2.Dot(toTarget, m_SpriteForward) < 0)
            {
                SetFacingData(Mathf.RoundToInt(-m_SpriteForward.x));
            }
        }

        public void CheckTargetStillVisible()       //距離を再計算。視野内にいるとき、デフォルトで左向きなりならtestforwardを反転。敵方向と左右で視野角どないなら
        {                                           //タイマーをリセットする。タイマーが0になったら追跡終了
            if (m_Target == null)
                return;

            Vector3 toTarget = m_Target.position - transform.position;

            if (toTarget.sqrMagnitude < viewDistance * viewDistance)
            {
                Vector3 testForward = Quaternion.Euler(0, 0, spriteFaceLeft ? -viewDirection : viewDirection) * m_SpriteForward;
                if (skeletonAnimation.skeleton.FlipX) testForward.x = -testForward.x;

                float angle = Vector3.Angle(testForward, toTarget);

                if (angle <= viewFov * 0.5f)
                {
                    //we reset the timer if the target is at viewing distance.
                    m_TimeSinceLastTargetView = timeBeforeTargetLost;
                }
            }


            if (m_TimeSinceLastTargetView <= 0.0f)
            {
            Debug.Log("forg");
                ForgetTarget();
            }
        }

        public void ForgetTarget()             //ターゲットを消してidleにする。
        {
        //m_Animator.SetTrigger(m_HashTargetLostPara);
        lost = true;
            m_Target = null;
        m_Animator.CrossFadeInFixedTime(Plusstatename + "idle",0f);
        setSKelAnimation("idle");
    }

        //This is used in case where there is a delay between deciding to shoot and shoot (e.g. Spitter that have an animation before spitting)
        //so we shoot where the target was when the animation started, not where it is when the actual shooting happen
        public void RememberTargetPos()
        {
            if (m_Target == null)
                return;

            m_TargetShootPosition = m_Target.transform.position;
        }

        //Call every frame when the enemy is in pursuit to check for range & Trigger the attack if in range
        public void CheckMeleeAttack() //ターゲットが攻撃範囲内なら攻撃に移行。
        {
            if (m_Target == null)
            {//we lost the target, shouldn't shoot
                return;
            }

            if ((m_Target.transform.position - transform.position).sqrMagnitude < (meleeRange * meleeRange))
        {
            Debug.Log("attack");
            //m_Animator.SetTrigger(m_HashMeleeAttackPara);
            Attack = true;
            m_Animator.CrossFadeInFixedTime(Plusstatename+"attack", 0f);
                //meleeAttackAudio.PlayRandomSound();
            }
        }

        //This is called when the damager get enabled (so the enemy can damage the player). Likely be called by the animation throught animation event (see the attack animation of the Chomper)
        public void StartAttack()  //damagerをONする。攻撃による移動
        {
            if (skeletonAnimation.skeleton.FlipX)
                meleeDamager.transform.localPosition = Vector3.Scale(m_LocalDamagerPosition, new Vector3(-1, 1, 1));
            else
                meleeDamager.transform.localPosition = m_LocalDamagerPosition;

            meleeDamager.EnableDamage();
            meleeDamager.gameObject.SetActive(true);
           

        if (attackDash)
        {
            Vector3 moves;
            moves = new Vector2(m_SpriteForward.x * attackForce.x, attackForce.y);
            rb2d.transform.position = rb2d.transform.position + moves; ;
        }
        }

        public void EndAttack() //damagerをOFF
        {
            if (meleeDamager != null)
            {
                meleeDamager.gameObject.SetActive(false);
                meleeDamager.DisableDamage();
            }
        }

        //This is call each update if the enemy is in a attack/shooting state, but the timer will early exit if too early to shoot.
        public void CheckShootingTimer()
        {
            if (m_FireTimer > 0.0f)
            {
                return;
            }

            if (m_Target == null)
            {//we lost the target, shouldn't shoot
                return;
            }

            m_Animator.SetTrigger(m_HashShootingPara);
           // shootingAudio.PlayRandomSound();

            m_FireTimer = 1.0f;
        }

    public void Effectmaker(GameObject effect, Vector3 hosei)
    {
        effectpos.position = rb2d.transform.position;
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

    public void EffectDes(GameObject effect)
    {
        Destroy(effectprefab);
    }

    public void Shooting()
        {
            Vector2 shootPosition = shootingOrigin.transform.localPosition;

            //if we are flipped compared to normal, we need to localy flip the shootposition too
            if ((spriteFaceLeft && m_SpriteForward.x > 0) || (!spriteFaceLeft && m_SpriteForward.x > 0))
                shootPosition.x *= -1;

            //BulletObject obj = m_BulletPool.Pop(shootingOrigin.TransformPoint(shootPosition));

            //shootingAudio.PlayRandomSound();

            //obj.rigidbody2D.velocity = (GetProjectilVelocity(m_TargetShootPosition, shootingOrigin.transform.position));
        }

        //This will give the velocity vector needed to give to the bullet rigidbody so it reach the given target from the origin.
        private Vector3 GetProjectilVelocity(Vector3 target, Vector3 origin)
        {
            const float projectileSpeed = 30.0f;

            Vector3 velocity = Vector3.zero;
            Vector3 toTarget = target - origin;

            float gSquared = Physics.gravity.sqrMagnitude;
            float b = projectileSpeed * projectileSpeed + Vector3.Dot(toTarget, Physics.gravity);
            float discriminant = b * b - gSquared * toTarget.sqrMagnitude;

            // Check whether the target is reachable at max speed or less.
            if (discriminant < 0)
            {
                velocity = toTarget;
                velocity.y = 0;
                velocity.Normalize();
                velocity.y = 0.7f;

                velocity *= projectileSpeed;
                return velocity;
            }

            float discRoot = Mathf.Sqrt(discriminant);

            // Highest
            float T_max = Mathf.Sqrt((b + discRoot) * 2f / gSquared);

            // Lowest speed arc
            float T_lowEnergy = Mathf.Sqrt(Mathf.Sqrt(toTarget.sqrMagnitude * 4f / gSquared));

            // Most direct with max speed
            float T_min = Mathf.Sqrt((b - discRoot) * 2f / gSquared);

            float T = 0;

            // 0 = highest, 1 = lowest, 2 = most direct
            int shotType = 1;

            switch (shotType)
            {
                case 0:
                    T = T_max;
                    break;
                case 1:
                    T = T_lowEnergy;
                    break;
                case 2:
                    T = T_min;
                    break;
                default:
                    break;
            }

            velocity = toTarget / T - Physics.gravity * T / 2f;

            return velocity;
        }

        public void Die(Damager damager, Damagerable damageable)
        {
        setSKelAnimation("dead");
        m_Animator.CrossFadeInFixedTime(Plusstatename + "dead", 0f);
        Vector2 throwVector = new Vector2(0, 2.0f);
            Vector2 damagerToThis = damager.transform.position - transform.position;
        
        throwVector.x = Mathf.Sign(damagerToThis.x) * -4.0f;
            SetMoveVector(throwVector);

        //m_Animator.SetTrigger(m_HashDeathPara);
        death = true;
            //dieAudio.PlayRandomSound();

            m_Dead = true;
            //m_Collider.enabled = false;
        
            //CameraShaker.Shake(0.15f, 0.3f);
        }

        public void Hit(Damager damager, Damagerable damageable)
        {
            if (damageable.CurrentHealth <= 0)
                return;

        
        //m_Animator.SetTrigger(m_HashHitPara);
        damaged = true;
        m_Animator.CrossFadeInFixedTime(Plusstatename+"damaged",0f);
        setSKelAnimation("damaged");
        Vector2 throwVector = new Vector2(8f, 2.0f);
            Vector2 damagerToThis = damager.transform.position - transform.position;

            throwVector.x = Mathf.Sign(damagerToThis.x) * -5.0f;
            m_MoveVector = throwVector;
        rb2d.AddForce(throwVector,ForceMode2D.Impulse);
            if (m_FlickeringCoroutine != null)
            {
                StopCoroutine(m_FlickeringCoroutine);
                //skeletonAnimation.skeleton.SetColor(m_OriginalColor);
            }
        //this.transform.DOShakePosition(0.7f, 1.2f, 20, 1, false, true);    2
        m_FlickeringCoroutine = StartCoroutine(Flicker(damageable));
            CameraShaker.Shake(0.15f, 0.3f);
       
        }

    public void setidle()
    {
        m_Animator.CrossFadeInFixedTime(Plusstatename + "walk", 0f);
            //setSKelAnimation("walk");
          }
    public void Effectmaker(GameObject effect)
    {
        Vector3 pos = this.transform.position;
         
        //effect.transform.position = effectpos.transform.position;
        Instantiate(effect, pos, Quaternion.identity) ;
    }
    public void damagedfreez()
    {
        //if (!armers)
            rb2d.velocity = new Vector2(0f, 0f);
    }

    public void seconddamager()
    {

    }

    public void AttackEnable()
    {

    }

    public void materialchange(Material hit,float mount)
    {
        
        m_SpriteRenderer.material = hit;
        disolvemount = disolvemount+0.1f;
        m_SpriteRenderer.material.SetFloat("AHO", mount); 
    }
    protected IEnumerator Flicker(Damagerable damageable)
        {
            float timer = 0f;
            float sinceLastChange = 0.0f;

            Color transparent = m_OriginalColor;
            transparent.r = 0.8f;
            int state = 1;

           // skeletonAnimation.skeleton.SetColor( transparent); 4

        //while (timer < damageable.invulnerabilityDuration)
        // {
        //    yield return null;
        //    timer += Time.deltaTime;
        //   sinceLastChange += 3*Time.deltaTime;
        //   if (sinceLastChange > flickeringDuration)
        //   {
        //        sinceLastChange -= flickeringDuration;
        //        state = 1 - state;
        //    if (state == 1) skeletonAnimation.skeleton.SetColor(transparent);
        //   else skeletonAnimation.skeleton.SetColor(m_OriginalColor);
        //     }
        //  }
        //skeletonAnimation.skeleton.SetColor(Color.red); 5
        yield return new WaitForSeconds(1);
        //skeletonAnimation.skeleton.SetColor(m_OriginalColor);  6
    }

        public void DisableDamage()
        {
            if (meleeDamager != null)
                meleeDamager.DisableDamage();
            if (contactDamager != null)
                contactDamager.DisableDamage();
        }

        public void PlayFootStep()
        {
            footStepAudio.PlayRandomSound();
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            //draw the cone of view
            Vector3 forward = spriteFaceLeft ? Vector2.left : Vector2.right;
            forward = Quaternion.Euler(0, 0, spriteFaceLeft ? -viewDirection : viewDirection) * forward;

            if (GetComponent<SpriteRenderer>().flipX) forward.x = -forward.x;

            Vector3 endpoint = transform.position + (Quaternion.Euler(0, 0, viewFov * 0.5f) * forward);

            Handles.color = new Color(0, 1.0f, 0, 0.2f);
            Handles.DrawSolidArc(transform.position, -Vector3.forward, (endpoint - transform.position).normalized, viewFov, viewDistance);

            //Draw attack range
            Handles.color = new Color(1.0f, 0, 0, 0.1f);
            Handles.DrawSolidDisc(transform.position, Vector3.back, meleeRange);
        }
#endif
    }

    //bit hackish, to avoid to have to redefine the whole inspector, we use an attirbute and associated property drawer to 
    //display a warning above the melee range when it get over the view distance.
    public class EnemyMeleeRangeCheckAttribute : PropertyAttribute
    {

    }

