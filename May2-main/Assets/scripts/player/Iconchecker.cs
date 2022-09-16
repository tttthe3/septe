using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class Iconchecker : MonoBehaviour
{

    
    public static Iconchecker Instance { get { return s_Iconcheck; } }
    protected static Iconchecker s_Iconcheck;
    
    private string gameobjectname;
    private GameObject weaponsouce;
    [SerializeField]
    private GameObject parents;

    private List<CreateItem> Iconcheck = new List<CreateItem>();
    Image iconimage;
    private void OnEnable()
    {
        Iconcheck = ItemDataBase.Instance.GetItemList();
        iconimage = GetComponent<Image>();
       
        for (int i = 0; i < Iconcheck.Count; i++) {
            
            if (Iconcheck[i].getitemname() == this.gameObject.name)
            {
                iconimage.sprite = Iconcheck[i].GetSprite();
                gameobjectname = Iconcheck[i].getitemname();

            }
    }
       
    }
    public void OnClick()
    {
        foreach(Transform n in parents.transform)
        {
            GameObject.Destroy(n.gameObject);
        }
       Transform parerot = parents.transform;
        parerot.Rotate(0f, 0f, 40f);
        weaponsouce = (GameObject)Resources.Load(gameobjectname);
        var parent = this.transform;
        var instance = Instantiate(weaponsouce, parents.transform.position,parents.transform.rotation, parents.transform);

    }

    //アイテムの現在レベルからコオブジェクトの画像を更新






}


