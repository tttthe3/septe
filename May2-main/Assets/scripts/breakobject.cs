using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakobject : MonoBehaviour
{
    public GameObject breakpart;
    public bool hitcheck;
    Collider2D hit;
    public string hittername;
    float countet = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == hittername)
        {
            hit = collision;
            hitcheck = true;
            Debug.Log("hits");
        }
    }
   
    // Start is called before the first frame update
    void Start()
    {
        breakpart.GetComponent<ParticleSystem>();
        //breakpart.Stop();
    }

    void FixedUpdate()
    {



        //int hitCount = Physics2D.OverlapArea(pointA, pointB, m_AttackContactFilter, m_AttackOverlapResults);

        if (hitcheck)
        {

           Damager damageable = hit.GetComponent<Damager>();

            if (damageable) //日っとありなら
            {
                Instantiate(breakpart, this.gameObject.transform.position, Quaternion.identity);
                //if(countet>0.7f)
                this.gameObject.SetActive(false);
            }
            else
            {
                //OnNonDamageableHit.Invoke(this);
            }

        }
    }

    public void moviebreak()
    {
        Instantiate(breakpart, this.gameObject.transform.position, Quaternion.identity);
        //if(countet>0.7f)
        this.gameObject.SetActive(false);
    }
}
