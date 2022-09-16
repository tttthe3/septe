using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;




public class Camera_zoom : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera camera;
    public float zoominrange;
    public float zoomoutrange;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
            zoomin();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
            zoomout();
    }

    public void zoomout()
    {

        camera.m_Lens.FieldOfView = zoomoutrange;
    }

    public void zoomin()
    {

        camera.m_Lens.FieldOfView = zoominrange;
    }

    
}
