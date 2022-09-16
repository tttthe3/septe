using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ItemChecker : MonoBehaviour
{
    [SerializeField]
    CreateItem Iteminfromation;

    public UnityEvent Itemget;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Iteminfromation.GetKindofItem() == CreateItem.KindofItem.Weapon)
            {
                ItemDataBase.Instance.AddItem(Iteminfromation);
                GetitemShow.Instance.seticonname(Iteminfromation.getitemname());
                this.gameObject.SetActive(false);
                Itemget.Invoke();
            }
            else
                ItemDataBase.Instance.AddItem_Use(Iteminfromation);

        }
    }
}
 