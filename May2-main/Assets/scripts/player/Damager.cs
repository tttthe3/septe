using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Damager : MonoBehaviour
{

    [Serializable]
    public class DamagableEvent : UnityEvent<Damager, Damagerable>
    { }


    [Serializable]
    public class NonDamagableEvent : UnityEvent<Damager>
    { }

    //call that from inside the onDamageableHIt or OnNonDamageableHit to get what was hit.
    public Collider2D LastHit { get { return m_LastHit; } }

    public int damage = 1;  //武器のダメージ量
    public Vector2 offset = new Vector2(1.5f, 1f);
    public Vector2 size = new Vector2(2.5f, 1f);
    [Tooltip("If this is set, the offset x will be changed base on the sprite flipX setting. e.g. Allow to make the damager alway forward in the direction of sprite")]
    public bool offsetBasedOnSpriteFacing = true;
    [Tooltip("SpriteRenderer used to read the flipX value used by offset Based OnSprite Facing")]
    public SpriteRenderer spriteRenderer;
    [Tooltip("If disabled, damager ignore trigger when casting for damage")]
    public bool canHitTriggers;
    public bool disableDamageAfterHit = false;
    [Tooltip("If set, the player will be forced to respawn to latest checkpoint in addition to loosing life")]
    public bool forceRespawn = false;
    [Tooltip("If set, an invincible damageable hit will still get the onHit message (but won't loose any life)")]
    public bool ignoreInvincibility = false;
    public LayerMask hittableLayers;
    public DamagableEvent OnDamageableHit;
    public NonDamagableEvent OnNonDamageableHit;
    public string enemytag;
    protected bool m_SpriteOriginallyFlipped;
    protected bool m_CanDamage = true;
    protected ContactFilter2D m_AttackContactFilter;
    protected Collider2D[] m_AttackOverlapResults = new Collider2D[10];
    protected Transform m_DamagerTransform;
    protected Collider2D m_LastHit;
    public bool hitcheck=false;
    public string hittername;
    public float attackonduration;
    Collider2D hit;
    public GameObject effect;
    private ContactPoint2D[] contacts = new ContactPoint2D[10];
    private Transform sample;
    void Awake()
    {
        m_AttackContactFilter.layerMask = hittableLayers;
        m_AttackContactFilter.useLayerMask = true;
        m_AttackContactFilter.useTriggers = canHitTriggers;
        

        m_DamagerTransform = transform;
    }

    public void EnableDamage() //characonで呼ぶ
    {
        m_CanDamage = true;
    }

    public void DisableDamage()
    {
        m_CanDamage = false;
    }
    public bool Getcandamage()
    {
        return m_CanDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == hittername)
        {
            hitcheck = true;
            hit = collision;
            EnableDamage();
            makeeffect(collision,collision.gameObject.transform);
            Debug.Log(this.gameObject.name);


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == hittername)
        {
            hitcheck = false;
        }
            
    }

    public void makeeffect(Collider2D collision,Transform  postion)
    {
        
        Vector2 hitPoint;
        collision.GetContacts(contacts);
        foreach (ContactPoint2D contact in contacts)
        {
             hitPoint = contact.point;
          

        };

        if (effect == null)
            return;
   
        Instantiate(effect, postion.position, Quaternion.identity);
    }


    void FixedUpdate() 
    {
        if (!m_CanDamage) //攻撃してない場合何もなし
            return;



        //int hitCount = Physics2D.OverlapArea(pointA, pointB, m_AttackContactFilter, m_AttackOverlapResults);

        if (hitcheck)
        {
            
            Damagerable damageable = hit.GetComponent<Damagerable>();
            Debug.Log("gtyio");
            if (damageable) //日っとありなら
            {
                OnDamageableHit.Invoke(this, damageable);
                damageable.TakeDamage(this, ignoreInvincibility);
               
                if (disableDamageAfterHit)
                    DisableDamage();
            }
            else
            {
                OnNonDamageableHit.Invoke(this);
            }

        }
    }
}


