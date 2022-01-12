using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button waveStartBtn = GameObject.Find("WaveStart").GetComponent<Button>();
        print(waveStartBtn);
        print(Spawner.instance);
        waveStartBtn.onClick.AddListener(() => Spawner.instance.WaveStart());
    }
}
