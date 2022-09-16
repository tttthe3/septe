using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Event_Wrapper : MonoBehaviour
{
    static public Event_Wrapper Instance { get { return s_Event_Wrapper; } }
    static protected Event_Wrapper s_Event_Wrapper;

    // Start is called before the first frame update
    void Start()
    {
        s_Event_Wrapper = this;
    }

    public void TextChange(string targetname,string nexttext)  //対象のオブジェクトを探し、同じシーン内にあれば、所持しているテキストを書き換え(テキストファイル)する。なければ次シーンに持ち越しロード。それでもなければ
    {
        
        ;
        
        //リセット機能
        var target = GameObject.Find(targetname);
        if (target == null)
            return;
        target.GetComponent<People>().talkingWords.Clear();
        string path = "Assets/Resources/"+nexttext;
        using (var fs = new StreamReader(path, System.Text.Encoding.GetEncoding("UTF-8")))

            while (fs.Peek() != -1)
            {
               target.GetComponent<People>().talkingWords.Add(fs.ReadLine());
            }
        
    }





    public void Initobject() //対象のオブジェクトをシーン内の特定座標に生成する。リソースから
    {


    }

    public void Destroyobject()//対象のオブジェクトをシーン内の特定座標を消す。
    {


    }


}
