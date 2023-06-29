using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    MiniMapLogic miniMapLogic;
    private void Awake() {
        miniMapLogic = new MiniMapLogic(this);
    }
}
