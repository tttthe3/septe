using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;
public class AttackWrapper : MonoBehaviour
{


    public enum Itemset {slash,lance,hummer,none }; //これをプレイヤーに八つける。colidも使用できる
    public Itemset set;
    private int NowLevel;
    private Slasher1 slash= new Slasher1();
    private Lans1 lans = new Lans1();
    private  CreateItem currentitem;
    private AttackWrapper curretAttack=null;
    public AttackWrapper[] Weapons;
    static public AttackWrapper Instance { get { return s_attack; } }
    static protected AttackWrapper s_attack;
    public string WeaponName;
    public Damager Slash;
    public Damager lance;
    public Damager Humer;
    private void Awake()
    {
        s_attack = this;
        
        set = Itemset.none;
        
    }

    public void Start()
    {
        Slash.enabled = false;
        lance.enabled = false;
        Humer.enabled = false;
        //damaegeg.enabled = false;
    }
    public void WeaponSetter(string ItemName)
    {
        
            for (int i = 0; i < Weapons.Length; i++)
            {
                if (Weapons[i].WeaponName == ItemName)
                {
                if (!ItemDataBase.Instance.HasItem(ItemName))
                    return;
                Debug.Log(ItemDataBase.Instance.HasItem(ItemName));
                    curretAttack = Weapons[i]; //ここでdamagerとる
                }

           
            

        }
        
            //curretAttack = null;

        Debug.Log(curretAttack);
        Debug.Log(ItemDataBase.Instance.HasItem(ItemName));

    }


    public Itemset ouptputstate()
    {
        return set;
    }
    public void firstframe(Rigidbody2D rb2d, float newHorizontalMovement, Animator animator, Transform parent) {
        if (curretAttack == null)
            return;
        curretAttack.Firstframe(rb2d,  newHorizontalMovement,  animator,  parent);
        
    }
    public void updateframe(Animator animator) {
        curretAttack.Updateframe(animator);
    }

    public void endframe(Animator animator) {
        curretAttack.EndFrame(animator);
    }

    public void special_firstframe(Rigidbody2D rb2d, float newHorizontalMovement, Animator animator, int set, Transform parent)
    {
        if (curretAttack == null)
            return;
        curretAttack.Special_Firstframe( rb2d,  newHorizontalMovement,  animator,  set, parent);
    }

    public void special_updateframe(Rigidbody2D rb2d, float newHorizontalMovement, Animator animator, int set, SkeletonAnimation animation)
    {
        curretAttack.Special_Updateframe( rb2d,  newHorizontalMovement,animator,  set,  animation);
    }

    public void Air_firstframe(Rigidbody2D rb2d, float newHorizontalMovement, Animator animator, int set, Transform parent)
    {
        if (curretAttack == null)
            return;
        curretAttack.Air_Firstframe(rb2d,  newHorizontalMovement,  animator,  set, parent);
    }

    public void Air_updateframe(Rigidbody2D rb2d, float newHorizontalMovement, Animator animator, int set)
    {
        curretAttack.Air_Updateframe( rb2d, newHorizontalMovement,  animator,  set);
    }


    //以下上書き用

    public virtual void Firstframe(Rigidbody2D rb2d, float newHorizontalMovement, Animator animator, Transform parent)
    {

    }

    public virtual void Updateframe(Animator animator)
    {

    }

    public virtual void EndFrame(Animator animator)
    {

    }

    public virtual void Air_Firstframe(Rigidbody2D rb2d, float newHorizontalMovement, Animator animator, int set, Transform parent)
    {

    }

    public virtual void Air_Updateframe(Rigidbody2D rb2d, float newHorizontalMovement, Animator animator, int set)
    {

    }

    public virtual void Special_Firstframe(Rigidbody2D rb2d, float newHorizontalMovement, Animator animator, int set, Transform parent)
    {

    }

    public virtual void Special_Updateframe(Rigidbody2D rb2d, float newHorizontalMovement, Animator animator, int set, SkeletonAnimation animation)
    {

    }
}