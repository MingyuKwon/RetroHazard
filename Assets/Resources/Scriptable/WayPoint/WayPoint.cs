using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "wayPoint" , menuName ="createWayPoint")]
public class WayPoint : ScriptableObject
{
    public MapNameCollection.SceneName toSceneName;
    public int index;
    public float x = 0;
    public float y = 0;

}
