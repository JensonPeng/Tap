using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    #region 定義
    [Header("Object")]
    public GameObject[] Holes;
    [Header("UI")]
    public TextMeshProUGUI title;
    public GameObject gameoverPanel;
    public GameObject countDownPanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI goalLeftAlert;
    
    [Header("Numbers")]
    public int score;

    float t;
    float playTime;

    #endregion

    void Start()
    {
        targetPooler = TargetPooler.instance;

        Time.timeScale = 1;

        playTime = 30;

        LoadStatus();
    }

    [HideInInspector] public bool isPlaying;
    public bool stageClear;

    #region 載入關卡資料
    TargetPooler targetPooler;

    LevelStatusControl stageData;
    [HideInInspector]public int goal;
    [HideInInspector]public float objAlive;
    float timeLimit;
    float frequency;

    int totalObj;

    void LoadStatus()
    {
        if (LevelStatusControl.instance != null)
        {
            stageData = LevelStatusControl.instance;
            title.text = stageData.data.DisplayName;
            goal = stageData.data.Goal;
            timeLimit = stageData.data.TimeLimit;
            objAlive = stageData.data.ObjLiveTime;

            totalObj = (int)Mathf.Ceil(goal * 1.25f);
            frequency = timeLimit / totalObj;
        }
        else
        {
            Debug.Log("not found");
        }
    }
    #endregion

    bool BossFight;

    void Update()
    {
        #region 觸控控制
        for (int i = 0; i < Input.touchCount; i++)
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);

            RaycastHit2D hit = Physics2D.Raycast(touchPosition, Camera.main.transform.forward);

            if (hit.collider != null)
            {
                if (Input.touches[i].phase == TouchPhase.Began)
                {
                    if (hit.collider.GetComponent<TouchObject>() != null)
                    {
                        hit.collider.GetComponent<TouchObject>().EnterHit(); 
                    }
                    if (hit.collider.GetComponent<Boss>() != null)
                    {
                        hit.collider.GetComponent<Boss>().EnterHit(hit.point);
                    }
                }
            }
        }
        #endregion

        #region 打擊物件
        if (!BossFight)
        {
            t += Time.deltaTime;
            if (t >= frequency && isPlaying)
            {
                int r = Random.Range(0, Holes.Length);
                targetPooler.SpawnFromPool("Client1", Holes[r].transform.position);

                t = 0;
            }
        }
        #endregion

        #region UI數值顯示
        scoreText.text = "Score : " + score.ToString();

        if (isPlaying)
        {
            playTime -= Time.deltaTime;
            if (playTime <= 0)
            {
                playTime = timeLimit;
                StageClear();
            }
        }

        if (!BossFight)
            timerText.text = ((int)playTime).ToString();
        else
            timerText.text = "";

        if (playTime <= 10 && goal - score > 0)
            goalLeftAlert.text = (goal - score).ToString() + " Left";
        else
            goalLeftAlert.text = "";
        #endregion

    }

    #region 分數計算
    public void AddScore(int _scoreFromHit)
    {
        score += _scoreFromHit;
    }
    #endregion

    #region 結算

    public GameObject br;

    void StageClear()
    {
        if (score >= goal)
        {
            if (!stageData.data.Boss)
            {
                //跳出結算畫面
                ShowClear();

                //新增通關記錄到清單
                StageClearDataAdd();
            }
            else
            {
                //Boss戰
                BossFight = true;
                br.SetActive(true);
            }
        }
        else
        {
            //失敗
            gameoverPanel.SetActive(true);
        }
    }

    public void ShowClear()
    {
        isPlaying = false;
        countDownPanel.SetActive(true);
        countDownPanel.GetComponent<CountDownScript>().showClear = true;
        countDownPanel.GetComponent<CountDownScript>().Coro();
        stageClear = true;
    }
    public void StageClearDataAdd()
    {
        System.Predicate<LevelStatusControl.isClear> check = new System.Predicate<LevelStatusControl.isClear>(name => name.stageName == stageData.data.LevelName);

        if (stageData.stageClearList.Find(check) == null)
        {
            LevelStatusControl.isClear c = new LevelStatusControl.isClear();
            c.stageName = stageData.data.LevelName;
            c.clear = true;
            c.active = true;

            stageData.stageClearList.Add(c);
        }
    }

    #endregion


    #region 結束選單
    public void PlaySceneReload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GoTitle()
    {
        stageData.thisClear = stageClear;
        SceneManager.LoadScene("Menu");
    }
    public void BackToStageSelect()
    {
        stageData.thisClear = stageClear;
        SceneManager.LoadScene("StageSelect");
    }
    #endregion
}
