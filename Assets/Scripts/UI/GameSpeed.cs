using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeed : MonoBehaviour
{
    [SerializeField] Text timeScaleText;

    public void OnClcikGameSpeedChange()
    {
        if (Time.timeScale <= 2)
        {
            Time.timeScale++;
        }
        else
        {
            Time.timeScale = 1;
        }
        timeScaleText.text = Time.timeScale.ToString();
    }
}
