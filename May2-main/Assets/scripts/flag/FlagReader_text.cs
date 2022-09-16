using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagReader_text : MonoBehaviour
{


    public Flag_content_List refList;
    public Flag_content[] refcontent;
    public enum eventtype { text,init,destroy};
    public eventtype[] Eventtype;
    public TextEvent initevent; //このフラグリストからおこしうるイベント数
    public bool istrigger = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (initevent.Flagscheck()&&!initevent.GetEventState())
        {        
            if (initevent.Geteventtype() == TextEvent.eventstate.text)
            {
                initevent.SetEventState(true);
               Event_Wrapper.Instance.TextChange(initevent.Gettargetname(), initevent.GetTextref());
            }
        }
    }
}
