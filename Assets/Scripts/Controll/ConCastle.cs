using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConCastle : CONCharacter
{
    public HealthBar hpBar = null;

    public float throwSpeed = 2f;
    public Sprite[] castleSprites = null;
    public ConAcher[] achers = null;

    private SpriteRenderer _sr = null;
    
    float lastAttackTime = 0f;

    bool isDie = false;

    Action<float, float> attackAct = (float attackPower, float speed) => { };
    Action _onDie = () => { };

    private new void Start()
    {
        hpBar = GameObject.Find("CastleHPBar").GetComponent<HealthBar>();
        hpBar.UpdateHealthBar(maxHP, curHP, curHP.ToString());
        _sr = GetComponent<SpriteRenderer>();

        foreach (var item in achers)
        {
            attackAct += item.SetAttack;
        }
    }

    private void Update()
    {
        if(Time.time >= lastAttackTime + attackCool)
        {
            lastAttackTime = Time.time;
            attackAct(attackPower, throwSpeed);
        }
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
