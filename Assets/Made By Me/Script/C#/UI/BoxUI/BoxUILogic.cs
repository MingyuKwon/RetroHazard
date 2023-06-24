using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



///////////// 가득 찼을 때 어떻게 나오는지 보자 ////////////////////

public class BoxUILogic : CallBackInterface
{
    public bool isShowing = false;

    public int currentWindowLayer = 0;
    public bool isBox = false;
    public bool isCursorInBox = false;


    public int boxItemIndex = 0;
    public int playerItemIndex = 0;

    int previousBoxItemIndex = -1;
    int previousPlayerItemIndex = 0;

    int discardTargetItemIndex;
    public int combineStartItemIndex;
    public ItemInformation combineStartItem = null;


    Image background;

    BoxItemUI boxItemUI;
    PlayerItemUI playerItemUI;
    ItemExplainUI boxitemExplainUI;

    public BoxUILogic(Image background)
    {
        this.background = background;
        boxItemUI = UI.instance.boxUI.boxItemUI;
        playerItemUI = UI.instance.boxUI.playerItemUI;
        boxitemExplainUI = UI.instance.boxUI.boxitemExplainUI;
    }

    public void Visualize_BoxUI(bool flag)
    {
        if(isShowing && flag) return;

        Box_ChangeInput_PauseGame(flag);

        isShowing = flag;

        background.gameObject.SetActive(flag);
        boxItemUI.gameObject.SetActive(flag);
        playerItemUI.gameObject.SetActive(flag);
        boxitemExplainUI.gameObject.SetActive(flag);
    }

    private void ContainerLimit()
    {
        if(isBox)
        {
            boxItemIndex = Mathf.Clamp(boxItemIndex, 0, 15);
        }else
        {
            playerItemIndex = Mathf.Clamp(playerItemIndex, 0, GameManagerUI.CurrentContainer-1);
        }
        
    }

    public void GotoBox()
    {
        playerItemIndex = -1;
        isBox = true;

        boxItemUI.backgroundPanel.color = boxItemUI.selectColor;
        playerItemUI.backgroundPanel.color = playerItemUI.unSelectColor;
    }


    public void GotoInventory()
    {
        boxItemIndex = -1;
        isBox = false;

        boxItemUI.backgroundPanel.color = boxItemUI.unSelectColor;
        playerItemUI.backgroundPanel.color = playerItemUI.selectColor;
    }

    public void Box_ChangeInput_PauseGame(bool flag)
    {
        if(flag)
        {
            GameMangerInput.instance.changePlayerInputRule(1);
        }else
        {
            GameMangerInput.instance.changePlayerInputRule(0);
        }

        GameManager.instance.SetPauseGame(flag);
    }

    public void UpdateBoxUI()
    {
        boxItemUI.UpdateBoxItemUI();
        playerItemUI.UpdateInventoryUI();
    }

    public void OnEnable() {
        GameMangerInput.getInput(InputType.BoxUIInput);

        UI.instance.SetMouseCursorActive(true);

        isBox = false;
        boxItemIndex = -1;
        playerItemIndex = 0;
        discardTargetItemIndex = -1;

        boxItemUI.backgroundPanel.color = boxItemUI.unSelectColor;
        playerItemUI.backgroundPanel.color = playerItemUI.selectColor;

        GameMangerInput.InputEvent.BoxUIEnterPressed += EnterPressed;
        GameMangerInput.InputEvent.BoxUIBackPressed += BackPressed;
        GameMangerInput.InputEvent.BoxUIUpPressed += UpPressed;
        GameMangerInput.InputEvent.BoxUIDownPressed += DownPressed;
        GameMangerInput.InputEvent.BoxUIRightPressed += RightPressed;
        GameMangerInput.InputEvent.BoxUILeftPressed += LeftPressed;

        GameMangerInput.InputEvent.BoxUILeftTabPressed += LeftTab;
        GameMangerInput.InputEvent.BoxUIRightTabPressed += RightTab;
    }

