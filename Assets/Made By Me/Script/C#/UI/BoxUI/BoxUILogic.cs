using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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

        GameManager.instance.SetPauseGame(flag);

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

        string[] texts = {
            "<i><b>-Input-</b></i>\n\n<b>ENTER</b> : space  <b>BACK</b> : backSpace  <b>Box</b> : J    <b>Inventory</b> : K"
        };

        GameManager.EventManager.InvokeShowNotice("BoxUI", texts , false, 900 ,150);


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

        UI.instance.TotalUIUpdate();

        GameManager.EventManager.InvokeShowNotice("BoxUI");

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
            if(UI.instance.boxUI.boxItemIndex < 0) return;
            if(Player1.instance.playerItemBox.items[UI.instance.boxUI.boxItemIndex] == null) return;

            if(currentWindowLayer == 0)
            {
                currentWindowLayer++;
            }
            else if(currentWindowLayer == 1)
            {
                if(Player1.instance.playerInventory.isInventoryFull)
                {
                    GameManagerUI.instance.SetInteractiveDialogText(new string[] {"<b><color=red>Your inventory is full!</color></b> \n\n\nCan't move this item from box to inventory"});
                    GameManagerUI.instance.VisualizeInteractiveUI(true);
                    currentWindowLayer--;
                    return;
                }

                ItemInformation from = Player1.instance.playerItemBox.items[boxItemIndex];
                int fromAmount = Player1.instance.playerItemBox.itemsamount[boxItemIndex];
                GameManager.EventManager.Invoke_BoxEvent(false, from, fromAmount, boxItemIndex);
                    
                currentWindowLayer--;
            }
        }else
        {
            if(UI.instance.boxUI.playerItemIndex < 0) return;
            if(Player1.instance.playerInventory.items[UI.instance.boxUI.playerItemIndex] == null) return;

            if(currentWindowLayer == 0)
            {
                currentWindowLayer++;
            }
            else if(currentWindowLayer == 1)
            {
                if(playerItemUI.itemContainers[UI.instance.boxUI.playerItemIndex].selectIndex == 0)
                {
                    if(Player1.instance.playerItemBox.isBoxfull)
                    {

                        GameManagerUI.instance.SetInteractiveDialogText(new string[] {"<b><color=red>Your Box is full!</color></b> \n\n\nCan't move this item from inventory to box"});
                        GameManagerUI.instance.VisualizeInteractiveUI(true);
                        currentWindowLayer--;
                        return;
                    }
                    ItemInformation from = Player1.instance.playerInventory.items[playerItemIndex];
                    int fromAmount = Player1.instance.playerInventory.itemsamount[playerItemIndex];
                    GameManager.EventManager.Invoke_BoxEvent(true, from, fromAmount, playerItemIndex);
                    // move item from inventory to Box
                    
                    currentWindowLayer--;

                }else if(playerItemUI.itemContainers[UI.instance.boxUI.playerItemIndex].selectIndex == 1) // combine
                {
                    combineStartItem = Player1.instance.playerInventory.items[UI.instance.boxUI.playerItemIndex];
                    combineStartItemIndex = UI.instance.boxUI.playerItemIndex;
                    previousPlayerItemIndex = combineStartItemIndex;
                    currentWindowLayer++;
                }else if(playerItemUI.itemContainers[UI.instance.boxUI.playerItemIndex].selectIndex == 2) // discard
                {
                    discardTargetItemIndex = UI.instance.boxUI.playerItemIndex;
                    AlertUI.instance.ShowAlert("Are you sure to Discard this Item? \n\n <i>(discarded Item cannot be restored)</i>", this);
                }
            }
            else if(currentWindowLayer == 2)
            {
                if(!playerItemUI.itemContainers[UI.instance.boxUI.playerItemIndex].isCombineable) return;

                GameManager.EventManager.Invoke_CombineEvent(combineStartItem, 
                                                                combineStartItemIndex, 
                                                                Player1.instance.playerInventory.items[UI.instance.boxUI.playerItemIndex], 
                                                                UI.instance.boxUI.playerItemIndex);
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
                int n = boxItemUI.itemContainers[UI.instance.boxUI.boxItemIndex].selectIndex;
                n--;
                boxItemUI.itemContainers[UI.instance.boxUI.boxItemIndex].SetSelectIndex(Mathf.Clamp(n, 0, 2));
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
                int n = playerItemUI.itemContainers[UI.instance.boxUI.playerItemIndex].selectIndex;
                n--;
                playerItemUI.itemContainers[UI.instance.boxUI.playerItemIndex].SetSelectIndex(Mathf.Clamp(n, 0, 2));
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
                int n = boxItemUI.itemContainers[UI.instance.boxUI.boxItemIndex].selectIndex;
                n++;
                boxItemUI.itemContainers[UI.instance.boxUI.boxItemIndex].SetSelectIndex(Mathf.Clamp(n, 0, 2));
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
                int n = playerItemUI.itemContainers[UI.instance.boxUI.playerItemIndex].selectIndex;
                n++;
                playerItemUI.itemContainers[UI.instance.boxUI.playerItemIndex].SetSelectIndex(Mathf.Clamp(n, 0, 2));
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
