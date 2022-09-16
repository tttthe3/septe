using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using UnityEngine.SceneManagement;

public class savepoint : MonoBehaviour
{
    public string Savescene;
    public bool respawnFacingLeft;
    public string Savepointname;
    public GameObject transitioningGameObject;
    public GameObject Player;
    private void Start()
    {
        Savescene= SceneManager.GetActiveScene().name;
    }

    private void Reset()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Charactercontrolelr c = collision.GetComponent<Charactercontrolelr>();
        if (c != null)
        {
            Debug.Log(Savepointname );
            c.SetSavepoint(Savescene,this.gameObject.name);
            Savepointname= this.gameObject.name;
        }
    }

    public void Menuin()
    {
        float disToPlayer = Vector3.Distance(transform.position, Player.transform.position);
        if (disToPlayer < 2.5f&&Playerinput.Instance.Intract.Down)
        {
            StartCoroutine("DisplaySB");
        }
    }
}