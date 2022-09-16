using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Maker : MonoBehaviour ///イベントアイテムをマップ内に生成する
{

    static protected Event_Maker s_Maker;
    static public Event_Maker Instance { get { return s_Maker; } }

    string root ;
    public Transform samplepos;

    private void Awake()
    {
        s_Maker = this;
    }
    public void callevent(string eventname)
    {
        GameObject list = Resources.Load(eventname) as GameObject; //特定のイベントを生成
        Instantiate(list, samplepos.position, Quaternion.identity);
    }
    public void InitObject(GameObject item)
    {
        GameObject sample = item;
        Instantiate(item, samplepos.position, Quaternion.identity);
    }



    public void InitDirector(DirectorTtrigger director, string foldername)
    {
        DirectorTtrigger dic = director;
        //instantiate

    }
}
