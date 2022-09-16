using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Camerazoominouter : MonoBehaviour
{   
    
    public CinemachineCameraOffset camerapos;
    public Vector3 upper;
    [SerializeField]
    private CinemachineVirtualCamera camera;
    public VolumeProfile sample;
    public MotionBlur blu;
    private MotionBlur motions;
    [SerializeField]
    private GameObject fixedcamera;

    public BoxCollider2D colod;
    // Start is called before the first frame update
    void Start()
    {
        
        camera = GetComponent<CinemachineVirtualCamera>();
        //vols.profile.TryGet<MotionBlur>(out blu);
        //camerapos = GetComponent<CinemachineCameraOffset>();
        sample.TryGet<MotionBlur>(out motions);
    }

    public void zoomout()
    {
       
        camera.m_Lens.FieldOfView = 135f;
    }

    public void Changefixed()
    {

        camera.Follow = fixedcamera.transform;
    }

    public void Cameraup()
    { Vector3 dest = new Vector3(10f,0f,0f);
        Time.timeScale = 0.3f;
        camerapos.m_Offset.x += 30f;
        camerapos.m_Offset.y += 15f ;
        blu.active = true;
    }

    public void delay()
    {
        Time.timeScale = 0.2f;
    }

    public void delayreset()
    {
        Time.timeScale = 1f;
    }

    public void Cameraupreset()
    {
        Time.timeScale = 1f;
        camerapos.m_Offset.x -= 30f;
        camerapos.m_Offset.y -= 15f;
        blu.active = false;

    }
    public void zoomin()
    {

        camera.m_Lens.FieldOfView = 115f;
    }

    public void colidset()
    {
        colod.offset = new Vector2(0f,5f);
    }

    public void colidreset()
    {
        colod.offset = new Vector2(0f, -2.3f);
    }
}
