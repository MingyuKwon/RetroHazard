using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxUI : MonoBehaviour
{
    BoxUILogic boxUILogic;

    public int currentWindowLayer{
        get{
            return boxUILogic.currentWindowLayer;
        }
        set{
            boxUILogic.currentWindowLayer = value;
        }
    }

    public bool isBox{
        get{
            return boxUILogic.isBox;
        }
        set{
            boxUILogic.isBox = value;
        }
    }

    public bool isCursorInBox{
        get{
            return boxUILogic.isCursorInBox;
        }
        set{
            boxUILogic.isCursorInBox = value;
        }
    }

    public int boxItemIndex{
        get{
            return boxUILogic.boxItemIndex;
        }
        set{
            boxUILogic.boxItemIndex = value;
        }
    }

    public int playerItemIndex{
        get{
            return boxUILogic.playerItemIndex;
        }
        set{
            boxUILogic.playerItemIndex = value;
        }
    }

    public BoxItemUI boxItemUI;
    public PlayerItemUI playerItemUI;
    public ItemExplainUI boxitemExplainUI;

    public ItemInformation combineStartItem{
        get{
            return boxUILogic.combineStartItem;
        }
        set{
            boxUILogic.combineStartItem = value;
        }
    }

    public int combineStartItemIndex{
        get{
            return boxUILogic.combineStartItemIndex;
        }
        set{
            boxUILogic.combineStartItemIndex = value;
        }
    }

    private void OnEnable() {
        boxUILogic.OnEnable();
    }

    private void OnDisable() {
        boxUILogic.OnDisable();
    }

    public void UpdateBoxUI()
    {
        boxUILogic.UpdateBoxUI();
    }

    private void Awake() {
        boxitemExplainUI = GetComponentInChildren<ItemExplainUI>();
        boxItemUI = GetComponentInChildren<BoxItemUI>();
        playerItemUI = GetComponentInChildren<PlayerItemUI>();

        boxUILogic = new BoxUILogic(transform.GetChild(0).GetComponent<Image>());
    }

    public void GotoBox()
    {
        boxUILogic.GotoBox();
    }

    public void GotoInventory()
    {
        boxUILogic.GotoInventory();
    }

    public void Visualize_BoxUI(bool flag)
    {
        boxUILogic.Visualize_BoxUI(flag);
    }

}
