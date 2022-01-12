using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConCastle : CONCharacter
{
    public HealthBar hpBar = null;

    public Sprite[] castleSprites = null;


    private new void Start()
    {
        hpBar.UpdateHealthBar(maxHP, curHP, curHP.ToString());
    }

    public override void Hit(float damage)
    {
        base.Hit(damage);
        hpBar.UpdateHealthBar(maxHP, curHP, curHP.ToString());
    }
}
