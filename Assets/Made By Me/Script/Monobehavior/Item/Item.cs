using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Item : ForNoticeBroadCast
{
    public ItemInformation information;
    public SpriteRenderer spriteRenderer;

    public void Init(ItemInformation info)
    {
        this.information = info;
        information = info;
    }
}
