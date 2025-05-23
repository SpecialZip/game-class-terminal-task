using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropUI : MonoBehaviour
{
    [SerializeField] private Image firstProp;
    [SerializeField] private Image firstPropCooling;
    [SerializeField] private Image secondProp;
    [SerializeField] private Image secondPropCooling;

    [SerializeField] private GameObject speedUpUI;            //氮气加速UI图片

    private void Start()
    {
        PropManager.Instance.OnPropCollected+= CollectPropUI;
        PropManager.Instance.OnPropConsumed+= ConsumePropUI;
        firstPropCooling.gameObject.SetActive(false);
        secondPropCooling.gameObject.SetActive(false);
    }
    //更新道具栏UI
    private void ConsumePropUI(object sender, EventArgs e)
    {
        int propNum = PropManager.Instance.GetPropNum();
        if (propNum == 1)
        {
            foreach (Transform child in secondProp.transform)
            {
                Destroy(child.gameObject);
            }
        }
        else if (propNum == 0)
        {
            foreach (Transform child in firstProp.transform)
            {
                Destroy(child.gameObject);
            }
        }
        
        StartCoroutine(CooldownUI(firstPropCooling));
        StartCoroutine(CooldownUI(secondPropCooling));
    }

    private void CollectPropUI(object sender, EventArgs e)
    {
        int propNum = PropManager.Instance.GetPropNum();
        if (propNum == 1)
        {
            Debug.Log("创建道具UI1");
            Instantiate(speedUpUI, firstProp.transform);
        }
        else if (propNum == 2)
        {
            Debug.Log("创建道具UI2");

            Instantiate(speedUpUI, secondProp.transform);
        }
    }
    
    
    private IEnumerator CooldownUI(Image coolingImage)
    {
        coolingImage.gameObject.SetActive(true);
        coolingImage.fillAmount = 1f;

        float cooldownTime = 3f;
        float elapsedTime = 0f;

        while (elapsedTime < cooldownTime)
        {
            elapsedTime += Time.deltaTime;
            coolingImage.fillAmount = 1f - (elapsedTime / cooldownTime);
            yield return null;
        }

        coolingImage.gameObject.SetActive(false);
    }
}
