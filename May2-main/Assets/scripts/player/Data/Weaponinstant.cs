using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weaponinstant : MonoBehaviour
{
    [SerializeField]
    private GameObject Weapon;

    private GameObject weaponsouce;
   private string Itemname;

    public void GenerateChildren(string itemname)
    {
       
       
        weaponsouce = (GameObject)Resources.Load(Itemname);
            var parent = this.transform;
            var instance = Instantiate(weaponsouce, parent.transform.position, parent.transform.rotation, parent);
        
    }

    private Vector3 spawnPos()
    {
        return new Vector3(0f, 0f, 0.0f);
    }

}
