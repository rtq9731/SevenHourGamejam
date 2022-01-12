using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CONCharacter : CONEntity
{
    // 캐릭터가 가지고 있는 고유 스탯 선언
    // FSM, Detect 기능 등
    // 고유 캐릭터 스탯 데이터
    // 애니메이션 정보
    protected Animator _anim = null;

    public float curHP = 100f;
    public float maxHP = 100f;
    public float attackCool = 10f;
    public float attackPower = 10f;

    public virtual void Hit(float damage)
    {
        curHP -= damage;
        if(curHP < 0)
        {
            gameObject.SetActive(false);
        }
    }

    public virtual void Attack(float attackPower)
    {
        _anim.SetTrigger("Attack");
    }

    public override void Awake()
    {
        _anim = GetComponent<Animator>();
        curHP = maxHP;
        base.Awake();
    }

    public override void OnEnable()
    {
        curHP = maxHP;
        base.OnEnable();
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void cleanUpOnDisable()
    {

    }

    protected override void firstUpdate()
    {
        base.firstUpdate();
    }

}
