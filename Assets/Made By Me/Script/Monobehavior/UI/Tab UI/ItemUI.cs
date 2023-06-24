using UnityEngine;

/////////////// Cleared ///////////////////////
public class ItemUI : MonoBehaviour
{
    ItemUILogic itemUILogic;
    [SerializeField] Sprite nullSprite;
    public ItemContainer[] itemContainers{
        get{
            return itemUILogic.itemContainers;
        }
        set{
            itemUILogic.itemContainers = value;
        }
    }
    
    private void Awake() {
        itemUILogic = new ItemUILogic(nullSprite, GetComponentsInChildren<ItemContainer>());
    }

    public void UpdateInventoryUI()
    {
        itemUILogic.UpdateInventoryUI();
    }

    private void Start() {
        UpdateInventoryUI();
    }

    private void Update() {
        itemUILogic.ItemContainerFocus();
    }

    public void ItemContainerFocusDirect(int num)
    {
        itemUILogic.ItemContainerFocusDirect(num);
    }

    public void SetInteractFade()
    {
        itemUILogic.SetInteractFade();
    }

    
}
