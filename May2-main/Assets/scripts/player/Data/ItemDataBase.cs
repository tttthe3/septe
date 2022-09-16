using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemDataBase : MonoBehaviour,IDataPersister
{
    //接触したら落ちてるアイテム側が持っている追加メソッドを実行する

    static protected ItemDataBase s_ItemBase;
    static public ItemDataBase Instance { get { return s_ItemBase; } }
    [SerializeField]
    private List<CreateItem> haveitemList = new List<CreateItem>();

    [SerializeField]
    private List<CreateItem> haveitemList_Use = new List<CreateItem>();

    public DataSettings dataSettings;
    private bool havetrigger = false;
    [System.Serializable]
    public class InventoryChecker
    {
        public string[] inventoryItems;
        public UnityEvent OnHasItem, OnDoesNotHaveItem;

        public bool CheckInventory(ItemDataBase inventory)
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
    void OnEnable()
    {
        PersistentDataManager.RegisterPersister(this);
    }

    void Awake()
    {
        
        s_ItemBase = this;
    }

    public List<CreateItem> GetItemList()
    {
        return haveitemList;
    }

    public List<CreateItem> GetUseItemList()
    {
        return haveitemList_Use;
    }

    public void AddItem(CreateItem Item)
    {

        haveitemList.Add(Item);
      
    }

    public void AddItem_Use(CreateItem Item)
    {

        haveitemList_Use.Add(Item);

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

    public bool HasItem_Use(string key)
    {
        for (int i = 0; i < haveitemList_Use.Count; i++)
        {
            Debug.Log(haveitemList_Use.Count);
            if (haveitemList_Use[i].getitemname().Contains(key))
            {
                havetrigger = true;

            }
            else
                havetrigger = false;

        }
        return havetrigger;


    }

    public CreateItem GetItemData(string key)
    {
        for (int i = 0; i < haveitemList.Count; i++)
        {

            if (haveitemList[i].getitemname().Contains(key))
            {
                return haveitemList[i];

            }

        }
        return null;

    }

    public int UseItemCount(string key)
    {
        int count = 0;
        for (int i = 0; i < haveitemList_Use.Count; i++)
        {
            if (haveitemList_Use[i].getitemname() == key)
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
        return 10 ;


    }

    public DataSettings GetDataSettings()
    {
        return dataSettings;
    }

    public void SetDataSettings(string dataTag, DataSettings.PersistenceType persistenceType)
    {
        dataSettings.dataTag = dataTag;
        dataSettings.persistenceType = persistenceType;
    }

    public Data SaveData()
    {
        return new Data<List<CreateItem>>(haveitemList);
    }

    public void LoadData(Data data)
    {
        Data<List<CreateItem>> inventoryData = (Data<List<CreateItem>>)data;
        foreach (var i in inventoryData.value)
            AddItem(i);
    }


}
