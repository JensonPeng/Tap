using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelBtnScript : MonoBehaviour
{
    StageStatus thisData;
    public TextMeshProUGUI text;

    public bool active;
    public bool clear;

    private void Start()
    {
        System.Predicate<LevelStatusControl.isClear> NAME = new System.Predicate<LevelStatusControl.isClear>(name => name.stageName == gameObject.name);
        LevelStatusControl.isClear lsc = LevelStatusControl.instance.stageClearList.Find(NAME);
        if (lsc != null)
        {
            active = lsc.active;
            clear = lsc.clear;
        }

        if (active)
            GetComponent<Button>().interactable = true;
        else
            GetComponent<Button>().interactable = false;
    }

    public void LoadData(string diff, string big, string small, StageStatus[] _allData)
    {
        gameObject.name = diff + "-" + big + "-" + small;

        text.text = big + "-" + small;
        if (big == "2" && small == "5")
            text.text = big + "-" + small + "\n(Boss)";

        foreach (StageStatus _data in _allData)
        {
            if (_data.LevelName == gameObject.name)
            {
                thisData = _data;
            }
        } 
    }

    public void ButtonClick()
    {
        LevelStatusControl.instance.data = thisData;
        LevelStatusControl.instance.thisClear = clear;
        SceneManager.LoadScene("Main");
    }
}
