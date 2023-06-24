using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

////////////// ItemContainerLogic 를 이용해서 커스터 마이징 정리 완료 ///////////////////
public class PlayerItemContainer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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

    public bool isCombineable{
        get{
            return itemContainerLogic.isCombineable;
        }

        set{
            itemContainerLogic.isCombineable = value;
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
        UI.instance.boxUI.playerItemIndex = containerNum;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UI.instance.boxUI.playerItemIndex = -1;
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

    private void OnEnable() {
        itemContainerLogic.OnEnablePlayerContainer();
    }

    private void OnDisable() {
        itemContainerLogic.OnDisable();
    }

    private void Update() {
        itemContainerLogic.UpdatePlayerItemContainer();
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
