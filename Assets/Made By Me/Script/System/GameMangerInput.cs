using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class GameMangerInput : MonoBehaviour
{
    public static GameMangerInput instance = null;
    private Player player;

    private ControllerMapEnabler mapEnabler;
    private ControllerMapEnabler.RuleSet[] ruleSets;
    // 0 : normal State
    // 1 : talk with NPC State
    // 2 : UI
    // 3 : Alert

    public int currentInputRule = 0;

    void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else
        {
            Destroy(this.gameObject);
        }

        player = ReInput.players.GetPlayer(0);
        mapEnabler = player.controllers.maps.mapEnabler;

        ruleSets = new ControllerMapEnabler.RuleSet[mapEnabler.ruleSets.Count];
        ruleSets[0] = mapEnabler.ruleSets.Find(x => x.tag == "NormalState");
        ruleSets[1] = mapEnabler.ruleSets.Find(x => x.tag == "TalkWithNPC");
        ruleSets[2] = mapEnabler.ruleSets.Find(x => x.tag == "UI");
        ruleSets[3] = mapEnabler.ruleSets.Find(x => x.tag == "Alert");
    }

    [Button]
    public void changePlayerInputRule(int ruleNum)
    {
        StartCoroutine(DelayChangeRule(ruleNum));
    }

    IEnumerator DelayChangeRule(int ruleNum)
    {
        yield return new WaitForEndOfFrame();

        foreach(var rule in ruleSets)
        {
            rule.enabled = false;
        }
        ruleSets[ruleNum].enabled = true;

        currentInputRule = ruleNum;

        if(currentInputRule != 0)
        {
            MouseUI.instance.MouseActive(true);
        }else
        {
            MouseUI.instance.MouseActive(false);
        }

        mapEnabler.Apply();
    }
}
