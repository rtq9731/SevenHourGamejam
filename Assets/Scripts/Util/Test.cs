using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public CONCharacter damageObj;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            damageObj.Hit(10);
        }
    }
}
