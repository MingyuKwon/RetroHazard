using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class bulletItem : Item
{
    public static event Action<ItemInformation, int> Obtain_bullet_Item_Event;

    public void ObtainBulletItem()
    {        
        Obtain_bullet_Item_Event?.Invoke(information, information.amount);
        Destroy(this.gameObject);
    }
}
