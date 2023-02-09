using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class bulletItem : Item
{
    public static event Action<bool, bool, bool, bool, int> Obtain_bullet_Item_Event;

    public bool isSheildDurability;
    public bool isEnergy1;
    public bool isEnergy2;
    public bool isEnergy3;

    public int amount;

    public void ObtainBulletItem()
    {        
        Obtain_bullet_Item_Event?.Invoke(isSheildDurability, isEnergy1, isEnergy2, isEnergy3, amount);
        Destroy(this.gameObject);
    }
}
