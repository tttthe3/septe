using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
public class switchObject : MonoBehaviour
{
    public GameObject switchon;
    public PlayableDirector director;
    public UnityEvent OnDirectorPlay;
    public UnityEvent OnDirectorFinish;
    public TriggerType triggerType;
    protected bool m_AlreadyTriggered;
    public enum TriggerType
    {
        Once, Everytime,
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == switchon)
        {
            if (triggerType == TriggerType.Once && m_AlreadyTriggered)
            
                return;
                director.Play();
                m_AlreadyTriggered = true;
                OnDirectorPlay.Invoke();
                collision.gameObject.GetComponent<PushObject>().enabled = false;
                Invoke("FinishInvoke", (float)director.duration);
            
        }
    }

    void FinishInvoke()
    {
        OnDirectorFinish.Invoke();
    }
}
