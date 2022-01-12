using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image barFillImage;

    [SerializeField] Text editableText = null;

    float maxHP = 100f;
    float curHp = 100f;

    public void UpdateHealthBar(float max, float cur, string editText = "")
    {
        barFillImage.fillAmount = cur / max;

        if(editText != "")
        {
            editableText.text = editText;
        }
    }
}
