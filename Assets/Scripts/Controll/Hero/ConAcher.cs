using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConAcher : CONCharacter
{
    [SerializeField] Transform attackSword;

    float speed;
    float damage;

    public void SetAttack(float damage, float speed)
    {
        this.speed = speed;
        this.damage = damage;
        CONEntity nearObj = GameSceneClass.gMGPool.poolTotalDic[ePrefabs.Monster].Find(x => x.gameObject.activeSelf);
        if (nearObj != null)
        {
            Attack(damage); // 원래 함수 실행
        }
    }

    public void Throw()
    {
        CONEntity nearObj = GameSceneClass.gMGPool.poolTotalDic[ePrefabs.Monster].Find(x => x.gameObject.activeSelf);
        if(nearObj != null)
        {
            (GameSceneClass.gMGPool.CreateObj(ePrefabs.AttackSword, attackSword.position) as AttackSword).SetAttack(nearObj != null ? nearObj.transform.position : Vector3.zero * Random.Range(-1f, 1f), speed, damage);
        }
    }
}
