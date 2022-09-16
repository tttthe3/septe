using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idlewrapper : MonoBehaviour
{
    public enum Itemset { slash, lance, hummer }; //これをプレイヤーに八つける。colidも使用できる
    public Itemset set;
    private int NowLevel;
    private Slasher1 slash = new Slasher1();
    private Damager damaegeg;
    static public idlewrapper Instance { get { return s_idle; } }
    static protected idlewrapper s_idle;
    void Start()
    {
        s_idle = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
