using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag_write_Break : MonoBehaviour
{
    public GameObject targetobject;
    public Flag_content flags;
    private bool alreadyflag=false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!alreadyflag && !flags.flag)
        {
            if (!targetobject.activeSelf)
            {
                flags.flag = true;
                alreadyflag = true;
            }
        }
        
    }
}
