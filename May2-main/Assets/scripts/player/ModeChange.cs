using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ModeChange : MonoBehaviour
{

    public CanvasGroup Leftcanvas;
    public CanvasGroup Rightcanvas;
    public CanvasGroup Currentcanvas;
    [SerializeField]
    private GameObject firstSelect;

    GameObject sekect;
    private Image images;

    void OnEnable()
    {
        StartCoroutine(reboot());


        Debug.Log(firstSelect);

    }

    void OnDisable()
    {

        EventSystem.current.SetSelectedGameObject(null);

    }


    IEnumerator reboot()
    {
        yield return new WaitForSecondsRealtime(1f);

        EventSystem.current.SetSelectedGameObject(firstSelect);

        yield return new WaitForFixedUpdate();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            OnClickLeft();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            OnClickRight();
        }



    }
    public void OnClickLeft()
    {

        Currentcanvas.gameObject.SetActive(false);
        Leftcanvas.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(firstSelect);



    }

    public void OnClickRight()
    {

        Currentcanvas.gameObject.SetActive(false);
        Rightcanvas.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(firstSelect);
    }




}
