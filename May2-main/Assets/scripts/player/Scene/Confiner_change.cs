using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Confiner_change : MonoBehaviour
{
    [SerializeField]
    Cinemachine.CinemachineVirtualCamera camera;
    [SerializeField]
    Cinemachine.CinemachineConfiner confiner;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Cinemachine.CinemachineVirtualCamera>();
        confiner = GetComponent<Cinemachine.CinemachineConfiner>();
    }

    // Update is called once per frame
   void act_change()
    {
       
    }
}
