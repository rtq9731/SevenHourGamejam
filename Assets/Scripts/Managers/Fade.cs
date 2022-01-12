using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public static Fade Instance { get; set; }

    public Image fade;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

    }
    void Start()
    {
        
    }

    
    public void SetFade(System.Action callBack)
    {
        fade.color = new Color(0, 0, 0, 0);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(fade.DOFade(1, 0.5f));
        sequence.Append(fade.DOFade(0, 0.5f));
        sequence.OnComplete(()=>callBack());
    }
}
