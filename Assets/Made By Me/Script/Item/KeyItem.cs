using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    public ItemInformation information;
    public SpriteRenderer spriteRenderer;
    public Vector3 itemUp = new Vector3(0f,0.5f,0f);

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        spriteRenderer.sprite = information.ItemImage;
    }
    public void Get_Item_Pause_Game()
    {
        if(information.isKeyItem)
        {
            GameManager.instance.SetPlayerAnimationObtainKeyItem(true);
            GameManagerUI.instance.SetInteractiveDialogText(information.ItemDescription);
        }
        spriteRenderer.sortingLayerName = "4";
        GameManagerUI.instance.showInteractiveDialogPanelUI(true, information.ItemName);
        StartCoroutine(isDialogEnd());
    }

    IEnumerator isDialogEnd()
    {
        while(GameManager.instance.ObtainKeyItem)
        {
            yield return new WaitForEndOfFrame();
        }
        Destroy(this.gameObject);
    }
}
