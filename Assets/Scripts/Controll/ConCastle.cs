using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConCastle : CONCharacter
{
    public HealthBar hpBar = null;

    public Sprite[] castleSprites = null;
    public ConAcher[] achers = null;

    private SpriteRenderer _sr = null;

    bool isDie = false;

    Action<float> attackAct = (float attackPower) => { };
    Action _onDie = () => { };

    private new void Start()
    {
        hpBar.UpdateHealthBar(maxHP, curHP, curHP.ToString());
        _sr = GetComponent<SpriteRenderer>();
    }

    public override void Hit(float damage)
    {
        if(isDie)
        {
            return;
        }

        base.Hit(damage);
        if(curHP >= maxHP / 2)
        {
            _sr.sprite = castleSprites[0];
        }
        else
        {
            _sr.sprite = castleSprites[1];
        }

        if(curHP <= 0)
        {
            _onDie();
            _sr.sprite = castleSprites[2];
            isDie = true;
        }

        hpBar.UpdateHealthBar(maxHP, curHP, curHP.ToString());
    }
}
