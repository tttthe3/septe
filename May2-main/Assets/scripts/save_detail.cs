using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using LitJson;
using TMPro;

public class save_detail : MonoBehaviour
{
    public TextMeshProUGUI stagename;
    public TextMeshProUGUI progress;
    public TextMeshProUGUI playtime;
    public TextMeshProUGUI nodata;
    public string datename;
    savainstance.PlayerInfo inform;

    public void dataset()
    {
        
        TextAsset texts = Resources.Load(datename) as TextAsset;
        Debug.Log(texts);
       inform = JsonMapper.ToObject<savainstance.PlayerInfo>(texts.text);
       
    }

   void Start()
    {
        dataset();
        
        if (inform == null)
        {
            stagename.text = "";
            progress.text = "";
            playtime.text = "";
            nodata.gameObject.SetActive(false);
        }
        else
        {
            nodata.gameObject.SetActive(false);
            stagename.text = inform.savepoint;
            progress.text = inform.flas.ToString();
            playtime.text = inform.playtime.ToString();
        }
    }


}
