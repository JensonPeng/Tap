using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBreak : MonoBehaviour
{
    public GameObject prefab;
    public GameObject smoke;

    LevelStatusControl stageData;

    void Start()
    {
        stageData = FindObjectOfType<LevelStatusControl>();
    }

    public void BossActive()
    {
        GameObject bossClone = Instantiate(prefab);
        Boss bossData = bossClone.GetComponent<Boss>();
        bossData.hp = stageData.data.bossHp;
        bossData.timeLimit = stageData.data.bossTimeLimit;

        gameObject.SetActive(false);
    }
}
