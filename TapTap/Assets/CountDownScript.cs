using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDownScript : MonoBehaviour
{
    [HideInInspector] public bool counting;
    [HideInInspector] public bool showClear;
    public GM gm;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI infoText;
    public Button btn;

    private void Start()
    {
        Set();
    }

    void Go()
    {
        btn.gameObject.SetActive(false);
        //gm.showInfo = true;
        counting = true;
        infoText.text = "";
        StartCoroutine(Count());
    }

    public void Coro()
    {
        StartCoroutine(Count());
    }

    public void Set()
    {
        countText.text = "";
        btn.gameObject.SetActive(true);

        if (!gm.stageClear)
        {
            infoText.text = "Goal : " + FindObjectOfType<LevelStatusControl>().data.Goal.ToString();
            btn.onClick.AddListener(Go);
        }
        else
        {
            infoText.text = "Stage Clear!";
            btn.onClick.AddListener(gm.BackToStageSelect);
        }
    }

    IEnumerator Count()
    {
        while (true)
        {
            if (counting)
            {
                countText.text = "3";
                yield return new WaitForSeconds(1);
                countText.text = "2";
                yield return new WaitForSeconds(1);
                countText.text = "1";
                yield return new WaitForSeconds(1);

                countText.text = "";
                counting = false;
                gm.isPlaying = true;
                gameObject.SetActive(false);
            }

            if (showClear)
            {
                infoText.text = "Clear!";
                yield return new WaitForSeconds(3);
                showClear = false;
                Set();
            }

            yield return null;
        }
    }
}
