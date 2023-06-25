using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class PlayerInventroyLogic 
{
    private PlayerStatus status;
    public ItemUI itemUI;
    public BoxItemUI boxItemUI;
    public PlayerItemUI playerItemUI;

    private ItemInformation[] items;
    private int[] itemsamount;
    private bool[] isEquipped;

    public bool isInventoryFull;

    public int DefaultContainerSize = 8;
    public int CurrentContainerSize;

    public int SheildBatteryLimit = 8;
    public int Energy1BatteryLimit = 30;
    public int Energy2BatteryLimit = 12;
    public int Energy3BatteryLimit = 9;

    public PlayerInventroyLogic(ItemInformation[] items , int[] itemsamount, bool[] isEquipped )
    {
        this.items = items;
        this.itemsamount = itemsamount;
        this.isEquipped = isEquipped;
    }

/////////////////////// Logic Method /////////////////////////////////////////////////////////////////
/////////////////////// Logic Method /////////////////////////////////////////////////////////////////
/////////////////////// Logic Method /////////////////////////////////////////////////////////////////

    public void LoadSave(PlayerInventorySave save)
    {
        for(int i=0; i< save.itemsPath.Length; i++)
        {
            items[i] = AssetDatabase.LoadAssetAtPath<ItemInformation>(save.itemsPath[i]);
        }
        Array.Copy( save.itemsamount , itemsamount, save.itemsamount.Length);
        Array.Copy( save.isEquipped , isEquipped, save.isEquipped.Length);

        CurrentContainerSize = save.CurrentContainer;
        GameManagerUI.CurrentContainer = CurrentContainerSize;

        isInventoryFull = save.isInventoryFull;
    }


    private bool CheckInventoryFull()
    {
        for(int i=0; i<CurrentContainerSize; i++)
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
        for(int i=0; i<CurrentContainerSize; i++)
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
            for(int i=0; i<CurrentContainerSize; i++)
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
            for(int i=0; i<CurrentContainerSize; i++)
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
            for(int i=0; i<CurrentContainerSize; i++)
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

        UI.instance.inGameUI.UpdateIngameUI();
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


        for(int i=0; i<CurrentContainerSize; i++)
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

        UI.instance.inGameUI.UpdateIngameUI();
        itemUI.UpdateInventoryUI();
        playerItemUI.UpdateInventoryUI();
    }

    public void DiscardItem(int index)
    {

        Obtain_bullet_Item(items[index], -itemsamount[index]);

        items[index] = null;
        itemsamount[index] = 0;

        UI.instance.inGameUI.UpdateIngameUI();
        itemUI.UpdateInventoryUI();
        playerItemUI.UpdateInventoryUI();
        
    }

    public void DiscardItem(InteractiveDialog interactiveDialog, int index)
    {
        DiscardItem(index);
    }

    public void UsedPotionItem(int index, float damage)
    {
        status.HealthChangeDefaultMinus(-damage);

        DiscardItem(index);
        
    }

    private void BoxInOut(bool flag, ItemInformation information, int amount, int currentindex)
    {
        if(flag) // 인벤토리에서 박스로 나감
        {
            DiscardItem(currentindex);
        }else // 박스에서 인벤토리로 들어옴
        {       
            if(information.isBullet)
            {
                Obtain_bullet_Item(information, amount);
            }else if(information.isEquipItem)
            {
                Obtain_Equip_Item(information, information.KeyItemCode);
            }else
            {
                for(int i=0; i<CurrentContainerSize; i++)
                {
                if(items[i] == null)
                {
                    items[i] = information;
                    itemsamount[i] = amount;
                    break;
                }
                }
            }

        }

        playerItemUI.UpdateInventoryUI();
        boxItemUI.UpdateBoxItemUI();
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
        UI.instance.inGameUI.UpdateIngameUI();
        playerItemUI.UpdateInventoryUI();
    }


    public void Obtain_Potion_Item(ItemInformation itemInformation)
    {
        Debug.Log("Obtain_Potion_Item : " + itemInformation.ItemName);
        for(int i=0; i<CurrentContainerSize; i++)
        {
            if(items[i] == null)
            {
                items[i] = itemInformation;
                itemsamount[i] = 1;
                break;
            }
        }

        itemUI.UpdateInventoryUI();
        UI.instance.inGameUI.UpdateIngameUI();
        playerItemUI.UpdateInventoryUI();
    }

    public void Obtain_bullet_Item(ItemInformation itemInformation, int amount)
    {
        Debug.Log("Obtain_bullet_Item : " + itemInformation.ItemName + " amount : " + amount);
        if(amount > 0)
        {
            for(int i=0; i<CurrentContainerSize; i++)
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
        UI.instance.inGameUI.UpdateIngameUI();
        playerItemUI.UpdateInventoryUI();
        
    }

    public void Obtain_Expansion_Item(ItemInformation itemInformation)
    {
        Debug.Log("Obtain_Expansion_Item : " + itemInformation.ItemName);
        for(int i=0; i<CurrentContainerSize; i++)
        {
            if(items[i] == null)
            {
                items[i] = itemInformation;
                itemsamount[i] = 1;
                break;
            }
        }
        itemUI.UpdateInventoryUI();
        UI.instance.inGameUI.UpdateIngameUI();
        playerItemUI.UpdateInventoryUI();
    }

    public void Obtain_RealKey_Item(ItemInformation itemInformation)
    {
        Debug.Log("Obtain_RealKey_Item : " + itemInformation.ItemName);
        for(int i=0; i<CurrentContainerSize; i++)
        {
            if(items[i] == null)
            {
                items[i] = itemInformation;
                itemsamount[i] = 1;
                break;
            }
        }
        itemUI.UpdateInventoryUI();
        UI.instance.inGameUI.UpdateIngameUI();
        playerItemUI.UpdateInventoryUI();
    }


    public void Obtain_Equip_Item(ItemInformation itemInformation, int KeyItemCode)
    {
        if(!itemInformation.isEquipItem) return;

        Debug.Log("Obtain_Equip_Item : " + itemInformation.ItemName);

        for(int i=0; i<CurrentContainerSize; i++)
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
        UI.instance.inGameUI.UpdateIngameUI();
        playerItemUI.UpdateInventoryUI();
    }

    private void inventoryExpand()
    {
        CurrentContainerSize += 2;
        GameManagerUI.CurrentContainer += 2;
        CurrentContainerSize = Mathf.Clamp(CurrentContainerSize, 1, 16);

        itemUI.UpdateInventoryUI();
        playerItemUI.UpdateInventoryUI();
    }

/////////////////////// Logic Method /////////////////////////////////////////////////////////////////
/////////////////////// Logic Method /////////////////////////////////////////////////////////////////
/////////////////////// Logic Method /////////////////////////////////////////////////////////////////



////// Monobehavior Method //////////////////////////////////////////////////////////////////////////////////
////// Monobehavior Method //////////////////////////////////////////////////////////////////////////////////
////// Monobehavior Method //////////////////////////////////////////////////////////////////////////////////

    public void Awake()
    {
        // 일단 기본 값으로 넣어놓긴 하는 데, 저장 파일이 있다 면 해당 크기로 덮여짐
        CurrentContainerSize = DefaultContainerSize; 
        GameManagerUI.CurrentContainer = DefaultContainerSize;
        // 일단 기본 값으로 넣어놓긴 하는 데, 저장 파일이 있다 면 해당 크기로 덮여짐
    }

    // UI instance에서 가져오는 것은 awake 에서 적용시, 아직 awake 작업이 덜 되어서 null을 가져올 수 도 있으니 안됨
    public void Start() {
        itemUI = UI.instance.tabUI.itemUI;
        boxItemUI = UI.instance.boxUI.boxItemUI;
        playerItemUI = UI.instance.boxUI.playerItemUI;

        status = Player1.instance.playerStatus;
    }

    public void Update()
    {
        isInventoryFull = CheckInventoryFull();
        CheckInventoryEquipped();
    }

    ///////////////// delegate Method ///////////////////
    public void delegateFunctions(){
       GameManager.EventManager.Obtain_bullet_Item_Event += Obtain_bullet_Item;
       GameManager.EventManager.Obtain_Expansion_Item_Event += Obtain_Expansion_Item;
       GameManager.EventManager.Obtain_RealKey_Item_Event += Obtain_RealKey_Item;
       GameManager.EventManager.Obtain_Equip_Item_Event += Obtain_Equip_Item;
       GameManager.EventManager.Obtain_potion_Item_Event += Obtain_Potion_Item;

       GameManager.EventManager.discardItemEvent += DiscardItem;
       GameManager.EventManager.UsePotionEvent += UsedPotionItem;
       GameManager.EventManager.CombineEvent += UpgradeItem;

       GameManager.EventManager.Interact_KeyItem_Success_Event += DiscardItem;

       GameManager.EventManager.BoxEvent += BoxInOut;

       GameManager.EventManager.inventoryExpandEvent += inventoryExpand;
    }

    public void removeFunctions()
    {
        GameManager.EventManager.Obtain_bullet_Item_Event -= Obtain_bullet_Item;
        GameManager.EventManager.Obtain_Expansion_Item_Event -= Obtain_Expansion_Item;
        GameManager.EventManager.Obtain_RealKey_Item_Event -= Obtain_RealKey_Item;
        GameManager.EventManager.Obtain_Equip_Item_Event -= Obtain_Equip_Item;
        GameManager.EventManager.Obtain_potion_Item_Event -= Obtain_Potion_Item;

        GameManager.EventManager.discardItemEvent -= DiscardItem;
        GameManager.EventManager.UsePotionEvent -= UsedPotionItem;
        GameManager.EventManager.CombineEvent -= UpgradeItem;

        GameManager.EventManager.Interact_KeyItem_Success_Event -= DiscardItem;

        GameManager.EventManager.BoxEvent -= BoxInOut;

        GameManager.EventManager.inventoryExpandEvent -= inventoryExpand;
    }

    ///////////////// delegate Method ///////////////////

////// Monobehavior Method //////////////////////////////////////////////////////////////////////////////////
////// Monobehavior Method //////////////////////////////////////////////////////////////////////////////////
////// Monobehavior Method //////////////////////////////////////////////////////////////////////////////////


}
