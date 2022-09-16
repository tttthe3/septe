using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;
public class Laddar : InteractWrapper
{
    private Vector3 velocity = Vector3.zero;
    RaycastHit2D ground;
    private bool groundtrigger = false;
    public LayerMask groundtype;
    Vector2 direction = new Vector2(0f, 0f);
    public Vector2 power;
    public RandomAudioPlayer Ladder_sound;
    private float timer = 0f;
    public void Playsound()
    {
        Ladder_sound.PlayRandomSound();
    }
    public override void firstframe(Rigidbody2D rb2d, Animator animator,Transform parent)
    {
        timer = 0f;
        GameObject PlayerPos= GameObject.Find("player");
        Ladder_sound = parent.Find("sounds/Ladder").GetComponent<RandomAudioPlayer>();
        if (Charactercontrolelr.CCInstance.GetAniState("idle"))
        animator.CrossFadeInFixedTime("ladder2", 0f);

    }
    public override void updateframe(Rigidbody2D rb2d, Animator animator, SkeletonAnimation animation)
    {

        if (Playerinput.Instance.Select_Vert.Value == 1)
        {
            direction = Vector2.up;
           
        }
        else if (Playerinput.Instance.Select_Vert.Value == -1)
        {
            direction = Vector2.down;
            
        }
        timer += Time.deltaTime;

        

            groundtrigger = ground;
        Debug.Log(rb2d.transform.position);
        if (Playerinput.Instance.Select_Vert.Value==1)
        {
            if (timer > 0.5f)
            {
                Playsound();
                timer = 0f;
            }
            rb2d.gravityScale = 0f;
            animation.timeScale = 1.7f;
            Vector3 targetvelocity = new Vector2(0f, 4f);
            rb2d.velocity = targetvelocity;
            ground = Physics2D.Raycast(rb2d.transform.position, direction, 1f, groundtype);
            groundtrigger = ground;
            if (groundtrigger)
            {
                animator.CrossFadeInFixedTime("idle", 0f);
                rb2d.gravityScale = 6f;
                rb2d.AddForce(power,ForceMode2D.Impulse);

            }
        }
        else if(Playerinput.Instance.Select_Vert.Value == -1)
        {
            if (timer > 0.5f)
            {
                Playsound();
                timer = 0f;
            }

            rb2d.gravityScale = 0f;
            Vector3 targetvelocity = new Vector2(0f, -4f);
            rb2d.velocity = targetvelocity;
            animation.timeScale = -1.7f;
            ground = Physics2D.Raycast(rb2d.transform.position, direction, 4f, groundtype);
            groundtrigger = ground;
            Debug.Log(direction);
            Debug.Log(Playerinput.Instance.Select_Vert.Value == 1);
            if (groundtrigger)
            {
                animator.CrossFadeInFixedTime("idle", 0f);
                rb2d.gravityScale = 6f;
            }
        }
        else
        {
            rb2d.gravityScale = 0f;
            Vector3 targetvelocity = new Vector2(0f, 0f);
            rb2d.velocity = targetvelocity;
            animation.timeScale = 0f;
            
        }
        


    }

    public override void endframe() { }
}
