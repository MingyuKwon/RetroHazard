using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RealInteract : Interact
{
    private void Start() {
        if(SaveSystem.instance.ActiveStageSaves[SceneManager.GetActiveScene().buildIndex].is_Interact_Destroy[transform.GetSiblingIndex()])
        {
            this.gameObject.SetActive(false);
        }
    }
}
