using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_managers : MonoBehaviour
{
    static protected Game_managers s_Gamemanager;
    static public Game_managers Instance { get { return s_Gamemanager; } }
    // Start is called before the first frame update
    public enum Bossflag
    {
        zero, first, second, third,
    }

    public Bossflag flag;

    private void Awake()
    {
        s_Gamemanager = this;
        Debug.Log(flag);       
        flag++;
        Debug.Log(flag);
    }

    public Bossflag GetProgress()
    {
        return flag;

    }

    public void upflag()
    {
       ++flag;

    }
}
