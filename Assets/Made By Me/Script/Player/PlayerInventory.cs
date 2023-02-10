using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    ItemUI itemUI;

    [SerializeField] ItemInformation[] basicItems;
    public ItemInformation[] items;
    public int[] itemsamount;

    public int StartContainer = 8;
    public int CurrentContainer;

    public const int SheildBatteryLimit = 8;
    public const int Energy1BatteryLimit = 30;
    public const int Energy2BatteryLimit = 12;
    public const int Energy3BatteryLimit = 9;

    private void Awake() {
        itemUI = FindObjectOfType<ItemUI>();

        items = new ItemInformation[16];
        itemsamount = new int[16];
        CurrentContainer = StartContainer;

        for(int i=0; i<basicItems.Length; i++)
        {
            items[i] = basicItems[i];
        }
    }

    private void Start() {
        GameManagerUI.instance.CurrentContainer = StartContainer;
    }

    private void OnEnable() {
       bulletItem.Obtain_bullet_Item_Event += Obtain_bullet_Item;
       ExpansionItem.Obtain_Expansion_Item_Event += Obtain_Expansion_Item;
    }

    public void Obtain_bullet_Item(ItemInformation itemInformation, int amount)
    {
        for(int i=0; i<CurrentContainer; i++)
        {
            if(items[i] == null)
            {
                items[i] = itemInformation;
                itemsamount[i] = amount;
                break;
            }
            
        }

        itemUI.UpdateInventoryUI();
    }

    public void Obtain_Expansion_Item(ItemInformation itemInformation, int amount)
    {
        for(int i=0; i<CurrentContainer; i++)
        {
            if(items[i] == null)
            {
                items[i] = itemInformation;
                break;
            }
            
        }

        itemUI.UpdateInventoryUI();
    }

}
