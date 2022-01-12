using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image gameOverPanel;
    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.gameObject.SetActive(false);
        //Button waveStartBtn = GameObject.Find("WaveStart").GetComponent<Button>();
        //print(waveStartBtn);
        //waveStartBtn.onClick.AddListener(() => Spawner.instance.WaveStart());
        FindObjectOfType<ConCastle>()._onDie += () => gameOverPanel.gameObject.SetActive(true);
        gameOverPanel.GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
            FindObjectOfType<ConCastle>().Init();
            FindObjectOfType<Spawner>().Init();
            gameOverPanel.gameObject.SetActive(false);
        });
    }
}
