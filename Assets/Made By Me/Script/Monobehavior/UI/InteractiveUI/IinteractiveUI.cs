using UnityEngine;
using UnityEngine.UI;

///////////// Cleared //////////////////////////
public class IinteractiveUI : MonoBehaviour
{
    InteractiveUILogic interactiveUILogic;
    GameObject interactiveDialogPanel;
    Text interactiveDialogText;

    private void Awake() {
        interactiveDialogPanel = GetComponentInChildren<InteractiveDialogPanel>().gameObject;
        interactiveDialogText = interactiveDialogPanel.gameObject.GetComponentInChildren<Text>();

        interactiveUILogic = new InteractiveUILogic(interactiveDialogText, this);
    }

    private void OnEnable() {
        interactiveUILogic.OnEnable();
    }

    private void OnDisable() {
       interactiveUILogic.OnDisable();
    }

    public void SetInteractiveDialogText(string[] texts)
    {
        interactiveUILogic.SetInteractiveDialogText(texts);
    }

    public void VisualizeInteractiveUI(bool flag, string ItemName = "")
    {        
        interactiveUILogic.VisualizeInteractiveUI(flag, ItemName);
    }

}
