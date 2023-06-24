using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BoxItemContainer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
        UI.instance.boxUI.boxItemIndex = containerNum;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UI.instance.boxUI.boxItemIndex = -1;
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

        EquipImage.gameObject.SetActive(false);
    }    

    private void OnEnable() {
        itemContainerLogic.OnEnableBoxContainer();
    }

    private void OnDisable() {
        itemContainerLogic.OnDisable();
    }

    private void Start() {
        focus.SetselectText(0, "Take item");
        focus.SetselectText(1, "");
        focus.SetselectText(2, "");
    }

    private void Update() {
        itemContainerLogic.UpdateBoxItemContainer();
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
