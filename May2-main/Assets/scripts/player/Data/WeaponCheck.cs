using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WeaponCheck : MonoBehaviour
{
   
    [SerializeField]
    GameObject  parenetobject;
    GameObject[] childobject;

    public Image[] image;

   
    private List<CreateItem> Iconcheck = new List<CreateItem>();

    private void Start()
    {

       
        



    }
    private void OnEnable()
    {
        childobject = new GameObject[parenetobject.transform.childCount];
        for (int i = 0; i < parenetobject.transform.childCount; i++)
        {

            childobject[i] = parenetobject.transform.GetChild(i).gameObject;
            image[i] = childobject[i].GetComponent<Image>();


        }

        Iconcheck =ItemDataBase.Instance.GetItemList();

        for (int i = 0; i < parenetobject.transform.childCount; i++)

            if (Iconcheck[i].getitemname() == childobject[i].gameObject.name)
                image[i].sprite = Iconcheck[i].GetSprite();
            else
                return;
            



    }
}


