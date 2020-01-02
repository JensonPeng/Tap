using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [HideInInspector]public int hp;
    [HideInInspector]public float timeLimit;

    int currHp;
    float t;
    GM gm;

    public GameObject hpBar;
    public GameObject blood;

    Image hpClone;

    void Start()
    {
        gm = FindObjectOfType<GM>();

        currHp = hp;

        GameObject c = Instantiate(hpBar, GameObject.Find("Canvas").transform);
        hpClone = c.GetComponent<Image>();
    }

    void Update()
    {
        hpClone.fillAmount = (float)currHp / (float)hp;

        if (currHp <= 0)
        {
            hpBar.gameObject.SetActive(false);

            gm.ShowClear();
            gm.StageClearDataAdd();

            Destroy(gameObject);
        }

        timeLimit -= Time.deltaTime;
        if (timeLimit <= 0)
        {
            // over
            gm.gameoverPanel.SetActive(true);
        }
    }

    public void EnterHit(Vector3 point)
    {
        currHp -= Random.Range(10, 15);

        GameObject bloodClone = Instantiate(blood, point - Vector3.forward, blood.transform.rotation);
        Destroy(bloodClone, 5f);
    }
}
