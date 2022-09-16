using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class magnetobject : MonoBehaviour
{
    public bool polarity;
    public GameObject Player;
    private Rigidbody2D rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        float disToPlayer = Vector3.Distance(this.transform.position, Player.transform.position);
        if (disToPlayer < 15f&&Charactercontrolelr.CCInstance.GetAniState("gravidle"))
        {
            rigid.isKinematic = false;
            this.gameObject.transform.DOMove(Player.transform.position,0.3f);
            //rigid.AddForce(new Vector3((this.gameObject.transform.position.x-Player.transform.position.x)/Mathf.Pow(disToPlayer,2), (this.gameObject.transform.position.y - Player.transform.position.y) / Mathf.Pow(disToPlayer, 2),0f)*-5f);
        }
    }
   
}
