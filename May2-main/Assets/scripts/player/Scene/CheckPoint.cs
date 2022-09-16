using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    [RequireComponent(typeof(BoxCollider2D))]
    public class CheckPoint : MonoBehaviour
    {
        public bool respawnFacingLeft;

        private void Reset()
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Charactercontrolelr c = collision.GetComponent<Charactercontrolelr>();
            if (c != null)
            {
                c.SetChekpoint(this);
            }
        }
    }
