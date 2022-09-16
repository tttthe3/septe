using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weraponeffect : MonoBehaviour
{
    static protected Weraponeffect s_Weraponeffect;

    static public Weraponeffect Instance { get { return s_Weraponeffect; } }

    public CreateItem iteminfo;
    GameObject effectAir;
    GameObject effectground;



    private GameObject parents;

    private void OnEnable()
    {
        if (s_Weraponeffect == null)
            s_Weraponeffect = this;

        //parents = GameObject.FindGameObjectWithTag("kama1");
        Debug.Log(parents);
        //iteminfo = GetComponent<CreateItem>();
        effectground = iteminfo.getgroundeffect();
        effectAir = iteminfo.getAireffect();
        //Charactercontrolelr.CCInstance.getanimationname();
        Debug.Log(effectAir);
    }

    public void MakeAIreffect()
    {
        parents = GameObject.FindGameObjectWithTag("kama1");
        Debug.Log(parents);
        Instantiate(effectground, this.gameObject.transform.position, effectground.transform.rotation, this.gameObject.transform);

    }

    public void MakeAIreffec2t()
    {
        ;
        Debug.Log("");


    }



}
