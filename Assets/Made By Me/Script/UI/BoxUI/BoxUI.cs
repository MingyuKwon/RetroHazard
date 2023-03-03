using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using Sirenix.OdinInspector;


public class BoxUI : MonoBehaviour
{
    static public event Action<int> discardItemEvent;
    static public event Action<ItemInformation , int , ItemInformation, int> CombineEvent; // Combine start Item, combine start index, select Item, selected index
    static public event Action<bool ,ItemInformation , int, int> BoxEvent; // true : inventory -> box, false : box -> inventory


    private Player player;
    PlayerStatus status;

    public bool inputOk = false;

    public bool isShowing = false;

    public int currentWindowLayer = 0;
    public bool isBox = false;
    public bool isInventory = true;

    public int boxItemIndex = 0;
    public int playerItemIndex = 0;

    public int previousBoxItemIndex = -1;
    public int previousPlayerItemIndex = 0;

    Image background;
    public BoxItemUI boxItemUI;
    public PlayerItemUI playerItemUI;
    ItemExplainUI itemExplainUI;

    public ItemInformation combineStartItem = null;
    public int combineStartItemIndex;

    private void OnEnable() {
        IinteractiveUI.interact_Input_Rlease_Event += InputGetBack;
        boxItemIndex = -1;
        playerItemIndex = 0;
        isBox = false;
        isInventory = true;

        boxItemUI.backgroundPanel.color = boxItemUI.unSelectColor;
        playerItemUI.backgroundPanel.color = playerItemUI.selectColor;

        transform.parent.GetComponent<UI>().MouseCursor(true);
    }

    private void OnDisable() {
        IinteractiveUI.interact_Input_Rlease_Event -= InputGetBack;
        currentWindowLayer = 0;

        transform.parent.GetComponent<UI>().MouseCursor(false);
    }
    private void InputGetBack()
    {
        if(this.gameObject.activeInHierarchy != true) return;
        inputOk = true;
    }

