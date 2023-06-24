using UnityEngine;
using UnityEngine.UI;

public class CurrentGoalUI : MonoBehaviour
{
    Text currentGoalText;

    private void Awake() {
        currentGoalText = transform.GetChild(1).GetComponent<Text>();
    }

    private void OnEnable() {
        PlayerGoalCollection.goalChangeEvent += ChangeGoalTextAuto;

        ChangeGoalTextAuto();
    }

    private void OnDisable() {
        PlayerGoalCollection.goalChangeEvent -= ChangeGoalTextAuto;
    }

    public void ChangeGoalText(string text)
    {
        currentGoalText.text = text;
    }

    private void ChangeGoalTextAuto()
    {
        ChangeGoalText(PlayerGoalCollection.PlayerGoals[PlayerGoalCollection.currentGoalIndex]);
    }
}
