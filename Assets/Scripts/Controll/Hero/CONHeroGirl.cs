using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CONHeroGirl : CONHero
{
	public Transform playerTrm;

	State curState;

	public override void Start()
	{
		//myAnim = this.GetComponent<Animator>();
		CONEntity nearObj = GameSceneClass.gMGPool.poolTotalDic[ePrefabs.Monster].Find(x => x.gameObject.activeSelf);
		curState = new Move(this, gameObject, _anim, playerTrm, attackRange);
	}

	public override void Update()
	{
		curState = this.curState.Process();
	}
}
