using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : Item
{
    Vector3 firstPosition;
    public SpriteRenderer spriteRenderer;
    public Vector3 itemUp = new Vector3(0f,0.5f,0f);

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        firstPosition = GetComponent<Transform>().position;
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
        GameManagerUI.instance.VisualizeInteractiveUI(true, information.ItemName);
        StartCoroutine(isDialogEnd());
    }

    IEnumerator isDialogEnd()
    {
        while(GameManager.instance.ObtainKeyItem)
        {
            yield return new WaitForEndOfFrame();
        }   
        transform.position = firstPosition;
        GameManagerUI.instance.Visualize_Tab_Obtain(true , this);


        //Destroy(this.gameObject);
    }

    public void ObtainKeyItem(Collider2D other)
    {
        Get_Item_Pause_Game();

        GetComponentInChildren<Interact>().check.SetActive(false);
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
