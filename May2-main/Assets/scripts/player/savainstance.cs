using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using LitJson;
using System.Linq;
public class savainstance : MonoBehaviour
{


    public static savainstance Instance
    {
        get
        {
            if (instance != null)
                return instance;
            instance = FindObjectOfType<savainstance>();
            if (instance != null)
                return instance;

            Create();
            return instance;
        }
    }

    protected static savainstance instance;
    protected static bool quitting;

    public static savainstance Create()
    {
        GameObject dataManagerGameObject = new GameObject("savainstance");

        instance = dataManagerGameObject.AddComponent<savainstance>();
        return instance;
    }

    static protected savainstance s_Saveinstance;

    PlayerInfo playerdate = new PlayerInfo();

    const string datadir = "Assets/Resources/";
    string datadir2 ;
    const string SAVE_FILE = "save_data.json";
    [System.Serializable]
    public class PlayerInfo
    {
        public Dictionary<string, int> Itemstate = new Dictionary<string, int>();
        public float currenhealth;
        public float maxhealth;
        public int restSpoint;
        public string loadscene;
        public string savepoint;
        public Game_managers.Bossflag flas;
        public float playtime;
        public int camerapripriority;
        public string cameraname;
    }

    public void Start()
    {
        datadir2 = Application.dataPath;
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
        instance = this;

    }

    private void FixedUpdate()
    {
        playerdate.playtime += Time.deltaTime;
    }


    public PlayerInfo Getdate()
    {
        return playerdate;
    }

    public void saveupdate()
    {
        playerdate.currenhealth = 15;
        playerdate.maxhealth = 15;
        for (int i = 0; i < ItemDataBase.Instance.GetItemList().Count; i++)
            playerdate.Itemstate.Add(ItemDataBase.Instance.GetItemList()[i].getitemname(), ItemDataBase.Instance.GetItemList()[i].Getitemlevel());
        playerdate.loadscene = Charactercontrolelr.CCInstance.GetSavepoint1();
        playerdate.savepoint = Charactercontrolelr.CCInstance.GetSavepoint2();
        playerdate.flas = Game_managers.Instance.GetProgress();
        GetCamerapriority();
    }

    public void GetCamerapriority()
    {
        int nowpriority = 0;
        Cinemachine.CinemachineVirtualCamera nowcamera;
        GameObject parentcamera = GameObject.Find("Cameras");
        var camera = parentcamera.GetComponentsInChildren<Transform>().Where(t => t.tag == "Vcam");
        foreach (Transform one in camera)
        {
            one.GetComponent<Cinemachine.CinemachineVirtualCamera>();
            if (nowpriority < one.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority)
            {
                Debug.Log(one.gameObject.name);
                nowpriority = one.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority;
                nowcamera = one.GetComponent<Cinemachine.CinemachineVirtualCamera>();
                one.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = 2;
                playerdate.cameraname = one.gameObject.name;
                playerdate.camerapripriority = 3;
            }
            else { one.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = 1; }
        }

    }


    public void saveset(string dataname)
    {
        string json;
        json = LitJson.JsonMapper.ToJson(playerdate);
        StreamWriter writer = new StreamWriter(datadir2 +"/"+ dataname, false);
        writer.WriteLine(json);
        writer.Flush();
        writer.Close();
        Debug.Log(json);
    }

    public void loadsave(string dataname)
    {
        //string loadjson = Resources.Load("sava_data").ToString();
        string texts2 = File.ReadAllText(datadir2+"/"+dataname);
        TextAsset texts = Resources.Load(dataname) as TextAsset;
        Debug.Log(texts);
        playerdate = JsonMapper.ToObject<PlayerInfo>(texts2);
        if (playerdate.savepoint == null)
        {
            playerdate.loadscene = "directo_op";
            playerdate.savepoint = "stage1";
        }
        Debug.Log(Instance.playerdate.savepoint);
    }

    public void loadcamera()
    {
        int nowpriority = 0;
        Cinemachine.CinemachineVirtualCamera nowcamera;
        GameObject parentcamera = GameObject.Find("Cameras");
        var camera = parentcamera.GetComponentsInChildren<Transform>().Where(t => t.tag == "Vcam");
        foreach (Transform one in camera)
        {
            if (one.gameObject.name == playerdate.cameraname)
            {
                one.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = 5;
            }
            else
            {
                one.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = 1;
            }
        }
            GameObject obj = GameObject.Find("Cameras/" + playerdate.cameraname);
            Debug.Log(obj);
            obj.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = 12;
        
    }
}
