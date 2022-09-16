using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;


   
    public class DirectorTtrigger : MonoBehaviour, IDataPersister
    {
        public enum TriggerType
        {
            Once, Everytime,
        }
    　　
    　　public enum triggercond
           {collid,intract,talk }
        [Tooltip("This is the gameobject which will trigger the director to play.  For example, the player.")]
        public GameObject triggeringGameObject;
        public PlayableDirector director;
        public TriggerType triggerType;
        public triggercond triggercondtion;
        public UnityEvent OnDirectorPlay;
        public UnityEvent OnDirectorFinish;
        [HideInInspector]
        public DataSettings dataSettings;
        public Flag_content finishflag;
        protected bool m_AlreadyTriggered;


    void Start()
    {
        if (finishflag.flag == true)
        {
            director.time = director.duration;
            OnDirectorPlay = null;
            this.gameObject.SetActive(false);
        }
    }
        void OnEnable()
        {
            PersistentDataManager.RegisterPersister(this);
        
    }

        void OnDisable()
        {
            PersistentDataManager.UnregisterPersister(this);
        }

    void Update()
    {
        if (Playerinput.Instance.Intract.Down)
        {
            //director.Pause();
        }
    }

        void OnTriggerEnter2D(Collider2D other)
        {
        if (triggercondtion == triggercond.collid)
        {
            if (other.gameObject != triggeringGameObject)
                return;

            if (triggerType == TriggerType.Once && m_AlreadyTriggered||finishflag.flag)
                return;

           
                director.Play();
                m_AlreadyTriggered = true;
                OnDirectorPlay.Invoke();
            if(director.time> director.duration)
                OnDirectorFinish.Invoke();

        }

        }
   

         void OnTriggerStay2D(Collider2D other) {
        if (triggercond.intract == triggercondtion)
        {
            if (other.gameObject != triggeringGameObject)
                return;

            if (triggerType == TriggerType.Once && m_AlreadyTriggered)
                return;

            if (Playerinput.Instance.Intract.Down)
            {
               // director.Play();
                //m_AlreadyTriggered = true;
               // OnDirectorPlay.Invoke();
               // Invoke("FinishInvoke", (float)director.duration);
            }
        }
        
    
         }

    public void Isdone()
    {
        finishflag.flag = true;
    }


    public void playmovie()
    {
        director.Play();
        m_AlreadyTriggered = true;
        OnDirectorPlay.Invoke();
        Invoke("FinishInvoke", (float)director.duration);

    }

        void cutinmovie()
        {
        OnDirectorPlay.Invoke();
        director.Play();
        Invoke("FinishInvoke", (float)director.duration);
    }
        void FinishInvoke()
        {
            OnDirectorFinish.Invoke();
        }

        public void OverrideAlreadyTriggered(bool alreadyTriggered)
        {
            m_AlreadyTriggered = alreadyTriggered;
        }

        public DataSettings GetDataSettings()
        {
            return dataSettings;
        }

        public void SetDataSettings(string dataTag, DataSettings.PersistenceType persistenceType)
        {
            dataSettings.dataTag = dataTag;
            dataSettings.persistenceType = persistenceType;
        }

        public Data SaveData()
        {
            return new Data<bool>(m_AlreadyTriggered);
        }

        public void LoadData(Data data)
        {
            Data<bool> directorTriggerData = (Data<bool>)data;
            m_AlreadyTriggered = directorTriggerData.value;
        }
    }
