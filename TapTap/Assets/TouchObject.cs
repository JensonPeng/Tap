using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchObject : MonoBehaviour
{
    Animator anim;
    GM gm;
    public int thisScore;
    public GameObject blood;

    void Start()
    {
        anim = GetComponent<Animator>();
        gm = FindObjectOfType<GM>();
    }

    public void EnterHit()
    {
        gm.AddScore(thisScore);

        GameObject bloodClone = Instantiate(blood, transform.position - Vector3.forward, blood.transform.rotation);
        Destroy(bloodClone, 5f);

        gameObject.SetActive(false);
    }

    bool triggerOnce = true;
    IEnumerator Alive()
    {
        while (true)
        {
            yield return new WaitForSeconds(gm.objAlive);
            if (triggerOnce)
            {
                anim.SetBool("escape", true);
                triggerOnce = false;
            }
        }
    }

    public void Enable()
    {
        triggerOnce = true;
        StartCoroutine(Alive());
    }
    public void Disable()
    {
        gameObject.SetActive(false);
        anim.SetBool("escape", false);
    }
}
