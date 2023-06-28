using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

////////////////// Cleared ////////////////////

public class ItemContainer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    ItemContainerLogic itemContainerLogic;
    [SerializeField] int containerNum;
    
    public Image itemImage{
        get{
            return itemContainerLogic.itemImage;
        }
        set{
            itemContainerLogic.itemImage = value;
        }
    }
    
    public Image EquipImage{
        get{
            return itemContainerLogic.EquipImage;
        }
        set{
            itemContainerLogic.EquipImage = value;
        }
    }

    public FocusUI focus{
        get{
            return itemContainerLogic.focus;
        }
        set{
            itemContainerLogic.focus = value;
        }
    }

    public bool isFocused{
        get{
            return itemContainerLogic.isFocused;
        }
        set{
            itemContainerLogic.isFocused = value;
        }
    }
    public bool isCombineable{
        get{
            return itemContainerLogic.isCombineable;
        }

        set{
            itemContainerLogic.isCombineable = value;
        }
    }
    public bool isInteractive{
        get{
            return itemContainerLogic.isInteractive;
        }

        set{
            itemContainerLogic.isInteractive = value;
        }
    }

    int indexLimitMin{
        get{
            return itemContainerLogic.indexLimitMin;
        }

        set{
            itemContainerLogic.indexLimitMin = value;
        }
    }
    int indexLimitMax{
        get{
            return itemContainerLogic.indexLimitMax;
        }

        set{
            itemContainerLogic.indexLimitMax = value;
        }
    }

    public int selectIndex{
        get{
            return itemContainerLogic.selectIndex;
        }

        set{
            itemContainerLogic.selectIndex = value;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        itemContainerLogic.OnInventoryPointerEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemContainerLogic.OnInventoryPointerExit();
    }

    private void Awake() {
        Image _backGround = GetComponent<Image>();
        Image _itemImage = transform.GetChild(0).GetComponent<Image>();
        Text _itemAmountText = transform.GetChild(1).GetComponentInChildren<Text>();
        GameObject _itemAmount =  transform.GetChild(1).gameObject;
        Image _EquipImage = transform.GetChild(2).GetComponent<Image>();
        Image _fadeImage = transform.GetChild(3).GetComponent<Image>();
        FocusUI _focus = transform.GetChild(4).GetComponent<FocusUI>();

        GameObject _focusSelectPanel = _focus.gameObject.transform.GetChild(0).gameObject;

        itemContainerLogic = new ItemContainerLogic(containerNum, _backGround, _itemImage, _EquipImage,
        _itemAmount,_itemAmountText, _fadeImage,_focus,_focusSelectPanel);
    }

    private void Update() {
        itemContainerLogic.UpdateInventoryItemContainer();
    }

    public void SetInteractFade()
    {
        itemContainerLogic.SetInteractFade();
    }

    private void OnEnable() {
        itemContainerLogic.OnEnableInventoryContainer();
    }

    private void OnDisable() {
        itemContainerLogic.OnDisable();
    }

    public void SetSelectIndex(int index)
    {
        itemContainerLogic.SetSelectIndex(index);
    }

    public void SetItemAmountUI(bool flag)
    {
        itemContainerLogic.SetItemAmountUI(flag);
    }

    public void SetItemAmountText(String str)
    {
        itemContainerLogic.SetItemAmountText(str);
    }

    public void SetFocus(bool flag)
    {
        itemContainerLogic.SetFocus(flag);
    }

}
