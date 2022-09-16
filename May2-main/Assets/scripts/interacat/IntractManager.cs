using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;
public class IntractManager : MonoBehaviour
{
    static public IntractManager Instance { get { return s_Intract; } }
    static protected IntractManager s_Intract;
    [SerializeField]
    private InteractWrapper[] Itemlist;
    private InteractWrapper currentitem;

    private bool push_trigger = false;
    private bool Laddr_trigger = false;
    private bool evevate = false;
    private bool open_trigger;
    private bool talk_trigger;
    // Start is called before the first frame update
    void Start()
    {
        currentitem = null;
        s_Intract = this;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "push")
        {
            PushObject objects = collision.GetComponent<PushObject>();
            if (objects)
            {
                push_trigger = true;
                currentitem = Itemlist[0];

            }
            //else push_trigger = false;
        }
        

        if (collision.gameObject.tag == "elevate")
        {
            PushObject objects = collision.GetComponent<PushObject>();
            if (objects)
            {
                evevate = true;
            }
            else evevate = false;
        }

        if (collision.gameObject.tag == "laddr")
        {
            Laddr_trigger = true;
            currentitem = Itemlist[1];

        }
        

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "laddr")
        {
            Laddr_trigger = false;

        }
        if (collision.gameObject.tag == "push")
        {
            push_trigger = false;
        }

      }
    private void FixedUpdate()
    {

        if (push_trigger)
        {
            currentitem = Itemlist[0];

        }
        else if (Laddr_trigger)
        {
            currentitem = Itemlist[1];
        }
        else
        { currentitem = null; }
    }

    public string getIntractname()
    {
        if (currentitem == null)
            return "";
        else
        return currentitem.name;
    }
    public void Firstframe(Rigidbody2D rb2d, Animator animator, Transform parent)
    {
        if (currentitem == null)
            return;
        currentitem.firstframe(rb2d,animator,parent);

    }
    public void Updateframe(Rigidbody2D rb2d, Animator animator,SkeletonAnimation animation)
    {
        
        currentitem.updateframe(rb2d, animator,animation);
    }

    public void Endframe()
    {
    }
}
