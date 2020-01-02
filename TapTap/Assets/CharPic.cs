using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharPic : MonoBehaviour
{
    public Animator anim;
    public TextMeshProUGUI bubbleText;
    public Sprite[] pics;
    [TextArea]public string[] talks;
    
    
    int n;

    private void Start()
    {
        n = Random.Range(0, pics.Length - 1);
        GetComponent<Image>().sprite = pics[n];
        bubbleText.text = talks[n];
    }

    public void ClickPic()
    {
        anim.SetTrigger("Out");
    }
    public void PicChange()
    {
        n += 1;
        if (n + 1 > pics.Length)
            n = 0;
        GetComponent<Image>().sprite = pics[n];
        bubbleText.text = talks[n];
    }
}
