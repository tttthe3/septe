using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EffectMaker : MonoBehaviour
{
    static protected EffectMaker s_effectmaker;
    static public EffectMaker CCInstance { get { return s_effectmaker; } }
    // Start is called before the first frame update

    public Transform SlashPos;
    void Start()
    {
        s_effectmaker = this;
    }

    // Update is called once per frame
    public void Initeffect(GameObject Effect,Transform Pos)
    {
        Instantiate(Effect, Pos.position, Quaternion.identity);

    }

    public void Initeffect_Slash(GameObject Effect)
    {
        Instantiate(Effect,SlashPos.position, Quaternion.identity,SlashPos);

    }

    public void DelaycallEffect(float time,GameObject Effect,Transform Pos)
    {
        DOVirtual.DelayedCall(time, () => Initeffect(Effect,Pos));
    }

    public void DestroyEffect(GameObject Effect)
    {
        Destroy(Effect);
    }

    public void Pause_effect(GameObject Effect,float timer)
    {

        if (Effect.GetComponent<ParticleSystem>().duration-0.2f < timer)
        {
            Debug.Log("pause");
            Effect.GetComponent<ParticleSystem>().Stop();
        }

    }
}
