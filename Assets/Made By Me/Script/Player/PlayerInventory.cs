using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    public PlayerStatus status;
    public ItemUI itemUI;
    public BoxItemUI boxItemUI;
    public PlayerItemUI playerItemUI;

    [SerializeField] ItemInformation[] basicItems;

    [SerializeField] ItemInformation[] EnergyUpgrade1Item;
    [SerializeField] ItemInformation[] EnergyUpgrade2Item;

    public ItemInformation[] items;
    public int[] itemsamount;
    public bool[] isEquipped;

    public int StartContainer = 8;
    public int CurrentContainer;

    public bool isInventoryFull;

    public int SheildBatteryLimit = 8;
    public int Energy1BatteryLimit = 30;
    public int Energy2BatteryLimit = 12;
    public int Energy3BatteryLimit = 9;

    private void Awake() {
        itemUI = FindObjectOfType<ItemUI>();
        playerItemUI = FindObjectOfType<PlayerItemUI>();
        boxItemUI = FindObjectOfType<BoxItemUI>();
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
            isEquipped[i] = false;
            if(items[i] == null) continue;
            if(!items[i].isEquipItem) continue;

            if((items[i].KeyItemCode + 1 == status.Energy || items[i].KeyItemCode -8 == status.Energy || items[i].KeyItemCode -11 == status.Energy) && status.Energy != 0)
            {
                isEquipped[i] = true;
            }else if(items[i].KeyItemCode - 3 == status.Sheild)
            {
                isEquipped[i] = true;
            }
        }
    }

    private void OnEnable() {
       bulletItem.Obtain_bullet_Item_Event += Obtain_bullet_Item;
       ExpansionItem.Obtain_Expansion_Item_Event += Obtain_Expansion_Item;
       EquipItem.Obtain_Equip_Item_Event += Obtain_Equip_Item;
       PotionItem.Obtain_potion_Item_Event += Obtain_Potion_Item;

       TabUI.discardItemEvent += DiscardItem;
       BoxUI.discardItemEvent += DiscardItem;
       TabUI.UsePotionEvent += UsedPotionItem;
       TabUI.CombineEvent += UpgradeItem;
       BoxUI.CombineEvent += UpgradeItem;

       TabUI.Interact_KeyItem_Success_Event += DiscardItem;

       BoxUI.BoxEvent += BoxInOut;

       KeyItem.inventoryExpandEvent += inventoryExpand;
    }

    private void OnDisable() {
        bulletItem.Obtain_bullet_Item_Event -= Obtain_bullet_Item;
        ExpansionItem.Obtain_Expansion_Item_Event -= Obtain_Expansion_Item;
        PotionItem.Obtain_potion_Item_Event -= Obtain_Potion_Item;

       TabUI.discardItemEvent -= DiscardItem;
       BoxUI.discardItemEvent -= DiscardItem;
       TabUI.UsePotionEvent -= UsedPotionItem;
       TabUI.CombineEvent -= UpgradeItem;
       BoxUI.CombineEvent -= UpgradeItem;

       BoxUI.BoxEvent -= BoxInOut;

       KeyItem.inventoryExpandEvent -= inventoryExpand;
    }

    private void inventoryExpand()
    {
        CurrentContainer += 2;
        GameManagerUI.instance.CurrentContainer += 2;
        CurrentContainer = Mathf.Clamp(CurrentContainer, 1, 16);

        itemUI.UpdateInventoryUI();
        playerItemUI.UpdateInventoryUI();
    }

    private void BoxInOut(bool flag, ItemInformation information, int amount, int currentindex)
    {
        if(flag)
        {
            DiscardItem(currentindex);
        }else
        {
            for(int i=0; i<CurrentContainer; i++)
            {
                if(items[i] == null)
                {
                    items[i] = information;
                    itemsamount[i] = amount;
                    break;
                }
            }

        }

        status.UpdateIngameUI();
        playerItemUI.UpdateInventoryUI();
        boxItemUI.UpdateBoxUI();
    }

    public void DiscardItem(int index)
    {
        Obtain_bullet_Item(items[index], -itemsamount[index]);

        items[index] = null;
        itemsamount[index] = 0;

        itemUI.UpdateInventoryUI();
        playerItemUI.UpdateInventoryUI();
        
    }

    public void DiscardItem(InteractiveDialog interactiveDialog, int index)
    {
        DiscardItem(index);
    }

    public void UsedPotionItem(int index, float damage)
    {
        status.HealthChange(-damage);

        DiscardItem(index);
        
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
                if(temp == 0) break;
            }
        }

        status.UpdateIngameUI();
        itemUI.UpdateInventoryUI();
        playerItemUI.UpdateInventoryUI();
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
        playerItemUI.UpdateInventoryUI();
    }

    public void UpgradeItem(ItemInformation combineStartItem, int combineStartItemIndex, ItemInformation combineEndItem, int combineEndIndex)
    {
        if(combineStartItem.isKeyItem)
        {
            for(int i=0; i < combineStartItem.combineItems.Length; i++)
            {
                if(combineStartItem.combineItems[i] == combineEndItem.KeyItemCode)
                {
                    items[combineStartItemIndex] =  combineStartItem.combinResultItems[i];
                    itemsamount[combineStartItemIndex] = 1;

                    if(combineStartItem.KeyItemCode == 6 || combineEndItem.KeyItemCode == 6)
                    {
                        status.SetEnergyEquipUpgrade(1,false, false, false);
                    }else if(combineStartItem.KeyItemCode == 7 || combineEndItem.KeyItemCode == 7)
                    {
                        status.SetEnergyEquipUpgrade(2,false, false, false);
                    }else if(combineStartItem.KeyItemCode == 8 || combineEndItem.KeyItemCode == 8)
                    {
                        status.SetEnergyEquipUpgrade(3,false, false, false);
                    }

                    break;
                }
            }

            DiscardItem(combineEndIndex);
        }else
        {
            if(combineStartItem.isPotion && combineEndItem.isPotion)
            {
                for(int i=0; i < combineStartItem.combineItems.Length; i++)
                {
                    if(combineStartItem.combineItems[i] == combineEndItem.NormalItemCode)
                    {
                        items[combineStartItemIndex] =  combineStartItem.combinResultItems[i];
                        itemsamount[combineStartItemIndex] = 1;
                        break;
                    }
                }
                DiscardItem(combineEndIndex);

            }else if(combineStartItem.isBullet && combineEndItem.isBullet)
            {
                int temp = 0;
                itemsamount[combineStartItemIndex] += itemsamount[combineEndIndex];

                if(combineStartItem.isSheild)
                {
                    if(itemsamount[combineStartItemIndex] > SheildBatteryLimit)
                    {
                        temp = itemsamount[combineStartItemIndex] - SheildBatteryLimit;
                        itemsamount[combineStartItemIndex] = SheildBatteryLimit;
                    }
                    
                }else if(combineStartItem.isEnergy1)
                {
                    if(itemsamount[combineStartItemIndex] > Energy1BatteryLimit)
                    {
                        temp = itemsamount[combineStartItemIndex] - Energy1BatteryLimit;
                        itemsamount[combineStartItemIndex] = Energy1BatteryLimit;
                    }

                }else if(combineStartItem.isEnergy2)
                {
                    if(itemsamount[combineStartItemIndex] > Energy2BatteryLimit)
                    {
                        temp = itemsamount[combineStartItemIndex] - Energy2BatteryLimit;
                        itemsamount[combineStartItemIndex] = Energy2BatteryLimit;
                    }

                }else if(combineStartItem.isEnergy3)
                {
                    if(itemsamount[combineStartItemIndex] > Energy3BatteryLimit)
                    {
                        temp = itemsamount[combineStartItemIndex] - Energy3BatteryLimit;
                        itemsamount[combineStartItemIndex] = Energy3BatteryLimit;
                    }

                }

                itemsamount[combineEndIndex] = temp;

                if(temp == 0)
                {
                    DiscardItem(combineEndIndex);
                }


            }
        }
        itemUI.UpdateInventoryUI();
        status.UpdateIngameUI();
        playerItemUI.UpdateInventoryUI();
    }

    public void Obtain_Potion_Item(ItemInformation itemInformation)
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
        playerItemUI.UpdateInventoryUI();
    }

    public void Obtain_bullet_Item(ItemInformation itemInformation, int amount)
    {
        if(amount > 0)
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
        
        if(itemInformation.isSheild && itemInformation.isBullet)
        {
            status.SheildStore += amount;
        }else if(itemInformation.isEnergy1 && itemInformation.isBullet)
        {
            status.EnergyStore[1] += amount;
        }else if(itemInformation.isEnergy2 && itemInformation.isBullet)
        {
            status.EnergyStore[2] += amount;
        }else if(itemInformation.isEnergy3 && itemInformation.isBullet)
        {
            status.EnergyStore[3] += amount;
        }
        
        itemUI.UpdateInventoryUI();
        status.UpdateIngameUI();
        playerItemUI.UpdateInventoryUI();
        
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
        playerItemUI.UpdateInventoryUI();
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
        playerItemUI.UpdateInventoryUI();
    }

}