    public void OnDisable() {
        GameMangerInput.releaseInput(InputType.BoxUIInput);

        UI.instance.SetMouseCursorActive(false);

        currentWindowLayer = 0;
        discardTargetItemIndex = -1;

        UI.instance.tabUI.UpdateTabUI();

        GameMangerInput.InputEvent.BoxUIEnterPressed -= EnterPressed;
        GameMangerInput.InputEvent.BoxUIBackPressed -= BackPressed;
        GameMangerInput.InputEvent.BoxUIUpPressed -= UpPressed;
        GameMangerInput.InputEvent.BoxUIDownPressed -= DownPressed;
        GameMangerInput.InputEvent.BoxUIRightPressed -= RightPressed;
        GameMangerInput.InputEvent.BoxUILeftPressed -= LeftPressed;

        GameMangerInput.InputEvent.BoxUILeftTabPressed -= LeftTab;
        GameMangerInput.InputEvent.BoxUIRightTabPressed -= RightTab;
    }

    public void EnterPressed()
    {
        if(isBox)
        {
            if(boxItemUI.currentindex < 0) return;
            if(boxItemUI.playerItemBox.items[boxItemUI.currentindex] == null) return;

            if(currentWindowLayer == 0)
            {
                currentWindowLayer++;
            }
            else if(currentWindowLayer == 1)
            {
                if(playerItemUI.isInventoryFull)
                {
                    GameManagerUI.instance.SetInteractiveDialogText(new string[] {"<b><color=red>Your inventory is full!</color></b> \n\n\nCan't move this item from box to inventory"});
                    GameManagerUI.instance.VisualizeInteractiveUI(true);
                    currentWindowLayer--;
                    return;
                }

                ItemInformation from = boxItemUI.playerItemBox.items[boxItemIndex];
                int fromAmount = boxItemUI.playerItemBox.itemsamount[boxItemIndex];
                GameManager.EventManager.Invoke_BoxEvent(false, from, fromAmount, boxItemIndex);
                    
                currentWindowLayer--;
            }
        }else
        {
            if(playerItemUI.currentindex < 0) return;
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

                        GameManagerUI.instance.SetInteractiveDialogText(new string[] {"<b><color=red>Your Box is full!</color></b> \n\n\nCan't move this item from inventory to box"});
                        GameManagerUI.instance.VisualizeInteractiveUI(true);
                        currentWindowLayer--;
                        return;
                    }
                    ItemInformation from = playerItemUI.playerInventory.items[playerItemIndex];
                    int fromAmount = playerItemUI.playerInventory.itemsamount[playerItemIndex];
                    GameManager.EventManager.Invoke_BoxEvent(true, from, fromAmount, playerItemIndex);
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
                    discardTargetItemIndex = playerItemUI.currentindex;
                    AlertUI.instance.ShowAlert("Are you sure to Discard this Item? \n\n <i>(discarded Item cannot be restored)</i>", this);
                }
            }
            else if(currentWindowLayer == 2)
            {
                if(!playerItemUI.itemContainers[playerItemUI.currentindex].isCombineable) return;

                GameManager.EventManager.Invoke_CombineEvent(combineStartItem, 
                                                                combineStartItemIndex, 
                                                                playerItemUI.playerInventory.items[playerItemUI.currentindex], 
                                                                playerItemUI.currentindex);
                currentWindowLayer--;
                currentWindowLayer--;
            }
        }

    }

    public void CallBack()
    {
        GameManager.EventManager.Invoke_discardItemEvent(discardTargetItemIndex);
        currentWindowLayer--;
        discardTargetItemIndex = -1;
    }

    public void BackPressed()
    {

        currentWindowLayer--;

        if(currentWindowLayer == 0)
        {
            if(isCursorInBox)
            {
                GotoBox();
            }else
            {
                GotoInventory();
            }

        }else if(currentWindowLayer == 1)
        {
            playerItemUI.ItemContainerFocusDirect(previousPlayerItemIndex);
        }
 
        if(currentWindowLayer < 0)
        {
            currentWindowLayer = 0;
            GameManagerUI.instance.Visualize_BoxUI(false);
        }
    }
    public void UpPressed()
    {
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
    public void DownPressed()
    {
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
    public void RightPressed()
    {
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
    public void LeftPressed()
    {
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

    public void LeftTab()
    {
        if(currentWindowLayer == 1) return;
        if(!isBox)
        {   
            previousPlayerItemIndex = playerItemIndex;
            boxItemIndex = previousBoxItemIndex;
            GotoBox();
        }
    }

    public void RightTab()
    {
        if(currentWindowLayer == 1) return;
        if(isBox)
        {
            previousBoxItemIndex = boxItemIndex;
            playerItemIndex = previousPlayerItemIndex;
            GotoInventory();
        }
    }

}
