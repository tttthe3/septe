using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using System.IO;
public class TextLoader : MonoBehaviour
{
    public TextMeshProUGUI texts;
    public string folder;
    public List<string> talkingWords = new List<string>();
    // Start is called before the first frame update
    void Start()
    {

        //string path = "Assets/Resources/fast1.txt";
        string path = folder;
        using (var fs = new StreamReader(path, System.Text.Encoding.GetEncoding("UTF-8"))) 
        while (fs.Peek() != -1)
        {
            talkingWords.Add(fs.ReadLine());
        }
        texts.text = talkingWords[0];
    }

     public void proceedtalk()
    {
       
        
        texts.text = talkingWords[0];
        talkingWords.RemoveAt(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
