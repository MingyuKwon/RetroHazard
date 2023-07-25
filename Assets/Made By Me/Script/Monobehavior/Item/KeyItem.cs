using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : Item
{
    Vector3 firstPosition;
    
    public Vector3 itemUp = new Vector3(0f,0.5f,0f);

    public Interact interact;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        firstPosition = GetComponent<Transform>().position;
        interact = GetComponentInChildren<Interact>();
    }

    public void Get_Item_Pause_Game()
    {
        GameManager.instance.SetPlayerAnimationObtainKeyItem(true);
        

        spriteRenderer.sortingLayerName = "4";
        
        if(GameAudioManager.LanguageManager.currentLanguage == "E")
        {
            GameManagerUI.instance.VisualizeInteractiveUI(true, information.ItemName);
            GameManagerUI.instance.SetInteractiveDialogText(information.ItemDescription);
        }else if(GameAudioManager.LanguageManager.currentLanguage == "K")
        {
            GameManagerUI.instance.VisualizeInteractiveUI(true, information.ItemNameKorean);
            GameManagerUI.instance.SetInteractiveDialogText(information.ItemDescriptionKorean);
        }
        StartCoroutine(isDialogEnd());
    }

    IEnumerator isDialogEnd()
    {
        while(GameManager.ObtainKeyItem)
        {
            yield return new WaitForEndOfFrame();
        }   

        if(information.KeyItemCode == 15) // 먹은게 가방인 경우
        {
            this.gameObject.SetActive(false);
            ((ExpansionItem)this).EventInvokeOverride();
            GameManager.EventManager.Invoke_inventoryExpandEvent();

        }else
        {
            transform.position = firstPosition;
            GameManagerUI.instance.Visualize_Tab_Obtain(true , this);
        }
    }

    public void ObtainKeyItem(Collider2D other)
    {
        Get_Item_Pause_Game();

        interact.SetCheckActive(false);
        transform.position = other.gameObject.transform.position + itemUp;
        
    }

    public void SetSpriteSortNoraml()
    {
        spriteRenderer.sortingLayerName = "Item";
    }

    public virtual void EventInvokeOverride()
    {

    }

}
