using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    Animator anim;

    public GameObject levelBtnPrefab;
    public Transform[] levelPanel;
    public GameObject BackBtn;
    public int primaryStage;
    public int secondaryStage;

    [Header("StageData")]
    public StageStatus[] stageDatas;

    int difficulty;

    LevelBtnScript previous;

    void Start()
    {
        anim = GetComponent<Animator>();

        for (int l = 0; l < levelPanel.Length; l++)
        {
            for (int p = 1; p <= primaryStage; p++)
            {
                for (int s = 1; s <= secondaryStage; s++)
                {
                    GameObject btnClone = Instantiate(levelBtnPrefab, levelPanel[l]);
                    LevelBtnScript lbs = btnClone.GetComponent<LevelBtnScript>();

                    lbs.LoadData(l.ToString(), p.ToString(), s.ToString(), stageDatas);

                    System.Predicate<LevelStatusControl.isClear> NAME = new System.Predicate<LevelStatusControl.isClear>(name => name.stageName == LevelStatusControl.instance.data.LevelName);

                    if (LevelStatusControl.instance.stageClearList.Find(NAME) != null)
                    {
                        if (lbs.name == LevelStatusControl.instance.stageClearList.Find(NAME).stageName)
                        {
                            if (LevelStatusControl.instance.stageClearList.Find(NAME).clear)
                            {
                                lbs.clear = true;
                                lbs.active = true;
                            }
                        }
                    }

                    if (previous == null)
                        lbs.active = true;
                    else
                    {
                        if (previous.clear)
                            lbs.active = true;
                        else
                            lbs.active = false;
                    }

                    previous = lbs;
                }
            }
        }
    }

    #region 按鈕功能
    public void SelectEasy()
    {
        difficulty = 0;
        anim.SetTrigger("Out");
    }
    public void SelectNormal()
    {
        difficulty = 1;
        anim.SetTrigger("Out");
    }
    public void SelectHard()
    {
        difficulty = 2;
        anim.SetTrigger("Out");
    }
    public void BackToSelectLevel()
    {
        for (int i = 0; i < levelPanel.Length; i++)
        {
            levelPanel[i].gameObject.SetActive(false);
        }
        anim.SetTrigger("In");
    }

    public void CallLevel()
    {
        for (int i = 0; i < levelPanel.Length; i++)
        {
            if (i == difficulty)
                levelPanel[i].gameObject.SetActive(true);
            else
                levelPanel[i].gameObject.SetActive(false);
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void BackDisable()
    {
        BackBtn.SetActive(false);
    }
    public void BackEnable()
    {
        BackBtn.SetActive(true);
    }
    #endregion
}