    private void Awake() {
        status = FindObjectOfType<PlayerStatus>();

        player = ReInput.players.GetPlayer(0);

        player.AddInputEventDelegate(EnterPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Enter");
        player.AddInputEventDelegate(BackPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Back");
        player.AddInputEventDelegate(UpPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "SelectUp");
        player.AddInputEventDelegate(DownPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "SelectDown");
        player.AddInputEventDelegate(RightPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "SelectRight");
        player.AddInputEventDelegate(LeftPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "SelectLeft");
        player.AddInputEventDelegate(LeftTab, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Move to Left Tab");
        player.AddInputEventDelegate(RightTab, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Move to Right Tab");

        background = transform.GetChild(0).GetComponent<Image>();
        itemExplainUI = GetComponentInChildren<ItemExplainUI>();
        boxItemUI = GetComponentInChildren<BoxItemUI>();
        playerItemUI = GetComponentInChildren<PlayerItemUI>();
    }

    public void LeftTab(InputActionEventData data)
    {
        if(currentWindowLayer == 1) return;
        if(isInventory)
        {   
            previousPlayerItemIndex = playerItemIndex;
            playerItemIndex = -1;
            boxItemIndex = previousBoxItemIndex;
            isBox = true;
            isInventory = false;

            boxItemUI.backgroundPanel.color = boxItemUI.selectColor;
            playerItemUI.backgroundPanel.color = playerItemUI.unSelectColor;
        }
        
    }

    public void RightTab(InputActionEventData data)
    {
        if(currentWindowLayer == 1) return;
        if(isBox)
        {
            previousBoxItemIndex = boxItemIndex;
            boxItemIndex = -1;
            playerItemIndex = previousPlayerItemIndex;
            isBox = false;
            isInventory = true;

            boxItemUI.backgroundPanel.color = boxItemUI.unSelectColor;
            playerItemUI.backgroundPanel.color = playerItemUI.selectColor;
        }
        
    }

    IEnumerator showInteractiveDialogDelay()
    {
        yield return new WaitForEndOfFrame();
        GameManagerUI.instance.VisualizeInteractiveUI(true);
    }

    public void EnterPressed(InputActionEventData data)
    {
        if(!inputOk) return;

        if(isBox)
        {
            if(boxItemUI.playerItemBox.items[boxItemUI.currentindex] == null) return;

            if(currentWindowLayer == 0)
            {
                currentWindowLayer++;
            }
            else if(currentWindowLayer == 1)
            {
                if(playerItemUI.isInventoryFull)
                {
                    inputOk = false;
                    GameManagerUI.instance.SetInteractiveDialogText(new string[] {"<b><color=red>Your inventory is full!</color></b> \n\n\nCan't move this item from box to inventory"});
                    StartCoroutine(showInteractiveDialogDelay());
                    currentWindowLayer--;
                    return;
                }

                ItemInformation from = boxItemUI.playerItemBox.items[boxItemIndex];
                int fromAmount = boxItemUI.playerItemBox.itemsamount[boxItemIndex];
                BoxEvent.Invoke(false, from, fromAmount, boxItemIndex);
                    
                currentWindowLayer--;
            }
        }else
        {
            if(playerItemUI.playerInventory.items[playerItemUI.currentindex] == null) return;

            if(currentWindowLayer == 0)
            {
                currentWindowLayer++;
            }
            else if(currentWindowLayer == 1)
            {
                if(playerItemUI.itemContainers[playerItemUI.currentindex].selectIndex == 0)
                {
                    if(boxItemUI.isBoxFull)
                    {
                        inputOk = false;
                        GameManagerUI.instance.SetInteractiveDialogText(new string[] {"<b><color=red>Your Box is full!</color></b> \n\n\nCan't move this item from inventory to box"});
                        StartCoroutine(showInteractiveDialogDelay());
                        currentWindowLayer--;
                        return;
                    }
                    ItemInformation from = playerItemUI.playerInventory.items[playerItemIndex];
                    int fromAmount = playerItemUI.playerInventory.itemsamount[playerItemIndex];
                    BoxEvent.Invoke(true, from, fromAmount, playerItemIndex);
                    // move item from inventory to Box
                    
                    currentWindowLayer--;

                }else if(playerItemUI.itemContainers[playerItemUI.currentindex].selectIndex == 1) // combine
                {
                    combineStartItem = playerItemUI.playerInventory.items[playerItemUI.currentindex];
                    combineStartItemIndex = playerItemUI.currentindex;
                    previousPlayerItemIndex = combineStartItemIndex;
                    currentWindowLayer++;
                }else if(playerItemUI.itemContainers[playerItemUI.currentindex].selectIndex == 2) // discard
                {
                    discardItemEvent.Invoke(playerItemUI.currentindex);
                    currentWindowLayer--;
                }
            }
            else if(currentWindowLayer == 2)
            {
                if(!playerItemUI.itemContainers[playerItemUI.currentindex].isCombineable) return;

                CombineEvent.Invoke(combineStartItem, combineStartItemIndex, playerItemUI.playerInventory.items[playerItemUI.currentindex], playerItemUI.currentindex);
                currentWindowLayer--;
                currentWindowLayer--;
                playerItemIndex = previousPlayerItemIndex;
            }
        }

    }

    public void BackPressed(InputActionEventData data)
    {
        if(!inputOk) return;

        currentWindowLayer--;
        if(currentWindowLayer == 1)
        {
            playerItemIndex = previousPlayerItemIndex;
        }
 
        if(currentWindowLayer < 0)
        {
            currentWindowLayer = 0;
            GameManagerUI.instance.Visualize_BoxUI(false);
        }
    }
    public void UpPressed(InputActionEventData data)
    {
        if(!inputOk) return;

        if(isBox)
        {
            if(currentWindowLayer == 0 || currentWindowLayer == 2)
            {
                boxItemIndex -= 4;
                ContainerLimit();
            }
            else if(currentWindowLayer == 1)
            {
                int n = boxItemUI.itemContainers[boxItemUI.currentindex].selectIndex;
                n--;
                boxItemUI.itemContainers[boxItemUI.currentindex].SetSelectIndex(Mathf.Clamp(n, 0, 2));
            }
        }else
        {
            if(currentWindowLayer == 0 || currentWindowLayer == 2)
            {
                playerItemIndex -= 4;
                ContainerLimit();
            }
            else if(currentWindowLayer == 1)
            {
                int n = playerItemUI.itemContainers[playerItemUI.currentindex].selectIndex;
                n--;
                playerItemUI.itemContainers[playerItemUI.currentindex].SetSelectIndex(Mathf.Clamp(n, 0, 2));
            }
        }
        
        
    }
    public void DownPressed(InputActionEventData data)
    {
        if(!inputOk) return;

        if(isBox)
        {
            if(currentWindowLayer == 0 || currentWindowLayer == 2)
            {
                boxItemIndex += 4;
                ContainerLimit();
            }
            else if(currentWindowLayer == 1)
            {
                int n = boxItemUI.itemContainers[boxItemUI.currentindex].selectIndex;
                n++;
                boxItemUI.itemContainers[boxItemUI.currentindex].SetSelectIndex(Mathf.Clamp(n, 0, 2));
            }
        }else
        {
            if(currentWindowLayer == 0 || currentWindowLayer == 2)
            {
                playerItemIndex += 4;
                ContainerLimit();
            }
            else if(currentWindowLayer == 1)
            {
                int n = playerItemUI.itemContainers[playerItemUI.currentindex].selectIndex;
                n++;
                playerItemUI.itemContainers[playerItemUI.currentindex].SetSelectIndex(Mathf.Clamp(n, 0, 2));
            }
        }

    }
    public void RightPressed(InputActionEventData data)
    {
        if(!inputOk) return;

        if(isBox)
        {
            if(currentWindowLayer == 0 || currentWindowLayer == 2)
            {
                boxItemIndex++;
                ContainerLimit();
            }

        }else
        {
            if(currentWindowLayer == 0 || currentWindowLayer == 2)
            {
                playerItemIndex++;
                ContainerLimit();
            }
        }
    }
    public void LeftPressed(InputActionEventData data)
    {
        if(!inputOk) return;

        if(isBox)
        {
            if(currentWindowLayer == 0 || currentWindowLayer == 2)
            {
                boxItemIndex--;
                ContainerLimit();
            }

        }else
        {
            if(currentWindowLayer == 0 || currentWindowLayer == 2)
            {
                playerItemIndex--;
                ContainerLimit();
            }
        }
    }

    private void ContainerLimit()
    {
        if(isBox)
        {
            boxItemIndex = Mathf.Clamp(boxItemIndex, 0, 15);
        }else
        {
            playerItemIndex = Mathf.Clamp(playerItemIndex, 0, GameManagerUI.instance.CurrentContainer-1);
        }
        
        
    }

    public void Visualize_BoxUI(bool flag)
    {
        if(isShowing && flag) return;

        Tab_Menu_ChangeInput_PauseGame(flag);

        isShowing = flag;

        background.gameObject.SetActive(flag);
        boxItemUI.gameObject.SetActive(flag);
        playerItemUI.gameObject.SetActive(flag);
        itemExplainUI.gameObject.SetActive(flag);
    }

    private void Tab_Menu_ChangeInput_PauseGame(bool flag)
    {
        if(flag)
        {
            GameMangerInput.instance.changePlayerInputRule(1);
        }else
        {
            GameMangerInput.instance.changePlayerInputRule(0);
        }

        inputOk = flag;

        GameManager.instance.SetPauseGame(flag);
    }
}
