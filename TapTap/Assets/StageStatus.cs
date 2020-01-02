using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "StageStatus")]
public class StageStatus : ScriptableObject
{
    public string LevelName;
    public string DisplayName;

    public int Goal;
    public float TimeLimit;
    public float ObjLiveTime;

    public bool Boss;
    public int bossHp;
    public float bossTimeLimit;
}
