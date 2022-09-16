using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class save_point : MonoBehaviour
{

    public CanvasGroup SaveUI;
    public Image saveC;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            saveC.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.A))
            {

            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SaveUI.gameObject.SetActive(false);
        }
    }

}
