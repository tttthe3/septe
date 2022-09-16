using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UseItemDataBase : MonoBehaviour
{
    //接触したら落ちてるアイテム側が持っている追加メソッドを実行する

    static protected UseItemDataBase s_ItemBase;
    static public UseItemDataBase Instance { get { return s_ItemBase; } }
    [SerializeField]
    private List<CreateItem2> haveitemList = new List<CreateItem2>();

    public DataSettings dataSettings;
    private bool havetrigger = false;
    [System.Serializable]
    public class InventoryChecker
    {
        public string[] inventoryItems;
        public UnityEvent OnHasItem, OnDoesNotHaveItem;

        public bool CheckInventory(UseItemDataBase inventory)
        {
            if (inventory != null)
            {
                for (var i = 0; i < inventoryItems.Length; i++)
                {
                    if (!inventory.HasItem(inventoryItems[i]))
                    {
                        OnDoesNotHaveItem.Invoke();
                        return false;
                    }
                }
                OnHasItem.Invoke();
                return true;
            }
            return false;
        }
    }
   
    void Awake()
    {

        s_ItemBase = this;
    }

    public List<CreateItem2> GetItemList()
    {
        return haveitemList;
    }

    public void AddItem(CreateItem2 Item)
    {

        haveitemList.Add(Item);

    }

    public void levelup()
    {

    }

    public bool HasItem(string key)
    {
        for (int i = 0; i < haveitemList.Count; i++)
        {
            if (haveitemList[i].getitemname().Contains(key))
            {
                havetrigger = true;

            }
            else
                havetrigger = false;

        }
        return havetrigger;


    }

    public int UseItemCount(string key)
    {
        int count = 0;
        for (int i = 0; i < haveitemList.Count; i++)
        {
            if (haveitemList[i].getitemname()==key)
            {
                count++;


            }
           

        }
        return count;


    }

    public int HasItemnumber(string key)
    {
        for (int i = 0; i < haveitemList.Count; i++)
        {
            if (haveitemList[i].getitemname().Contains(key))
            {
                return i;

            }

        }
        return 10;


    }



}
