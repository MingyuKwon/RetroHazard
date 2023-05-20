using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerGoalCollection : MonoBehaviour
{
    public static event Action goalChangeEvent;

    private static int private_currentGoalIndex = 0;
    public static int currentGoalIndex
    {
        get
        {
            return private_currentGoalIndex;
        }

        set
        {
            ChangeGoalIndex(value);
        }
    }

    public static void ChangeGoalIndex(int n)
    {
        if(n >= PlayerGoals.Length)
        {
            Debug.LogError(n + "is bigger than array index limit " + PlayerGoals.Length);
            return;
        }

        private_currentGoalIndex = n;
        goalChangeEvent?.Invoke();
    }

    public static string[] PlayerGoals = {
        "Find a way to go outside",
        "Find the key to unlock the locked door",
        "Search someting useful"
    };

}
