using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    public ItemDialog dialog;
    public SpriteRenderer spriteRenderer;
    public Vector3 itemUp = new Vector3(0f,0.5f,0f);

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Get_Item_Pause_Game()
    {
        if(dialog.isKeyItem)
        {
            GameManager.instance.SetPlayerAnimationObtainKeyItem(true);
            GameManagerUI.instance.SetInteractiveDialogText(dialog.ItemDescription);
        }

        GameManager.instance.SetPauseGame(true);
        spriteRenderer.sortingLayerName = "4";
        GameManagerUI.instance.showInteractiveDialogPanelUI(true, dialog.ItemName);
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
