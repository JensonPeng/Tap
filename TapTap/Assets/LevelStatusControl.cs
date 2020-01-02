using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStatusControl : MonoBehaviour
{
    public StageStatus data;
    public bool thisClear;

    public static LevelStatusControl instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public class isClear
    {
        public string stageName;
        public bool clear;
        public bool active;
    }

    public List<isClear> stageClearList = new List<isClear>();
}
