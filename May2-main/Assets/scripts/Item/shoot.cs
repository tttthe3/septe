using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
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
    public LayerMask ground;

    // Start is called before the first frame update


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag =="ground")
        {
            Debug.Log("exp");
            Instantiate(expo, collision.contacts[0].point, Quaternion.identity);
            //expo.transform.position = collision.ClosestPoint(this.transform.position);
            //expo.gameObject.SetActive(true) ;
            Destroy(this.gameObject);
        }
    }

   



    // Update is called once per frame


    public void vectorotate()
    {
        for (int i = 0; i < vectnumber; i++) {
            var angle = Vector3.Angle(dummySphereList[i].transform.position-dummyObjParent.transform.position, Vector3.up);
            angle = angle * Mathf.Rad2Deg;
            dummySphereList[i].transform.rotation = Quaternion.Euler(0f,0f, angle);
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
        if (Playerinput.Instance.Select_Vert.Value ==- 1)
        {
            firstvector.y -= 0.2f;
        }
        if (Playerinput.Instance.Select_Vert.Value == 1)
        {
            firstvector.y += 0.2f;
        }
    }
}

