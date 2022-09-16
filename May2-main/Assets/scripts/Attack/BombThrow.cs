using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombThrow :Itemwrapper
{
    public Vector3 firstvector;
    public int vectnumber;
    public GameObject vectline;
    private Rigidbody2D rigid;
    [SerializeField]
    private Transform dummyObjParent;
    private List<GameObject> dummySphereList = new List<GameObject>();
    public GameObject expo;
    [SerializeField]
    private float secInterval;
    public Vector2 bompos;
    [SerializeField]
    private GameObject bomb;
    private Transform hand;
    // Start is called before the first frame update
    private bool lefttrigger = false;
    public override void  firstframeLeft(Rigidbody2D rb2d, Animator animator)
    {
        lefttrigger = true;
        firstvector = new Vector3(0,0,0);
        hand.position = GameObject.Find("Player").transform.position;
        GameObject bo = Instantiate(bomb, hand.transform.position, Quaternion.identity);

        dummyObjParent.position = GameObject.Find("Player").transform.position;
        //expo.transform.position = dummyObjParent.transform.position;
        rigid = bo.GetComponent<Rigidbody2D>();
        if (!rigid)
            rigid = bo.AddComponent<Rigidbody2D>();
        rigid.isKinematic = true;
        dummyObjParent.transform.position = transform.position;

        //弾道予測を表示するための点を生成
        for (int i = 0; i < vectnumber; i++)
        {
            var obj = (GameObject)Instantiate(vectline, dummyObjParent);
            dummySphereList.Add(obj);

        }
           
        
    }

    public override void updateframeLeft(Rigidbody2D rb2d, Animator animator)
    {
        if (!lefttrigger)
            return;
        for (int i = 0; i < vectnumber; i++)
        {
            var t = i * secInterval;
            var x = t * firstvector.x;
            var z = t * firstvector.z;
            var y = (firstvector.y * t) - 0.5f * (-Physics.gravity.y) * Mathf.Pow(t, 2.0f);
            dummySphereList[i].transform.localPosition = new Vector3(x, y, z);
        }

        //vectorotate();
        Changefirst();
        //スペースキーで球の弾道を確認
        if (Playerinput.Instance.Skill.Down)
        {
            for (int i = 0; i < vectnumber; i++)
            {
                dummySphereList[i].gameObject.SetActive(false);
            }
            rigid.isKinematic = false;
            rigid.AddForce(firstvector * 2f, ForceMode2D.Impulse);
        }
    }



public void vectorotate()
{
    for (int i = 0; i < vectnumber; i++)
    {
        var angle = Vector3.Angle(dummySphereList[i].transform.position - dummyObjParent.transform.position, Vector3.up);
        angle = angle * Mathf.Rad2Deg;
        dummySphereList[i].transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

}

public void Changefirst()
{
    if (Playerinput.Instance.Select_Hoz.Value == -1)
    {
        firstvector.x -= 0.2f;
    }
    if (Playerinput.Instance.Select_Hoz.Value == 1)
    {
        firstvector.x += 0.2f;
    }
    if (Playerinput.Instance.Select_Vert.Value == -1)
    {
        firstvector.y -= 0.2f;
    }
    if (Playerinput.Instance.Select_Vert.Value == 1)
    {
        firstvector.y += 0.2f;
    }
}



public override void firstframeRight(Rigidbody2D rb2d, Animator animator)
    {
        lefttrigger = false;
        firstvector = new Vector3(0, 0, 0);
        hand = GameObject.Find("Player/WallCheck").transform;
        Debug.Log(hand.position);
        GameObject bo = Instantiate(bomb, hand.transform.position, Quaternion.identity);
        Debug.Log(hand.position);
        //dummyObjParent = GameObject.Find("Player/WallCheck").transform;
        //expo.transform.position = dummyObjParent.transform.position;
        rigid = bo.GetComponent<Rigidbody2D>();
        if (!rigid)
            rigid = bo.AddComponent<Rigidbody2D>();
        rigid.isKinematic = true;
        //dummyObjParent.transform.position = transform.position;
        Debug.Log(hand.position);
        //弾道予測を表示するための点を生成
        for (int i = 0; i < vectnumber; i++)
        {
           //GameObject obj = Instantiate(vectline, dummyObjParent.transform.position,Quaternion.identity);
           // dummySphereList.Add(obj);
            Debug.Log(dummySphereList[i]);
        }
        animator.CrossFadeInFixedTime("walk", 0f);
        Debug.Log(hand.position);

    }

    public override void updateframeRight(Rigidbody2D rb2d, Animator animator)
    {
        if (lefttrigger)
            return;
        Debug.Log(dummySphereList[0]);
        for (int i = 0; i < vectnumber; i++)
        {
            var t = i * secInterval;
            var x = t * firstvector.x;
            var z = t * firstvector.z;
            var y = (firstvector.y * t) - 0.5f * (-Physics.gravity.y) * Mathf.Pow(t, 2.0f);
            //dummySphereList[i].transform.localPosition = new Vector3(x, y, z);
        }

        //vectorotate();
        Changefirst();
        //スペースキーで球の弾道を確認
        if (Playerinput.Instance.Skill.Down)
        {
            for (int i = 0; i < vectnumber; i++)
            {
                //dummySphereList[i].gameObject.SetActive(false);
            }
            rigid.isKinematic = false;
            rigid.AddForce(firstvector * 12f, ForceMode2D.Impulse);
           
            Debug.Log(hand.position);
            animator.CrossFadeInFixedTime("idle", 0f);
        }
    }

}
