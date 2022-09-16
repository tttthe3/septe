using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;




public class frei : People
{
    private Dictionary<string, string> command;

     public override void AddWords()
    {
        Debug.Log("call");
        //getword();
    }

    public override void Gender()
    {
        
        man = false;
        audioPitch = 2.5f;
    }

    public void getword()
    {
        string path = "Assets/Resources/sampletext.txt";
        using(var fs = new StreamReader(path, System.Text.Encoding.GetEncoding("UTF-8")))

            while (fs.Peek() != -1) {
                talkingWords.Add(fs.ReadLine());
            }

    }

    public override void writeflag()
    {
        

    }
}