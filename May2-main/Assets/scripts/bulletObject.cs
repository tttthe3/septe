using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletObject : MonoBehaviour
{
    public float burntimer = 0;
    public GameObject burn;
    public Rigidbody2D rb2d;
    public Vector2 Power;
    
    void Start()
    {
        Vector2 addpower = new Vector2(Charactercontrolelr.CCInstance.GetSpriteword()*Power.x,Power.y);
        rb2d.AddForce(addpower, ForceMode2D.Impulse);
    }

    private void OnEnable()
    {
        burntimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        burntimer += Time.deltaTime;
        if (burntimer > 1f)
        {
            Instantiate(burn, rb2d.transform.position, Quaternion.identity);
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "enemy")
        {
            Instantiate(burn, rb2d.transform.position, Quaternion.identity);
            //this.gameObject.SetActive(false);
        }
    }
}
