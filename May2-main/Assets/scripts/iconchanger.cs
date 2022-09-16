using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class iconchanger : MonoBehaviour
{
    [SerializeField]
    EventSystem events;
    Image current;

    void Start()
    {
        current = GetComponent<Image>();
    }

    void Update()
    {
        //current = EventSystem.current.currentSelectedGameObject.gameObject.GetComponent<Image>();
        //Debug.Log(current);
    }




}
