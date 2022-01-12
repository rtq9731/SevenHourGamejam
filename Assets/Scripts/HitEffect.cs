using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : CONEntity
{
    private void OnEnable()
    {
        Invoke("SetActiveFalse", 1f);
    }

    private void SetActiveFalse()
    {
        gameObject.SetActive(false);
    }
}
