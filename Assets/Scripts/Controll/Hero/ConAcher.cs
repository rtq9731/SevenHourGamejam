using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConAcher : CONCharacter
{
    [SerializeField] Transform attackSword;

    float speed;
    float damage;

    public void SetAttack(float speed, float damage)
    {
        this.speed = speed;
        this.damage = damage;
        Attack(damage); // 원래 함수 실행
    }

    public void Throw()
    {
        Vector2 target = GameSceneClass.gMGPool.poolTotalDic[ePrefabs.Monster].Find(x => x.gameObject.activeSelf).transform.position;
        (GameSceneClass.gMGPool.CreateObj(ePrefabs.AttackSword, attackSword.position) as AttackSword).SetAttack(target != null ? target : Vector2.zero, speed, damage);
    }
}
