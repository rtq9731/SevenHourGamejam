using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CONCharacter : CONEntity
{
    // 캐릭터가 가지고 있는 고유 스탯 선언
    // FSM, Detect 기능 등
    // 고유 캐릭터 스탯 데이터
    // 애니메이션 정보
    Animator _anim = null;

    [SerializeField] protected float curHP = 100f;
    [SerializeField] protected float maxHP = 100f;
    [SerializeField] protected float attackCool = 10f;
    [SerializeField] protected float attackPower = 10f;

    public virtual void Hit(float damage)
    {
        curHP -= damage;
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
