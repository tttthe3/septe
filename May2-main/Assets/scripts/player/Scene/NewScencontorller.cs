using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class NewScencontorller : MonoBehaviour
{


    [SerializeField]
    Cinemachine.CinemachineVirtualCamera cameras;
    protected Scene m_CurrentZoneScene;
    protected SceneTransitionDestination.DestinationTag m_ZoneRestartDestinationTag;
    protected Playerinput m_PlayerInput;
    protected bool m_Transitioning;

    public static NewScencontorller Instance
    {
        get
        {
            if (instance != null)
                return instance;

            instance = FindObjectOfType<NewScencontorller>();

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

    protected static NewScencontorller instance;

    public static NewScencontorller Create()
    {
        GameObject sceneControllerGameObject = new GameObject("NewScencontorller");
        instance = sceneControllerGameObject.AddComponent<NewScencontorller>();

        return instance;
    }



    public SceneTransitionDestination initialSceneTransitionDestination;

   
}
    
