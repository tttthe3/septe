using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;


    /// <summary>
    /// This class is used to transition between scenes. This includes triggering all the things that need to happen on transition such as data persistence.
    /// </summary>
    public class SceneController : MonoBehaviour
    {

    [SerializeField]
    Cinemachine.CinemachineVirtualCamera cameras;
    
    public static SceneController Instance
        {
            get
            {
                if (instance != null)
                    return instance;

                instance = FindObjectOfType<SceneController>();

                if (instance != null)
                    return instance;

                Create();

                return instance;
            }
        }

        public static bool Transitioning
        {
            get { return Instance.m_Transitioning; }
        }

        protected static SceneController instance;

        public static SceneController Create()
        {
            GameObject sceneControllerGameObject = new GameObject("SceneController");
            instance = sceneControllerGameObject.AddComponent<SceneController>();

            return instance;
        }

        public SceneTransitionDestination initialSceneTransitionDestination;

        protected Scene m_CurrentZoneScene;
        protected SceneTransitionDestination.DestinationTag m_ZoneRestartDestinationTag;
        protected Playerinput m_PlayerInput;
        protected bool m_Transitioning;
        [SerializeField]
       
       
        void Awake()
        {
            if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);

            m_PlayerInput = FindObjectOfType<Playerinput>();

            if (initialSceneTransitionDestination != null)
            {
                SetEnteringGameObjectLocation(initialSceneTransitionDestination);
                ScreenFader.SetAlpha(1f); 
                StartCoroutine(ScreenFader.FadeSceneIn());
                initialSceneTransitionDestination.OnReachDestination.Invoke();
            }
            else
            {
                m_CurrentZoneScene = SceneManager.GetActiveScene();
                m_ZoneRestartDestinationTag = SceneTransitionDestination.DestinationTag.A;
            }

        cameras = GetComponent<Cinemachine.CinemachineVirtualCamera>();
        }

        public static void RestartZone(bool resetHealth = true)
        {
            if (resetHealth && Charactercontrolelr.CCInstance != null)
            {
            Charactercontrolelr.CCInstance.damageable.SetHealth(Charactercontrolelr.CCInstance.damageable.startingHealth);
            }

            Instance.StartCoroutine(Instance.Transition(Instance.m_CurrentZoneScene.name, true, Instance.m_ZoneRestartDestinationTag, TransitionPoint.TransitionType.DifferentZone));
        }

        public static void RestartZoneWithDelay(float delay, bool resetHealth = true)
        {
            Instance.StartCoroutine(CallWithDelay(delay, RestartZone, resetHealth));
        }

        public static void TransitionToScene(TransitionPoint transitionPoint)  //この方法だと
        {
            Instance.StartCoroutine(Instance.Transition(transitionPoint.newSceneName, transitionPoint.resetInputValuesOnTransition, transitionPoint.transitionDestinationTag, transitionPoint.transitionType));
        }

    public static void TransitionToSceneGame(savainstance inst)  //この方法だと
    {
        Debug.Log(inst.Getdate());
        Instance.StartCoroutine(Instance.LoadGame(inst.Getdate().savepoint, inst.Getdate().loadscene));
        Debug.Log(inst.Getdate().loadscene);
    }

    protected IEnumerator LoadGame(string newSceneName,  string startpos, TransitionPoint.TransitionType transitionType = TransitionPoint.TransitionType.DifferentZone)
    {
        if (m_PlayerInput == null)
            m_PlayerInput = FindObjectOfType<Playerinput>();
        Debug.Log(newSceneName);
        yield return StartCoroutine(ScreenFader.FadeSceneOut(ScreenFader.FadeType.Loading)); //フェードイン
        yield return SceneManager.LoadSceneAsync(newSceneName); //次シーンロード

        m_PlayerInput = FindObjectOfType<Playerinput>();
        Playerinput.Instance.ReleaseController(true);
        PersistentDataManager.LoadAllData();
        Debug.Log("duts");
        //save_pointを代入して
        savepoint entrance = GetSavestation(startpos); //入口探し
        SetEnteringSaveLocation(entrance);
        savainstance.Instance.loadcamera();
       
        //SetupNewScene(transitionType, entrance);
        Playerinput.Instance.GainControl();
        yield return StartCoroutine(ScreenFader.FadeSceneIn());
        //m_PlayerInput.GainControl();

        m_Transitioning = false;
    }




        public static SceneTransitionDestination GetDestinationFromTag(SceneTransitionDestination.DestinationTag destinationTag)
        {
            return Instance.GetDestination(destinationTag);
        }

        protected IEnumerator Transition(string newSceneName, bool resetInputValues, SceneTransitionDestination.DestinationTag destinationTag, TransitionPoint.TransitionType transitionType = TransitionPoint.TransitionType.DifferentZone)
        {
            m_Transitioning = true;
            PersistentDataManager.SaveAllData();

            if (m_PlayerInput == null)
                m_PlayerInput = FindObjectOfType<Playerinput>();
            m_PlayerInput.ReleaseController(resetInputValues);
            yield return StartCoroutine(ScreenFader.FadeSceneOut(ScreenFader.FadeType.Loading)); //フェードイン
            PersistentDataManager.ClearPersisters();
            Debug.Log(PersistentDataManager.Instance);
            yield return SceneManager.LoadSceneAsync(newSceneName); //次シーンロード
            
            m_PlayerInput = FindObjectOfType<Playerinput>();
            m_PlayerInput.ReleaseController(resetInputValues);
            PersistentDataManager.LoadAllData();

            SceneTransitionDestination entrance = GetDestination(destinationTag); //入口探し
            SetEnteringGameObjectLocation(entrance);
            SetupNewScene(transitionType, entrance);
            
            if (entrance != null)
                entrance.OnReachDestination.Invoke();
            yield return StartCoroutine(ScreenFader.FadeSceneIn());
            m_PlayerInput.GainControl();

            m_Transitioning = false;
        }

        protected SceneTransitionDestination GetDestination(SceneTransitionDestination.DestinationTag destinationTag)
        {
            SceneTransitionDestination[] entrances = FindObjectsOfType<SceneTransitionDestination>(); //シーン内のセーブポイント全取得
            for (int i = 0; i < entrances.Length; i++)
            {
                if (entrances[i].destinationTag == destinationTag)
                    return entrances[i];
            }
            Debug.LogWarning("No entrance was found with the " + destinationTag + " tag.");
            return null;
        }

    protected savepoint GetSavestation(string savepintname)
    {
       savepoint[] entrances = FindObjectsOfType<savepoint>(); //シーン内のセーブポイント全取得
        for (int i = 0; i < entrances.Length; i++)
        {
            if (entrances[i].Savepointname == savepintname)
            {
                Debug.Log(entrances[i].Savepointname);
                return entrances[i];
            }
        }
       
        return null;
    }

    protected SceneTransitionDestination GetDestination2(string savepoint_name)
    {
        SceneTransitionDestination[] entrances = FindObjectsOfType<SceneTransitionDestination>(); //シーン内のセーブポイント全取得
        for (int i = 0; i < entrances.Length; i++)
        {
            if (entrances[i].gameObject.name == savepoint_name)
                return entrances[i];
        }
        
        return null;
    }

    protected void SetEnteringGameObjectLocation(SceneTransitionDestination entrance)
    {
        if (entrance == null)
        {
            Debug.LogWarning("Entering Transform's location has not been set.");
            return;
        }
        Transform entranceLocation = entrance.transform;
        Transform enteringTransform = entrance.transitioningGameObject.transform;
        enteringTransform.position = entranceLocation.position;
        enteringTransform.rotation = entranceLocation.rotation;



    }

    protected void SetEnteringSaveLocation(savepoint entrance)
    {
        if (entrance == null)
        {
            Debug.LogWarning("Entering Transform's location has not been set.");
            return;
        }
        Transform entranceLocation = entrance.transform;
        Transform enteringTransform = entrance.transitioningGameObject.transform;
        enteringTransform.position = entranceLocation.position;
        enteringTransform.rotation = entranceLocation.rotation;

    }

    protected void SetupNewScene(TransitionPoint.TransitionType transitionType, SceneTransitionDestination entrance)
        {
            if (entrance == null)
            {
                Debug.LogWarning("Restart information has not been set.");
                return;
            }

            if (transitionType == TransitionPoint.TransitionType.DifferentZone)
                SetZoneStart(entrance);
        }

        protected void SetZoneStart(SceneTransitionDestination entrance)
        {
            m_CurrentZoneScene = entrance.gameObject.scene;
            m_ZoneRestartDestinationTag = entrance.destinationTag;
        }

        static IEnumerator CallWithDelay<T>(float delay, Action<T> call, T parameter)
        {
            yield return new WaitForSeconds(delay);
            call(parameter);
        }
    }
