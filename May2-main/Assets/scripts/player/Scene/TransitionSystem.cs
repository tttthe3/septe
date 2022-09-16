using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionSystem : MonoBehaviour
{

    public enum TrasitionType
    {
        SameScene,DifferentScene,NotGame
    }                                         //シーン＆プレイヤー移動のパターン

    public enum TrasitionCall
    {
        Extracall,InteractPress,OntriggerEnter,
    }  ///シーン移動のトリガー


    public GameObject transitionGameobject;   //移動するオブジェクト
    public TrasitionType trasitionType; //移動のタイプ
    public string newScene; //シーン移動ありの時の次シーン名
    public TrasitionCall trasitionCall; //移動条件
    public bool requreinventoryCheck; //移動に必要なアイテムを女児しているか
    public ItemDataBase inventorycontoroller; //アイテムの所持チェック
    bool m_transitionGameobeject;
    public ItemDataBase.InventoryChecker inventoryCheck;
    public TransitionPoint destinationTransform;
    [SerializeField]
    private BoxCollider before;
    [SerializeField]
    private BoxCollider after;  //同シーン内移動の時、confinerの変更

    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera camera1;
    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera camera2;

    public string lastentrance;



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == transitionGameobject)
        {
            m_transitionGameobeject = true;
            lastentrance = other.gameObject.name;
            if (ScreenFader.IsFading || SceneController.Transitioning)
                return;

            if (trasitionCall == TrasitionCall.OntriggerEnter)
                TransitionInternal();
        }
    }

    protected void TransitionInternal()
    {
        if (requreinventoryCheck)
        {
            if (!inventoryCheck.CheckInventory(inventorycontoroller))
                return;
        }

        if (trasitionType == TrasitionType.SameScene)
        {

            GameObjectTeleporter.Teleport2(transitionGameobject, destinationTransform.transform);
           
            TransitionPoint.CameraChange(camera1, camera2);
        }
        else
        {
            //SceneController.TransitionToScene(this);
        }
    }


}
