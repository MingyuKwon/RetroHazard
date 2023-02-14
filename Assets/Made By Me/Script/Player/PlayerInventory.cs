using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    public PlayerStatus status;
    public ItemUI itemUI;

    [SerializeField] ItemInformation[] basicItems;
    public ItemInformation[] items;
    public int[] itemsamount;
    public bool[] isEquipped;

    public int StartContainer = 8;
    public int CurrentContainer;

    public bool isInventoryFull;

    public const int SheildBatteryLimit = 8;
    public const int Energy1BatteryLimit = 30;
    public const int Energy2BatteryLimit = 12;
    public const int Energy3BatteryLimit = 9;

    private void Awake() {
        itemUI = FindObjectOfType<ItemUI>();
        status = transform.parent.GetComponentInChildren<PlayerStatus>();

        items = new ItemInformation[16];
        itemsamount = new int[16];
        isEquipped = new bool[16];
        CurrentContainer = StartContainer;

        for(int i=0; i<basicItems.Length; i++)
        {
            items[i] = basicItems[i];
        }
    }

    private void Start() {
        GameManagerUI.instance.CurrentContainer = StartContainer;
    }

    private void Update() {
        isInventoryFull = CheckInventoryFull();
        CheckInventoryEquipped();
    }

    private bool CheckInventoryFull()
    {
        for(int i=0; i<CurrentContainer; i++)
        {
            if(items[i] == null)
            {
                return false;
            }
            
        }
        return true;
    }

    private void CheckInventoryEquipped()
    {
        for(int i=0; i<CurrentContainer; i++)
        {
            if(items[i] == null) continue;
            if(!items[i].isEquipItem) continue;

            if(items[i].KeyItemCode + 1 == status.Energy || items[i].KeyItemCode - 3 == status.Sheild)
            {
                isEquipped[i] = true;
            }else
            {
                isEquipped[i] = false;
            }
        }
    }

    private void OnEnable() {
       bulletItem.Obtain_bullet_Item_Event += Obtain_bullet_Item;
       ExpansionItem.Obtain_Expansion_Item_Event += Obtain_Expansion_Item;
       EquipItem.Obtain_Equip_Item_Event += Obtain_Equip_Item;

       TabUI.discardItemEvent += DiscardItem;
    }

    private void OnDisable() {
        bulletItem.Obtain_bullet_Item_Event += Obtain_bullet_Item;
        ExpansionItem.Obtain_Expansion_Item_Event += Obtain_Expansion_Item;
    }

    public void DiscardItem(int index)
    {
        Obtain_bullet_Item(items[index], -itemsamount[index]);

        items[index] = null;
        itemsamount[index] = 0;

        itemUI.UpdateInventoryUI();
        
    }


    private void EnergyReloadMacro(int n)
    {
        status.EnergyMaganize[status.Energy] += n;
        status.EnergyStore[status.Energy] -= n;
    }

    public void EnergyReload()
    {
        if(status.Energy == 0) return;
        
        int temp = 0;
        temp = status.EnergyMaganizeMaximum[status.Energy];
        temp -= status.EnergyMaganize[status.Energy];

        if(temp > status.EnergyStore[status.Energy])
        {
            temp = status.EnergyStore[status.Energy];
        } // up to , temp is determining how many bullet to reload 

        if(status.Energy == 1)
        {
            for(int i=0; i<CurrentContainer; i++)
            {
                if(items[i] == null) continue;
                if(items[i].isBullet && items[i].isEnergy1)
                {
                    if(temp >= itemsamount[i])
                    {
                        EnergyReloadMacro(itemsamount[i]);
                        temp -= itemsamount[i];
                        itemsamount[i] = 0;
                        items[i] = null;
                    }else
                    {
                        EnergyReloadMacro(temp);
                        itemsamount[i] -= temp;
                        temp = 0;
                    }
                }
                if(temp == 0) break;
            }

        }else if(status.Energy == 2)
        {
            for(int i=0; i<CurrentContainer; i++)
            {
                if(items[i] == null) continue;
                if(items[i].isBullet && items[i].isEnergy2)
                {
                    if(temp >= itemsamount[i])
                    {
                        EnergyReloadMacro(itemsamount[i]);
                        temp -= itemsamount[i];
                        itemsamount[i] = 0;
                        items[i] = null;
                    }else
                    {
                        EnergyReloadMacro(temp);
                        itemsamount[i] -= temp;
                        temp = 0;
                    }
                }
                if(temp == 0) break;
            }

        }else if(status.Energy == 3)
        {
            for(int i=0; i<CurrentContainer; i++)
            {
                if(items[i] == null) continue;
                if(items[i].isBullet && items[i].isEnergy3)
                {
                    if(temp >= itemsamount[i])
                    {
                        EnergyReloadMacro(itemsamount[i]);
                        temp -= itemsamount[i];
                        itemsamount[i] = 0;
                        items[i] = null;
                    }else
                    {
                        EnergyReloadMacro(temp);
                        itemsamount[i] -= temp;
                        temp = 0;
                    }
                }
                if(temp <= 0) break;
            }
        }
        status.UpdateIngameUI();
        itemUI.UpdateInventoryUI();
    }

    public void SheildReLoad()
    {
        if(status.Sheild == 3) return;

        float temp = 0;
        temp = status.SheildMaganizeMaximum[status.Sheild];
        temp -= status.SheildMaganize[status.Sheild];

        if(temp > status.SheildStore)
        {
            temp = status.SheildStore;
        } // up to , temp is determining how many bullet to reload 


        for(int i=0; i<CurrentContainer; i++)
        {
            if(items[i] == null) continue;
            if(items[i].isBullet && items[i].isSheild)
            {
                if( temp >= itemsamount[i])
                {
                    status.SheildDurabilityChange(-itemsamount[i]);
                    status.SheildStore -= itemsamount[i];

                    temp -= itemsamount[i];
                    itemsamount[i] = 0;
                    items[i] = null;
                }else
                {
                    status.SheildDurabilityChange(-temp);
                    status.SheildStore -= Mathf.CeilToInt(temp);
                    if(Mathf.CeilToInt(temp) == itemsamount[i])
                    {
                        items[i] = null;
                    }
                    itemsamount[i] -= Mathf.CeilToInt(temp);

                    temp = 0;
                }
            }
            if(temp <= 0) break;
        }

        status.UpdateIngameUI();
        itemUI.UpdateInventoryUI();
    }

    public void Obtain_bullet_Item(ItemInformation itemInformation, int amount)
    {
        if(amount >= 0)
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
        }
        

        if(itemInformation.isSheild)
        {
            status.SheildStore += amount;
        }else if(itemInformation.isEnergy1)
        {
            status.EnergyStore[1] += amount;
        }else if(itemInformation.isEnergy2)
        {
            status.EnergyStore[2] += amount;
        }else if(itemInformation.isEnergy3)
        {
            status.EnergyStore[3] += amount;
        }
        

        itemUI.UpdateInventoryUI();
        status.UpdateIngameUI();
    }

    public void Obtain_Expansion_Item(ItemInformation itemInformation, int amount)
    {
        for(int i=0; i<CurrentContainer; i++)
        {
            if(items[i] == null)
            {
                items[i] = itemInformation;
                itemsamount[i] = 1;
                break;
            }
        }

        itemUI.UpdateInventoryUI();
        status.UpdateIngameUI();
    }

    public void Obtain_Equip_Item(ItemInformation itemInformation, int KeyItemCode)
    {
        if(!itemInformation.isEquipItem) return;

        for(int i=0; i<CurrentContainer; i++)
        {
            if(items[i] == null)
            {
                items[i] = itemInformation;
                itemsamount[i] = 1;
                break;
            }
            
        }
        

        if(itemInformation.KeyItemCode <3)
        {
            status.SetEnergyEquipUpgrade(itemInformation.KeyItemCode +1, true, true, true);
        }else
        {
            status.SetSheildEquip(itemInformation.KeyItemCode-3, true);
        }
        

        itemUI.UpdateInventoryUI();
        status.UpdateIngameUI();
    }

}
