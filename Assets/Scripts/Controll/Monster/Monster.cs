using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : CONCharacter
{

	public Transform playerTrm;

	State curState;

	public override void Start()
	{
		//myAnim = this.GetComponent<Animator>();
		playerTrm = FindObjectOfType<ConCastle>().transform;
		curState = new Move(this,gameObject, _anim, playerTrm,attackRange);
	}

    public override void Hit(float damage)
    {
		GameSceneClass.gMGPool.CreateObj(ePrefabs.HitEffect, transform.position);
        base.Hit(damage);
    }

    public override void Update()
	{
		curState = this.curState.Process();
	}
}
