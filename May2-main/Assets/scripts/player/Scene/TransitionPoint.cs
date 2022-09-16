using UnityEngine;
using Cinemachine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class TransitionPoint : MonoBehaviour
{
    public enum TransitionType
    {
        DifferentZone, DifferentNonGameplayScene, SameScene,
    }


    public enum TransitionWhen
    {
        ExternalCall, InteractPressed, OnTriggerEnter,
    }


    [Tooltip("This is the gameobject that will transition.  For example, the player.")]
    public GameObject transitioningGameObject;
    [Tooltip("Whether the transition will be within this scene, to a different zone or a non-gameplay scene.")]
    public TransitionType transitionType;
   
    public string newSceneName;
    [Tooltip("The tag of the SceneTransitionDestination script in the scene being transitioned to.")]
    public SceneTransitionDestination.DestinationTag transitionDestinationTag;
    public savepoint savepointDestinationTag;
    [Tooltip("The destination in this scene that the transitioning gameobject will be teleported.")]
    public TransitionPoint destinationTransform;
    [Tooltip("What should trigger the transition to start.")]
    public TransitionWhen transitionWhen;
    [Tooltip("The player will lose control when the transition happens but should the axis and button values reset to the default when control is lost.")]
    public bool resetInputValuesOnTransition = true;
    [Tooltip("Is this transition only possible with specific items in the inventory?")]
    public bool requiresInventoryCheck;
    [Tooltip("The inventory to be checked.")]
    public ItemDataBase inventoryController;
    [Tooltip("The required items.")]
    public ItemDataBase.InventoryChecker inventoryCheck;

    bool m_TransitioningGameObjectPresent;
    bool stagemove = false;
    [SerializeField]
    private BoxCollider before;
    [SerializeField]
    private BoxCollider after;

    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera camera1;
    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera camera2;

    public string lastentrance;

    void Start()
    {
        if (transitionWhen == TransitionWhen.ExternalCall)
            m_TransitioningGameObjectPresent = true;
        before = GetComponent<BoxCollider>();
        after = GetComponent<BoxCollider>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == transitioningGameObject)
        {
            m_TransitioningGameObjectPresent = true;
            lastentrance = other.gameObject.name;
            if (ScreenFader.IsFading || SceneController.Transitioning)
                return;

            if (transitionWhen == TransitionWhen.OnTriggerEnter)
                TransitionInternal();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == transitioningGameObject)
        {
            m_TransitioningGameObjectPresent = false;
        }
    }

    void Update()
    {
        if (ScreenFader.IsFading || SceneController.Transitioning)
            return;

        if (!m_TransitioningGameObjectPresent)
            return;

        if (transitionWhen == TransitionWhen.InteractPressed)
        {
            // if (Playerinput.Instance.Interact.Down)
            {
                TransitionInternal();
            }
        }
    }

    protected void TransitionInternal()
    {
        if (requiresInventoryCheck)
        {
            if (!inventoryCheck.CheckInventory(inventoryController))
                return;
        }

        if (transitionType == TransitionType.SameScene)
        {
            StartCoroutine(cameras());
            GameObjectTeleporter.Teleport2(transitioningGameObject, destinationTransform.transform);
            Debug.Log(destinationTransform);
            //cameras2();
            //StartCoroutine(cameras());
            //TransitionPoint.CameraChange(camera1,camera2);
        }
        else
        {
            SceneController.TransitionToScene(this);
        }
    }

    protected IEnumerator cameras()
    {
        //Playerinput.Instance.ReleaseController(true);
        yield return new WaitForSeconds(1.6f);
        CameraChange(camera1, camera2);
        Playerinput.Instance.GainControl();
    }

    public void cameras2()
    {
        Playerinput.Instance.ReleaseController(true);
        
        CameraChange(camera1, camera2);
        Playerinput.Instance.GainControl();
    }

    public static void CameraChange(Cinemachine.CinemachineVirtualCamera c_camera1, Cinemachine.CinemachineVirtualCamera c_camera2)
    {
        //c_camera1.Priority = 1;
        c_camera2.Priority = c_camera1.Priority + 1;

    }

    IEnumerator cameramove(Cinemachine.CinemachineVirtualCamera c_camera1, Cinemachine.CinemachineVirtualCamera c_camera2)
    {
        yield return new WaitForSeconds(1.4f);
        c_camera1.Priority = 1;
        c_camera2.Priority = c_camera1.Priority + 1;
    }

    public void Transition()
    {
        if (!m_TransitioningGameObjectPresent)
            return;

        if (transitionWhen == TransitionWhen.ExternalCall)
            TransitionInternal();

    }
}