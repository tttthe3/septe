using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Itemshortchekcer : MonoBehaviour
{
    [SerializeField]
    private Image lefticon;

    [SerializeField]
    private Image Downicon;

    [SerializeField]
    private Image Righticon;

    private Image currentimage;

    public static Itemshortchekcer Instance
    {
        get { return s_instance; }

    }
    protected static Itemshortchekcer s_instance;

    private void Start()
    {
        currentimage = GetComponent<Image>();
        if (s_instance == null)
        {
            s_instance = this;

        }
    }

    public void onclickleft()
    {
        if (Playerinput.Instance.Attack.Down)
        {
            currentimage = EventSystem.current.currentSelectedGameObject.gameObject.GetComponent<Image>();

            lefticon.sprite = currentimage.sprite;
        }
    }

    public void setleficon(Image icon)
    {
        lefticon = icon;
    }

}


