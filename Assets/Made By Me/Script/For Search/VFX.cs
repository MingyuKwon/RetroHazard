using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class VFX : MonoBehaviour
{
    public CapsuleCollider2D playerBodyCollider;
    public BoxCollider2D attackCollider;
    public BoxCollider2D sheildCollider;

    public void ParryVFXStart()
    {
        playerBodyCollider.enabled = false;

    }

    public void ParryVFXEnd()
    {
        playerBodyCollider.enabled = true;
    }
}
