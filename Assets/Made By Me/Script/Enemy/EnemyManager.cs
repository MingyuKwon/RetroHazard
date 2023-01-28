using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private Animator animator;
    public bool isEnemyPaused = false;
    public bool isParried = false;

    [SerializeField] Color normalColor;
    [SerializeField] Color parriedColor;

    EnemyStatus status;
    private Animator vfxAnimator;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        animator = GetComponent<Animator>();
        status = GetComponentInChildren<EnemyStatus>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        vfxAnimator = GetComponentInChildren<VFX>().gameObject.GetComponent<Animator>();
    }

    public void TriggerEnemyParriedAnimation()
    {
        vfxAnimator.SetTrigger("Parried");
    }

    public void SetEnemyParried(bool flag)
    {
        isParried = flag;
    }

    public void SetEnemyStaggered(bool flag)
    {
        isEnemyPaused = flag;
        animator.SetTrigger("Idle");
        if(flag)
        {
            spriteRenderer.color = parriedColor;
        }else
        {
            spriteRenderer.color = normalColor;
        }
    }

}
